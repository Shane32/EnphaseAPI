#!/usr/bin/env python3
"""
Comprehensive audit comparing HTML Swagger models to .NET C# model classes.
"""

import re
from pathlib import Path
from bs4 import BeautifulSoup, Tag, NavigableString

# ============================================================
# STEP 1: Parse HTML to extract model definitions
# ============================================================

html_path = Path("/home/runner/work/EnphaseAPI/EnphaseAPI/docs/models.html")
html_content = html_path.read_text(encoding="utf-8")
soup = BeautifulSoup(html_content, "html.parser")


def _get_inner_span(outer_model_span: Tag):
    """Return the immediate inner <span class=""> of <span class="model">."""
    for child in outer_model_span.children:
        if isinstance(child, Tag) and child.name == "span":
            cls = child.get("class") or []
            if cls == [] or cls == [""]:
                return child
    return outer_model_span.find("span")


def _extract_type_info(td: Tag) -> dict:
    """
    Extract type information from a property's <td> element.

    Returns a dict with keys: type, format, is_array, ref, array_item_type,
    array_item_format, is_object, is_anon_object.

    Detection logic:
      - Simple type:    inner_span has <span class="prop"> → use prop-type/prop-format
      - Named obj ref:  button has <span class="pointer"> → use model-title__text
      - Array:          button is toggle-only AND '[' is a direct text child
      - Inline object:  button is toggle-only AND '{' brace-open is a direct child
    """
    prop_info = {}

    outer_model_span = td.find("span", class_="model")
    if not outer_model_span:
        return prop_info

    inner_span = _get_inner_span(outer_model_span)
    if not inner_span:
        return prop_info

    # 1. Check for simple type: <span class="prop"> as DIRECT child of inner_span
    prop_span = None
    for child in inner_span.children:
        if isinstance(child, Tag) and child.name == "span" and "prop" in (child.get("class") or []):
            prop_span = child
            break

    if prop_span:
        pt = prop_span.find("span", class_="prop-type")
        if pt:
            prop_info["type"] = pt.get_text(strip=True)
        fmt = prop_span.find("span", class_="prop-format")
        if fmt:
            prop_info["format"] = fmt.get_text(strip=True).strip("()$")
        return prop_info

    # 2. Find direct button child of inner_span
    btn = None
    for child in inner_span.children:
        if isinstance(child, Tag) and child.name == "button":
            btn = child
            break

    if not btn:
        return prop_info

    has_pointer = bool(btn.find("span", class_="pointer"))

    if has_pointer:
        # Named object reference (collapsed or expanded with pointer)
        title = btn.find("span", class_="model-title__text")
        if title:
            prop_info["ref"] = title.get_text(strip=True)
            prop_info["is_object"] = True
        return prop_info

    # Toggle-only button — determine array vs inline object from siblings
    has_bracket = False
    has_brace_open = False
    children_after_btn = []
    past_btn = False

    for child in inner_span.children:
        if child is btn:
            past_btn = True
            continue
        if past_btn:
            children_after_btn.append(child)
        if isinstance(child, NavigableString) and "[" in str(child):
            has_bracket = True
        if isinstance(child, Tag) and "brace-open" in (child.get("class") or []):
            has_brace_open = True

    if has_bracket:
        # Array type
        prop_info["is_array"] = True
        # Find the nested item model name
        for sibling in children_after_btn:
            if not isinstance(sibling, Tag):
                continue
            # Look for a nested <span class="model"> containing a button with pointer
            nested_model_spans = sibling.find_all("span", class_="model")
            for nms in nested_model_spans:
                inner2 = _get_inner_span(nms)
                if not inner2:
                    continue
                for c in inner2.children:
                    if isinstance(c, Tag) and c.name == "button":
                        ptr = c.find("span", class_="pointer")
                        if ptr:
                            title = ptr.find("span", class_="model-title__text")
                            if title:
                                prop_info["ref"] = title.get_text(strip=True)
                                return prop_info
                        # Toggle-only nested button → look for prop-type (primitive array)
                        prop_span2 = inner2.find("span", class_="prop")
                        if prop_span2:
                            pt2 = prop_span2.find("span", class_="prop-type")
                            if pt2:
                                prop_info["array_item_type"] = pt2.get_text(strip=True)
                            fmt2 = prop_span2.find("span", class_="prop-format")
                            if fmt2:
                                prop_info["array_item_format"] = fmt2.get_text(strip=True).strip("()$")
                            return prop_info
    elif has_brace_open:
        # Inline-expanded anonymous object or anon object with additionalProperties
        prop_info["is_anon_object"] = True

    return prop_info


