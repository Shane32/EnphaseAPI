# GET /api/v4/systems/{system_id}/latest_telemetry — Latest Telemetry

Returns a system's last reported PV Power, Consumption Power, and Battery Power in Watts. Also returns the operational mode for Battery, Heat Pump (HP), and EVSE devices.

If the `last_report_at` is older than 7 days for a specific device, `last_report_at`, `power`, and `operational_mode` will be returned as `null`.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/latest_telemetry`

---

## Background: Consumption Power Calculation

**PV + Storage sites** are always configured in Net Config. **PV only sites** can be configured as Net or Total config.

- **PV only (Net config):** `consumption_power = production + grid`
- **PV only (Total config):** `consumption_power` is the raw value measured by the consumption CT (home load)
- **PV + Storage (Net config):** `consumption_power = production + grid` (battery not included)

> The consumption meter config is available in the [`/api/v4/systems/{system_id}/devices`](../systems/get-system-devices.md) endpoint via the `config_type` field.

---

## HP Operational Modes

| Mode | Description |
|------|-------------|
| `MODE_1` | Blocked Operation |
| `MODE_2` | Normal Operation |
| `MODE_3` | Power Consumption Recommended |
| `MODE_4` | Power Consumption Enforced |

## EVSE Operational Modes

| Mode | Description |
|------|-------------|
| `PLUGGED_OUT` | EVSE is not plugged in |
| `IDLE` | EVSE is plugged in but EV is not charging |
| `CHARGING` | EVSE is plugged in and EV is charging |
| `FAULTED` | The EVSE connector is faulted |

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. |

---

## Responses

### 200 — Latest Telemetry

```json
{
  "system_id": 698943141,
  "items": "devices",
  "devices": {
    "meters": [
      {
        "id": 1084690247,
        "name": "production",
        "channel": 1,
        "last_report_at": 1701418500,
        "power": 755
      },
      {
        "id": 1084690247,
        "name": "production",
        "channel": 2,
        "last_report_at": null,
        "power": null
      },
      {
        "id": 1084690248,
        "name": "consumption",
        "channel": 1,
        "last_report_at": 1701418500,
        "power": 12
      }
    ],
    "encharges": [
      {
        "id": 1084690255,
        "name": "Encharge 492205005191",
        "channel": 1,
        "last_report_at": 1701087429,
        "power": -15,
        "operational_mode": "Idle"
      }
    ],
    "heat-pump": [
      {
        "serial_number": "",
        "name": "Viessmann VitoCal 300G",
        "last_report_at": 1757583826,
        "operational_mode": "MODE_2"
      }
    ],
    "evse": [
      {
        "serial_number": "380699",
        "name": "Wallbox",
        "last_report_at": 1757583779,
        "operational_mode": "PLUGGED_OUT"
      }
    ]
  }
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `items` | string | Always `"devices"` |
| `devices.meters` | array | Meter readings (production and consumption) |
| `devices.meters[].id` | integer | Meter device ID |
| `devices.meters[].name` | string | Meter name (`production` or `consumption`) |
| `devices.meters[].channel` | integer | Meter channel number |
| `devices.meters[].last_report_at` | integer\|null | Unix epoch of last report (`null` if older than 7 days) |
| `devices.meters[].power` | integer\|null | Current power in Watts (`null` if older than 7 days) |
| `devices.encharges` | array | Encharge battery readings |
| `devices.encharges[].id` | integer | Encharge device ID |
| `devices.encharges[].name` | string | Encharge device name |
| `devices.encharges[].channel` | integer | Channel number |
| `devices.encharges[].last_report_at` | integer\|null | Unix epoch of last report |
| `devices.encharges[].power` | integer\|null | Current power in Watts (negative = charging) |
| `devices.encharges[].operational_mode` | string\|null | Current operational mode |
| `devices.heat-pump` | array | Heat pump readings |
| `devices.heat-pump[].serial_number` | string | Heat pump serial number |
| `devices.heat-pump[].name` | string | Heat pump name |
| `devices.heat-pump[].last_report_at` | integer\|null | Unix epoch of last report |
| `devices.heat-pump[].operational_mode` | string\|null | Current operational mode (MODE_1 through MODE_4) |
| `devices.evse` | array | EVSE charger readings |
| `devices.evse[].serial_number` | string | EVSE serial number |
| `devices.evse[].name` | string | EVSE name |
| `devices.evse[].last_report_at` | integer\|null | Unix epoch of last report |
| `devices.evse[].operational_mode` | string\|null | Current operational mode |

### 401 — Authentication Error

```json
{
  "message": "Not Authorized",
  "details": "User is not authorized",
  "code": 401
}
```

### 403 — Forbidden

```json
{
  "message": "Forbidden",
  "details": "Not authorized to access this resource",
  "code": 403
}
```

### 404 — Not Found

```json
{
  "message": "Not Found",
  "details": "System not found for {:id=>\"1\"}",
  "code": 404
}
```

### 405 — Method Not Allowed

```json
{
  "reason": "405",
  "message": ["Method not allowed"]
}
```

### 429 — Too Many Requests

```json
{
  "message": "Too Many Requests",
  "details": "Usage limit exceeded for plan Kilowatt",
  "code": 429
}
```

### 501 — Not Implemented

```json
{
  "reason": "501",
  "message": ["Not Implemented"]
}
```

---

## See Also

- [Live Data](../streaming/live-data.md) — Real-time streaming live status
- [Get System Summary](../systems/get-system-summary.md) — System summary including current power
- [Get System Devices](../systems/get-system-devices.md) — Full device list with `config_type`
- [Back to API Index](../README.md)
