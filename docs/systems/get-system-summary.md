# GET /api/v4/systems/{system_id}/summary — Retrieve System Summary

Returns system summary based on the specified system ID and summary date. For historical requests (i.e., summary date is in the past), the fields `modules`, `size_w`, and `current_power` are returned as zero.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/summary`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. |

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `summary_date` | string | No | Requested date for the system's summary data (YYYY-MM-DD). Defaults to the current local date of the system. |

---

## Responses

### 200 — System Summary Fetched

```json
{
  "system_id": 698910067,
  "current_power": 0,
  "energy_lifetime": 0,
  "energy_today": 0,
  "last_interval_end_at": 1557400231,
  "last_report_at": 1557400231,
  "modules": 5,
  "operational_at": null,
  "size_w": 1250,
  "nmi": "1213141516",
  "source": "meter",
  "status": "normal",
  "summary_date": "2019-05-12",
  "battery_charge_w": 1280,
  "battery_discharge_w": 1280,
  "battery_capacity_wh": 3360,
  "evse_power": [
    {
      "21458935": {
        "evse_max_charge_w": 440,
        "evse_min_charge_w": 2230
      }
    },
    {
      "12785966": {
        "evse_max_charge_w": 530,
        "evse_min_charge_w": 1200
      }
    }
  ]
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | Unique numeric ID of the system |
| `current_power` | integer | Current power output in W (0 for historical requests) |
| `energy_lifetime` | integer | Lifetime energy produced in Wh |
| `energy_today` | integer | Energy produced today in Wh |
| `last_interval_end_at` | integer | Unix epoch of the last interval end |
| `last_report_at` | integer | Unix epoch of the last report |
| `modules` | integer | Number of modules (0 for historical requests) |
| `operational_at` | integer\|null | Unix epoch when system became operational |
| `size_w` | integer | System size in W (0 for historical requests) |
| `nmi` | string | National Meter Identifier |
| `source` | string | Data source (e.g., `meter`, `microinverter`) |
| `status` | string | System status |
| `summary_date` | string | Date of the summary (YYYY-MM-DD) |
| `battery_charge_w` | integer | Battery charge power in W |
| `battery_discharge_w` | integer | Battery discharge power in W |
| `battery_capacity_wh` | integer | Total battery capacity in Wh |
| `evse_power` | array | EVSE power data per device (keyed by device ID) |

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

- [Get System by ID](get-system-by-id.md) — Full system details
- [Get System Devices](get-system-devices.md) — Devices attached to a system
- [Latest Telemetry](../consumption/latest-telemetry.md) — Real-time power readings
- [Back to API Index](../README.md)