def parse_html_model(container: Tag):
    """
    Parse a model-container div.
    Returns (model_name, {prop_name: prop_info}).
    Only parses DIRECT properties (top-level table rows only).
    """
    model_name = container.get("data-name", "")
    props = {}

    # Top-level inner-object span
    inner_obj = container.find("span", class_="inner-object")
    if not inner_obj:
        return model_name, props

    # Outermost table.model
    top_table = inner_obj.find("table", class_="model")
    if not top_table:
        return model_name, props

    top_tbody = top_table.find("tbody")
    if not top_tbody:
        return model_name, props

    # DIRECT <tr> children only — not nested table rows
    for row in top_tbody.children:
        if not isinstance(row, Tag) or row.name != "tr":
            continue
        if "property-row" not in (row.get("class") or []):
            continue

        tds = [td for td in row.children if isinstance(td, Tag) and td.name == "td"]
        if not tds:
            continue

        # Property name
        name_td = tds[0]
        star = name_td.find("span", class_="star")
        if star:
            star.extract()
        prop_name = name_td.get_text(strip=True)

        required = "required" in (row.get("class") or [])

        prop_info = {"required": required}
        if len(tds) > 1:
            prop_info.update(_extract_type_info(tds[1]))

        props[prop_name] = prop_info

    return model_name, props


# Parse all HTML models
html_models = {}
for container in soup.find_all("div", class_="model-container"):
    name, props = parse_html_model(container)
    if name:
        html_models[name] = props

print(f"Parsed {len(html_models)} HTML models")

# ============================================================
# STEP 2: Parse C# files
# ============================================================

CS_FILES = [
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Config/BatterySettings.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Config/GetLoadControlResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Config/GridStatus.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Config/LoadControlChannel.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Config/StormGuardSettings.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Config/UpdateBatterySettingsRequest.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/BatteryEnergyData.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/BatterySocData.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/BatteryTelemetryInterval.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/ConsumptionMeterInterval.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/ExportTelemetryInterval.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/GetBatteryLifetimeResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/GetBatteryTelemetryResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/GetConsumptionLifetimeResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/GetConsumptionMeterReadingsResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/GetConsumptionMeterTelemetryResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/GetEnergyExportLifetimeResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/GetEnergyExportTelemetryResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/GetEnergyImportLifetimeResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/GetEnergyImportTelemetryResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/GetLatestTelemetryResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/GetStorageMeterReadingsResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/ImportTelemetryInterval.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/LatestTelemetryDevices.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/LatestTelemetryEncharge.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/LatestTelemetryEvse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/LatestTelemetryHeatPump.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Consumption/LatestTelemetryMeter.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Devices/EnchargeEnergyData.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Devices/EnchargeInterval.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Devices/EnchargeSocData.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Devices/EvseInterval.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Devices/GetEnchargeTelemetryResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Devices/GetEvseLifetimeResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Devices/GetEvseTelemetryResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Devices/GetHpLifetimeResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Devices/GetHpTelemetryResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Devices/GetMicroTelemetryResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Devices/HpInterval.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Devices/MicroInterval.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/EvCharger/ChargerSchedule.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/EvCharger/ChargerScheduleTimeBlock.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/EvCharger/ChargingCommandResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/EvCharger/ChargingSession.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/EvCharger/EvChargerDeviceInfo.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/EvCharger/EvChargerDevices.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/EvCharger/EvChargerDevicesContainer.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/EvCharger/EvChargerEvent.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/EvCharger/EvChargerTelemetryInterval.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/EvCharger/GetEvChargerEventsResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/EvCharger/GetEvChargerLifetimeResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/EvCharger/GetEvChargerSchedulesResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/EvCharger/GetEvChargerSessionsResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/EvCharger/GetEvChargerTelemetryResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/EvCharger/StartChargingRequest.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Production/GetEnergyLifetimeResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Production/GetProductionMeterReadingsResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Production/GetProductionMeterTelemetryResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Production/GetProductionMicroTelemetryResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Production/GetRgmStatsResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Production/ProductionMeterReading.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Production/RgmInterval.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Production/RgmMeterChannel.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Production/RgmMeterInterval.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Production/TelemetryInterval.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Production/TelemetryProductionMeterInterval.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Shared/SystemAddress.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Shared/SystemMeta.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/AlarmEvent.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/CellularModem.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/DeviceCollection.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/DeviceInfo.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/EvChargerDevice.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/EventType.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/GatewayDevice.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/GetEventTypesResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/GetOpenEventsResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/GetSystemAlarmsResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/GetSystemEventsResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/GetSystemsResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/InvertersSummaryItem.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/MeterDevice.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/MicroInverterInfo.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/PowerValue.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/RetrieveSystemIdResponse.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/SearchSystemsFilter.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/SearchSystemsRequest.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/SystemAlarm.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/SystemDevices.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/SystemEvent.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/SystemInfo.cs",
    "/home/runner/work/EnphaseAPI/EnphaseAPI/Project/Models/Systems/SystemSummary.cs",
]


