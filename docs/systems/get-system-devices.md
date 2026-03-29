# GET /api/v4/systems/{system_id}/devices — Retrieve Devices for a System

Retrieves devices for a given system. Only devices that are active will be returned in the response.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/devices`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. |

---

## Responses

### 200 — List of Devices

```json
{
  "system_id": 698910067,
  "total_devices": 11,
  "items": "devices",
  "devices": {
    "micros": [
      {
        "id": 1023273222,
        "last_report_at": 1508174262,
        "name": "Microinverter 902167438951",
        "serial_number": "902167438951",
        "part_number": "800-01333-r01",
        "sku": "IQ8A-72-2-US",
        "model": "M250",
        "status": "normal",
        "active": true,
        "product_name": "M250"
      }
    ],
    "meters": [
      {
        "id": 1059640322,
        "last_report_at": 1508174262,
        "name": "production",
        "serial_number": "901553005272EIM1",
        "part_number": "800-00655-r08",
        "sku": null,
        "model": "Envoy S",
        "status": "normal",
        "active": true,
        "state": "enabled",
        "config_type": "Net",
        "product_name": "RGM"
      }
    ],
    "gateways": [
      {
        "id": 1059563029,
        "last_report_at": 1508174262,
        "name": "Gateway 202323054201",
        "serial_number": "901553005272",
        "part_number": "800-00655-r08",
        "emu_sw_version": "D4.6.11.170403 (799d2d)",
        "sku": "ENV-IQ-AM1-240",
        "model": "Envoy-S-Standard-NA",
        "status": "normal",
        "active": true,
        "cellular_modem": {
          "imei": "352009112238477",
          "part_num": "860-00157-r01",
          "sku": "CELLMODEM-M1",
          "plan_start_date": 1614796200,
          "plan_end_date": 1772562600
        },
        "product_name": "Envoy-S-Metered-EU"
      }
    ],
    "q_relays": [
      {
        "id": 1059640316,
        "last_report_at": 1508174262,
        "name": "IQ Relay 912158973973",
        "serial_number": "912158973973",
        "part_number": "800-00595-r01",
        "sku": "Q-RELAY-1P-INT",
        "model": "",
        "status": "normal",
        "active": true,
        "product_name": "IQ Relay"
      }
    ],
    "acbs": [
      {
        "id": 1059640321,
        "last_report_at": 1508174262,
        "name": "AC Battery 911364446952",
        "serial_number": "911364446952",
        "part_number": "800-00560-r03",
        "sku": "IQ7-B1200-LN-I-INT01-RV0",
        "model": "",
        "status": "normal",
        "active": true,
        "product_name": "ACB"
      }
    ],
    "encharges": [
      {
        "id": 1059640295,
        "last_report_at": 1508174262,
        "name": "IQ Battery 492312001241",
        "serial_number": "121593621979",
        "part_number": "800-00562-r01",
        "sku": "B03-A01-US00-1-3",
        "model": "",
        "status": "normal",
        "active": true,
        "product_name": "IQ Battery R3 - 5P"
      }
    ],
    "enpowers": [
      {
        "id": 1059640294,
        "last_report_at": 1508174262,
        "name": "IQ System Controller 482218007023",
        "serial_number": "121245173988",
        "part_number": "800-01135-r02",
        "sku": "EP200G101-M240US00",
        "model": "",
        "status": "normal",
        "active": true,
        "product_name": "IQ System Controller"
      }
    ],
    "ev_chargers": [
      {
        "id": 202313029329,
        "last_report_at": 1686134789,
        "name": "IQ EV Charger 202313029329",
        "serial_number": "202313029329",
        "part_number": "861-02006 09",
        "sku": "IQ-EVSE-NA-1060-0100-0100",
        "model": "IQ-EVSE-60R",
        "status": "normal",
        "active": "true",
        "firmware": "v0.04.17"
      }
    ],
    "iq_collars": [
      {
        "id": 1085310043,
        "last_report_at": 1757523711,
        "name": "IQ Meter Collar 482443008618",
        "serial_number": "482443008618",
        "part_number": "865-00401-r01",
        "sku": "MC-200-011-V01",
        "model": "",
        "status": "normal",
        "active": true,
        "product_name": "IQ Meter Collar"
      }
    ]
  }
}
```

### Device Categories

| Category | Description |
|----------|-------------|
| `micros` | Microinverters |
| `meters` | Production/consumption meters. The `config_type` field indicates `Net` or `Production` configuration. |
| `gateways` | Envoy gateway devices. May include `cellular_modem` details. |
| `q_relays` | IQ Relay devices |
| `acbs` | AC Battery devices |
| `encharges` | IQ Battery (Encharge) devices |
| `enpowers` | IQ System Controller (Enpower) devices |
| `ev_chargers` | EV Charger devices |
| `iq_collars` | IQ Meter Collar devices |

### Common Device Fields

| Field | Type | Description |
|-------|------|-------------|
| `id` | integer | Unique device ID |
| `last_report_at` | integer | Unix epoch of last report |
| `name` | string | Device display name |
| `serial_number` | string | Device serial number |
| `part_number` | string | Part number |
| `sku` | string\|null | SKU identifier |
| `model` | string | Device model |
| `status` | string | Device status (e.g., `normal`) |
| `active` | boolean | Whether the device is active |
| `product_name` | string | Product name |

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

### 500 — Internal Server Error

```json
{
  "message": "Internal Server Error",
  "details": "unable to fetch data",
  "code": 500
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

- [Get System by ID](get-system-by-id.md) — Full system details
- [Micro Telemetry](../devices/micro-telemetry.md) — Telemetry for a single microinverter
- [Encharge Telemetry](../devices/encharge-telemetry.md) — Telemetry for a single Encharge battery
- [EV Charger Devices](../ev-charger/get-ev-charger-devices.md) — Active EV chargers for a system
- [Back to API Index](../README.md)
