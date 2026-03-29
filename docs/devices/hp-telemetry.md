# GET /api/v4/systems/{system_id}/hp_telemetry — Heat Pump Telemetry

Retrieves energy consumed by the Heat Pump (HP) in regular intervals.

- If no `start_at` is specified, defaults to midnight today in the system's timezone.
- If `start_at` is earlier than the system's first reported date, midnight of the first reported date is used.
- `end_at` is calculated as the minimum of the current time and `start_at + granularity`.
- The requested start date must be within 2 years from the current date.

**Granularity behavior (15-minute intervals):**
- `15mins`: maximum 1 interval
- `day`: maximum 96 intervals

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/hp_telemetry`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. |

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `start_at` | integer | No | Start time in Unix epoch format. |
| `start_date` | string | No | Start date in YYYY-MM-DD format (alternative to `start_at`). |
| `granularity` | string | No | Granularity of telemetry data. One of `week`, `day`, `15mins`, `5mins`. Default is `day`. |
| `interval_duration` | string | No | Size of the interval. Can be `5mins` or `15mins`. Site must be configured for 5-minute telemetry for `5mins` to be supported. |

---

## Responses

### 200 — HP Telemetry in Intervals

```json
{
  "system_id": 698905955,
  "granularity": "day",
  "interval_duration": "5mins",
  "start_at": 1496526300,
  "end_at": 1496528100,
  "items": "intervals",
  "intervals": [
    {
      "end_at": 1496526600,
      "wh_consumed": 40
    },
    {
      "end_at": 1496526900,
      "wh_consumed": 40
    },
    {
      "end_at": 1496527200,
      "wh_consumed": 40
    }
  ]
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `granularity` | string | Granularity of the returned data |
| `interval_duration` | string | Duration of each interval (`5mins` or `15mins`) |
| `start_at` | integer | Unix epoch of the start of the period |
| `end_at` | integer | Unix epoch of the end of the period |
| `items` | string | Always `"intervals"` |
| `intervals` | array | List of telemetry interval objects |
| `intervals[].end_at` | integer | Unix epoch of interval end |
| `intervals[].wh_consumed` | integer | Energy consumed by the heat pump in Wh during this interval |

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
  "details": "System not found for {:id=>\"70118390114\"}",
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
  "details": "Invalid request because of 'Invalid date/time range'",
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

- [HP Lifetime](hp-lifetime.md) — Daily heat pump time series
- [EVSE Telemetry](evse-telemetry.md) — EVSE charger telemetry intervals
- [Latest Telemetry](../consumption/latest-telemetry.md) — Latest real-time power readings including HP operational mode
- [Back to API Index](../README.md)