def parse_cs_file(filepath: str) -> dict:
    """Parse a C# file, extract class/enum info."""
    content = Path(filepath).read_text(encoding="utf-8")
    class_name = Path(filepath).stem

    # Override class name from source
    cm = re.search(r"\bclass\s+(\w+)\b", content)
    if cm:
        class_name = cm.group(1)

    # Parse enums — look for [JsonPropertyName] or [EnumMember(Value=...)] on each member
    enums = {}
    for em in re.finditer(r"public\s+enum\s+(\w+)\s*\{([^}]+)\}", content, re.DOTALL):
        enum_name = em.group(1)
        body = em.group(2)
        members = []
        for mm in re.finditer(
            r'(?:'
            r'\[JsonPropertyName\s*\(\s*"([^"]+)"\s*\)\s*\]'
            r'|'
            r'\[EnumMember\s*\(\s*Value\s*=\s*"([^"]+)"\s*\)\s*\]'
            r')?\s*(\b[A-Za-z_]\w*\b)\s*(?:=[^,\n]*)?\s*[,\n]',
            body,
        ):
            json_name = mm.group(1) or mm.group(2)
            cs_name = mm.group(3)
            if cs_name:
                members.append({"cs_name": cs_name, "json_name": json_name})
        enums[enum_name] = members

    # Parse properties — find all [JsonPropertyName("...")] occurrences
    properties = []
    required_props = set()

    for jpn_match in re.finditer(
        r'\[JsonPropertyName\s*\(\s*"([^"]+)"\s*\)\s*\]', content, re.DOTALL
    ):
        json_name = jpn_match.group(1)

        pre_text = content[max(0, jpn_match.start() - 200): jpn_match.start()]
        post_text = content[jpn_match.end(): jpn_match.end() + 500]

        # Check for [Required] immediately before or after [JsonPropertyName]
        has_required = bool(re.search(r"\[Required\]", pre_text[-60:])) or \
                       bool(re.search(r"\[Required\]", post_text[:120]))
        if has_required:
            required_props.add(json_name)

        # Strip additional attribute blocks after [JsonPropertyName]
        remaining = post_text
        while True:
            stripped = remaining.lstrip()
            if stripped.startswith("["):
                end = stripped.find("]")
                if end >= 0:
                    remaining = stripped[end + 1:]
                else:
                    break
            else:
                break

        # Match: public <Type> <Name> { get;
        prop_decl = re.search(
            r"public\s+([\w<>\[\]?,\s]+?)\s+(\w+)\s*\{",
            remaining,
            re.DOTALL,
        )
        if prop_decl:
            cs_type = re.sub(r"\s+", " ", prop_decl.group(1)).strip()
            cs_name = prop_decl.group(2)
            properties.append({
                "json_name": json_name,
                "cs_type": cs_type,
                "cs_name": cs_name,
                "nullable": cs_type.endswith("?"),
            })

    return {
        "file": filepath,
        "class_name": class_name,
        "properties": properties,
        "required_props": required_props,
        "enums": enums,
        "content": content,
    }


all_cs_data = {}
all_cs_enums = {}

for f in CS_FILES:
    data = parse_cs_file(f)
    stem = Path(f).stem
    all_cs_data[stem] = data
    all_cs_enums.update(data["enums"])

