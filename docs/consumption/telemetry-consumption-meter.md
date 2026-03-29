# GET /api/v4/systems/{system_id}/telemetry/consumption_meter — Telemetry: Consumption Meters

Retrieves telemetry for all the consumption meters of a system.

- If `start_at` is earlier than the system's first reported date, midnight of the first reported date is used.
- `end_at` is calculated as the minimum of the current time and `start_at + granularity`.
- If no `start_at` is specified, defaults to midnight today in the system's timezone.
- The requested start date must be within 2 years from the current date.
- An empty list is returned if `last interval < requested start time < current time`.

**Granularity behavior (15-minute intervals):**
- `15mins`: maximum 1 interval
- `day`: maximum 96 intervals
- `week`: maximum 672 intervals

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/telemetry/consumption_meter`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. |

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `start_at` | integer | No | Start time in Unix epoch format. Alternatively use `start_date` (YYYY-MM-DD). Defaults to midnight today in the system's timezone. |
| `granularity` | string | No | Granularity of telemetry data. One of `5mins`, `15mins`, `day`, `week`. Default is `day`. |

---

## Responses

### 200 — Telemetry for All Consumption Meters

```json
{
  "system_id": 698905955,
  "granularity": "day",
  "total_devices": 9,
  "start_at": 1496526300,
  "end_at": 1496529300,
  "items": "intervals",
  "intervals": [
    {
      "end_at": 1384122700,
      "devices_reporting": 1,
      "enwh": 40
    },
    {
      "end_at": 1384123600,
      "devices_reporting": 1,
      "enwh": 40
    }
  ],
  "meta": {
    "status": "normal",
    "last_report_at": 1445619615,
    "last_energy_at": 1445619033,
    "operational_at": 1357023600
  }
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `granularity` | string | Granularity of the returned data |
| `total_devices` | integer | Total number of consumption meter devices |
| `start_at` | integer | Unix epoch of the start of the period (or `start_date` if that parameter was used) |
| `end_at` | integer | Unix epoch of the end of the period (or `end_date` if that parameter was used) |
| `items` | string | Always `"intervals"` |
| `intervals` | array | List of telemetry interval objects |
| `intervals[].end_at` | integer | Unix epoch of interval end |
| `intervals[].devices_reporting` | integer | Number of devices reporting in this interval |
| `intervals[].enwh` | integer | Energy consumed in Wh during this interval |
| `meta.status` | string | System status |
| `meta.last_report_at` | integer | Unix epoch of last system report |
| `meta.last_energy_at` | integer | Unix epoch of last energy report |
| `meta.operational_at` | integer | Unix epoch when system became operational |

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

### 422 — Unprocessable Entity

```json
{
  "message": "Unprocessable Entity",
  "details": "Invalid request because of 'Requested date range is invalid for this system.'",
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

- [Consumption Lifetime](consumption-lifetime.md) — Daily lifetime consumption time series
- [Consumption Meter Readings](consumption-meter-readings.md) — Last known consumption meter readings
- [Telemetry: Production Meter](../production/telemetry-production-meter.md) — Production meter telemetry
- [Back to API Index](../README.md)
