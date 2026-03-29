# GET /api/v4/systems/inverters_summary_by_envoy_or_site — Inverters Summary by Envoy or Site

Returns the microinverters summary based on the specified active Envoy serial number or system. If both `envoy_serial_number` and `site_id` are provided, only `envoy_serial_number` is used to generate the response.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/inverters_summary_by_envoy_or_site`

---

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `site_id` | integer | No* | Site ID. Response contains only microinverters reporting to one of the active Envoys of the given site. |
| `envoy_serial_number` | integer | No* | Envoy serial number. Only microinverters reporting to the given Envoy will be present in the response. |
| `page` | integer | No | The page to be returned. Default=1, Min=1. |
| `size` | integer | No | Maximum number of records shown per page. Default=10, Min=1, Max=100. |

> **\*Note:** At least one of `site_id` or `envoy_serial_number` must be provided. If both are provided, `envoy_serial_number` takes precedence.

---

## Responses

### 200 — Inverters Summary

```json
[
  {
    "signal_strength": 5,
    "micro_inverters": [
      {
        "id": 1059689835,
        "serial_number": "688346865858",
        "model": "M215",
        "part_number": "800-00107-r01",
        "sku": "M215-60-2LL-S22-NA",
        "status": "normal",
        "power_produced": {
          "value": 96,
          "units": "W",
          "precision": 0
        },
        "proc_load": "521-00005-r00-v02.32.01",
        "param_table": "549-00018-r00-v02.32.01",
        "envoy_serial_number": "121842012242",
        "energy": {
          "value": 232,
          "units": "Wh",
          "precision": 0
        },
        "grid_profile": "57227c50e4d7973ae602c4e6",
        "last_report_date": 1600427843
      },
      {
        "id": 1059689836,
        "serial_number": "686868727227",
        "model": "M215",
        "part_number": "800-00107-r01",
        "sku": "M215-60-2LL-S22-NA",
        "status": "normal",
        "power_produced": 20,
        "proc_load": "521-00005-r00-v02.32.01",
        "param_table": "549-00018-r00-v02.32.01",
        "envoy_serial_number": "121842012242",
        "energy": {
          "value": 120,
          "units": "Wh",
          "precision": 0
        },
        "grid_profile": "57227c50e4d7973ae602c4e6",
        "last_report_date": 1600427843
      }
    ]
  }
]
```

### Response Fields

The response is an array of Envoy groupings, each containing:

| Field | Type | Description |
|-------|------|-------------|
| `signal_strength` | integer | Signal strength of the Envoy |
| `micro_inverters` | array | List of microinverter objects reporting to this Envoy |
| `micro_inverters[].id` | integer | Unique device ID |
| `micro_inverters[].serial_number` | string | Microinverter serial number |
| `micro_inverters[].model` | string | Device model |
| `micro_inverters[].part_number` | string | Part number |
| `micro_inverters[].sku` | string | SKU identifier |
| `micro_inverters[].status` | string | Device status (e.g., `normal`) |
| `micro_inverters[].power_produced` | object\|integer | Current power produced. May be an object with `value`, `units`, `precision` or a raw integer in Watts. |
| `micro_inverters[].proc_load` | string | Processor load firmware version |
| `micro_inverters[].param_table` | string | Parameter table firmware version |
| `micro_inverters[].envoy_serial_number` | string | Serial number of the reporting Envoy |
| `micro_inverters[].energy` | object | Energy produced with `value`, `units`, `precision` |
| `micro_inverters[].grid_profile` | string | Grid profile identifier |
| `micro_inverters[].last_report_date` | integer | Unix epoch of last report |

### 401 — Authentication Error

```json
{
  "message": "Not Authorized",
  "details": "User is not authorized",
  "code": 401
}
```

### 405 — Method Not Allowed

```json
{
  "reason": "405",
  "message": ["Method not allowed"]
}
```

### 422 — Unprocessable Entity

```json
{
  "message": "Unprocessable Entity",
  "details": "Envoy serial number or Site id mandatory",
  "code": 422
}
```

### 429 — Too Many Requests

```json
{
  "message": "Too Many Requests",
  "details": "Usage limit exceeded for plan Kilowatt",
  "code": 429,
  "period": "minute",
  "period_start": 1623825660,
  "period_end": 1623825720,
  "limit": 5
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

- [Get System Devices](get-system-devices.md) — Full device list including gateways with serial numbers
- [Micro Telemetry](../devices/micro-telemetry.md) — Detailed telemetry for a single microinverter
- [Production Micro Telemetry](../production/telemetry-production-micro.md) — Aggregate telemetry for all production micros
- [Back to API Index](../README.md)