print(f"Parsed {len(all_cs_data)} C# files")
if all_cs_enums:
    print(f"Found {len(all_cs_enums)} C# enums: {sorted(all_cs_enums.keys())}")

# ============================================================
# STEP 3: HTML → C# class mapping
# ============================================================
# The HTML page covers only EV-charger models.

HTML_TO_CS = {
    # HTML model name           : C# file stem
    "DefaultCommandResponse":       "ChargingCommandResponse",
    "StartChargingRequestBody":     "StartChargingRequest",
    "EVIntervalEnergyResponse":     "GetEvChargerTelemetryResponse",
    "EnergyConsumption":            "EvChargerTelemetryInterval",
    "ChargeSessionDetail":          "ChargingSession",
    "ChargeSessionDetailsResponse": "GetEvChargerSessionsResponse",
    "Schedule":                     "ChargerScheduleTimeBlock",
    "ScheduleList":                 "ChargerSchedule",
    "SchedulesBodyResponse":        "GetEvChargerSchedulesResponse",
    "EVEnergyResponse":             "GetEvChargerLifetimeResponse",
    "EVEventsResponse":             "GetEvChargerEventsResponse",
    "EventDetail":                  "EvChargerEvent",
    "ChargerSummary":               "EvChargerDeviceInfo",
    "ChargerSummaryResponse":       "EvChargerDevices",
}

HTML_NO_CS = {"ErrorResponse", "ErrorResponseArray"}
cs_covered = set(HTML_TO_CS.values())
cs_not_in_html = sorted(set(all_cs_data.keys()) - cs_covered)

# ============================================================
# STEP 4: Type-compatibility helpers
# ============================================================

def cs_base_type(cs_type: str) -> str:
    return cs_type.rstrip("?").strip()


def cs_lower(cs_type: str) -> str:
    return cs_base_type(cs_type).lower()


def is_list_type(cs_type: str) -> bool:
    b = cs_lower(cs_type)
    return "list<" in b or b.endswith("[]") or "ienumerable<" in b


def format_html_type(hp: dict) -> str:
    """Human-readable HTML type string for the property table."""
    req = "* " if hp.get("required") else "  "
    if hp.get("is_array"):
        ref = hp.get("ref", "")
        ait = hp.get("array_item_type", "")
        inner = ref or ait or "?"
        return f"{req}array<{inner}>"
    if hp.get("is_object"):
        return f"{req}object({hp.get('ref', '?')})"
    if hp.get("is_anon_object"):
        return f"{req}object(anon)"
    t = hp.get("type", "?")
    fmt = hp.get("format", "").replace("$", "")
    return f"{req}{t}({fmt})" if fmt else f"{req}{t}"


def check_type_compat(json_name: str, hp: dict, cp: dict, all_enums: dict) -> list:
    """Return list of discrepancy strings for one property."""
    issues = []
    cs_type = cp["cs_type"]
    cs_nullable = cp["nullable"]
    cs_b = cs_base_type(cs_type)
    cs_l = cs_lower(cs_type)

    html_required = hp.get("required", False)
    is_array = hp.get("is_array", False)
    is_object = hp.get("is_object", False)
    is_anon_object = hp.get("is_anon_object", False)
    ref = hp.get("ref", "")
    html_type = hp.get("type", "")
    html_fmt = hp.get("format", "").replace("$", "").lower()
    array_item_type = hp.get("array_item_type", "")
    array_item_fmt = hp.get("array_item_format", "").replace("$", "").lower()

    # --- Required / nullable ---
    if html_required and cs_nullable:
        issues.append(
            f"  REQUIRED mismatch: HTML marks '{json_name}' as required (*) "
            f"but C# type is nullable ({cs_type})"
        )
    elif not html_required and not cs_nullable and cs_l not in ("string",):
        issues.append(
            f"  OPTIONAL mismatch: HTML marks '{json_name}' as optional "
            f"but C# type is non-nullable ({cs_type})"
        )

    # --- Array ---
    if is_array:
        if not is_list_type(cs_type):
            issues.append(
                f"  TYPE mismatch: HTML '{json_name}' is array "
                f"but C# type '{cs_type}' is not a list type"
            )
        # If we know the item type, check the inner generic parameter
        if array_item_type and is_list_type(cs_type):
            inner_match = re.search(r"List<(.+?)>", cs_type, re.IGNORECASE)
            if inner_match:
                inner_cs = inner_match.group(1).rstrip("?").strip().lower()
                exp = array_item_type.lower()
                if exp == "integer":
                    if array_item_fmt == "int64" and inner_cs not in ("long",):
                        issues.append(
                            f"  TYPE mismatch: HTML '{json_name}' is array<integer(int64)> "
                            f"but C# inner type is '{inner_match.group(1)}' (expected long)"
                        )
                    elif array_item_fmt == "int32" and inner_cs not in ("int", "long"):
                        issues.append(
                            f"  TYPE mismatch: HTML '{json_name}' is array<integer(int32)> "
                            f"but C# inner type is '{inner_match.group(1)}' (expected int)"
                        )
                elif exp == "string" and inner_cs not in ("string",):
                    issues.append(
                        f"  TYPE mismatch: HTML '{json_name}' is array<string> "
                        f"but C# inner type is '{inner_match.group(1)}' (expected string)"
                    )
        return issues

    # --- Named object reference ---
    if is_object and ref:
        if cs_l in ("string", "int", "long", "bool", "double", "float", "decimal"):
            issues.append(
                f"  TYPE mismatch: HTML '{json_name}' is object ref '{ref}' "
                f"but C# type is primitive '{cs_type}'"
            )
        return issues

    # --- Anonymous object (additionalProperties / map type) ---
    if is_anon_object:
        # C# typically models this as a typed class or dictionary; just note it
        if cs_l in ("string", "int", "long", "bool", "double", "float", "decimal"):
            issues.append(
                f"  TYPE mismatch: HTML '{json_name}' is an anonymous object "
                f"but C# type is primitive '{cs_type}'"
            )
        return issues

    # --- Simple types ---
    if html_type == "string":
        if html_fmt in ("date-time", "date"):
            if cs_l not in ("datetimeoffset", "datetime", "string"):
                issues.append(
                    f"  TYPE mismatch: HTML '{json_name}' is string({html_fmt}) "
                    f"but C# type is '{cs_type}' (expected DateTimeOffset/DateTime)"
                )
        else:
            if cs_l not in ("string",) and cs_b not in all_enums:
                issues.append(
                    f"  TYPE mismatch: HTML '{json_name}' is string "
                    f"but C# type is '{cs_type}'"
                )

    elif html_type == "integer":
        if html_fmt == "int64":
            if cs_l == "int":
                issues.append(
                    f"  TYPE note: HTML '{json_name}' is integer(int64) "
                    f"but C# type is int (potential precision loss; consider long)"
                )
            elif cs_l not in ("long", "int"):
                issues.append(
                    f"  TYPE mismatch: HTML '{json_name}' is integer(int64) "
                    f"but C# type is '{cs_type}' (expected long)"
                )
        else:
            if cs_l == "long":
                issues.append(
                    f"  TYPE note: HTML '{json_name}' is integer(int32) "
                    f"but C# uses long (wider than needed)"
                )
            elif cs_l not in ("int", "long"):
                issues.append(
                    f"  TYPE mismatch: HTML '{json_name}' is integer "
                    f"but C# type is '{cs_type}' (expected int)"
                )

    elif html_type == "number":
        if html_fmt == "double":
            if cs_l not in ("double", "decimal", "float"):
                issues.append(
                    f"  TYPE mismatch: HTML '{json_name}' is number(double) "
                    f"but C# type is '{cs_type}' (expected double)"
                )
        else:
            if cs_l not in ("double", "decimal", "float", "single"):
                issues.append(
                    f"  TYPE mismatch: HTML '{json_name}' is number "
                    f"but C# type is '{cs_type}' (expected double/decimal)"
                )

    elif html_type == "boolean":
        if cs_l not in ("bool",):
            issues.append(
                f"  TYPE mismatch: HTML '{json_name}' is boolean "
                f"but C# type is '{cs_type}' (expected bool)"
            )

    return issues


# ============================================================
# STEP 5: Compare models
# ============================================================

discrepancies = {}
matching_models = []

for html_name, cs_name in sorted(HTML_TO_CS.items()):
    if cs_name not in all_cs_data:
        discrepancies[html_name] = [f"  C# class '{cs_name}' not found in parsed files"]
        continue

    html_props = html_models.get(html_name, {})
    cs_data = all_cs_data[cs_name]
    cs_props = {p["json_name"]: p for p in cs_data["properties"]}

    issues = []

    for prop_name, hp in html_props.items():
        if prop_name not in cs_props:
            issues.append(
                f"  MISSING in C#: '{prop_name}' exists in HTML but not in C# class '{cs_name}'"
            )
            continue
        issues.extend(check_type_compat(prop_name, hp, cs_props[prop_name], all_cs_enums))

    for json_name in cs_props:
        if json_name not in html_props:
            issues.append(
                f"  EXTRA in C#: '{json_name}' exists in C# class '{cs_name}' "
                f"but not in HTML model '{html_name}'"
            )

    if issues:
        discrepancies[html_name] = issues
    else:
        matching_models.append(html_name)

# ============================================================
# STEP 6: Print report
# ============================================================

SEP = "=" * 80
sep = "-" * 80

print()
print(SEP)
print("MODEL AUDIT REPORT")
print(SEP)
print()
print("## OVERVIEW")
print(f"  HTML models parsed:            {len(html_models)}")
print(f"  C# classes parsed:             {len(all_cs_data)}")
print(f"  HTML→C# mappings defined:      {len(HTML_TO_CS)}")
print(f"  HTML models with no C# match:  {len(HTML_NO_CS)}")
print(f"  C# classes not in HTML:        {len(cs_not_in_html)}")
print(f"  Matched models with issues:    {len(discrepancies)}")
print(f"  Matched models fully clean:    {len(matching_models)}")

print()
print("## HTML MODELS WITH NO C# COUNTERPART IN THE LISTED FILES")
for name in sorted(HTML_NO_CS):
    props = html_models.get(name, {})
    pnames = ", ".join(props.keys()) if props else "(none)"
    print(f"  - {name}")
    print(f"    Properties: {pnames}")

print()
print("## C# CLASSES NOT REPRESENTED IN HTML")
print("  (These C# classes have no corresponding model in docs/models.html)")
for name in cs_not_in_html:
    cs_data = all_cs_data[name]
    prop_names = [p["json_name"] for p in cs_data["properties"]]
    print(f"  - {name}  ({len(prop_names)} properties: {', '.join(prop_names[:6])}{'...' if len(prop_names) > 6 else ''})")

print()
print("## MATCHED MODELS — FULL PROPERTY COMPARISON")
print()

for html_name in sorted(HTML_TO_CS.keys()):
    cs_name = HTML_TO_CS[html_name]
    html_props = html_models.get(html_name, {})
    cs_data = all_cs_data.get(cs_name)
    cs_props = {p["json_name"]: p for p in cs_data["properties"]} if cs_data else {}

    all_prop_names = sorted(set(list(html_props.keys()) + list(cs_props.keys())))

    print(f"  {sep}")
    print(f"  HTML: {html_name}  →  C#: {cs_name}")
    print(f"  {sep}")
    print(f"  {'Property':<32} {'HTML Type':<30} {'C# Type':<32} Status")
    print(f"  {'─'*32} {'─'*30} {'─'*32} {'─'*10}")

    for pn in all_prop_names:
        in_html = pn in html_props
        in_cs = pn in cs_props

        html_col = format_html_type(html_props[pn]) if in_html else "(missing)"
        cs_col = cs_props[pn]["cs_type"] if in_cs else "(missing)"

        if in_html and in_cs:
            q_issues = check_type_compat(pn, html_props[pn], cs_props[pn], all_cs_enums)
            if q_issues:
                status = "⚠ ISSUE"
            else:
                status = "✓ OK"
        elif in_html:
            status = "✗ MISSING"
        else:
            status = "+ EXTRA"

        print(f"  {pn:<32} {html_col:<30} {cs_col:<32} {status}")

    print()

print()
print("## DISCREPANCIES — DETAIL")
print()

if not discrepancies:
    print("  No discrepancies found across all matched models!")
else:
    for html_name, issues in sorted(discrepancies.items()):
        cs_name = HTML_TO_CS.get(html_name, "?")
        print(f"  ### {html_name}  (C#: {cs_name})")
        for issue in issues:
            print(issue)
        print()

print()
print("## FULLY MATCHING MODELS (zero discrepancies)")
if matching_models:
    for name in sorted(matching_models):
        cs_name = HTML_TO_CS[name]
        print(f"  ✓ {name}  →  {cs_name}")
else:
    print("  (none)")

print()
print(SEP)
print("END OF REPORT")
print(SEP)
