# GET /api/v4/systems/{system_id}/energy_lifetime — Energy Lifetime

Returns a daily time series of energy produced by the system over its lifetime. All measurements are in Watt-hours.

- The time series includes one entry for each day from `start_date` to `end_date` with no gaps.
- Trailing zeroes (e.g., `[909, 4970, 0, 0, 0]`) indicate no energy was reported for those days.
- If the system has a meter, the time series uses microinverter data until the first full day after the meter was installed, then switches to meter data. This is called the **merged time series**.
- The `meter_start_date` attribute indicates when meter measurements begin.
- Use `production=all` to retrieve the complete time series from both the meter and the microinverters separately.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/energy_lifetime`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. |

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `start_date` | string | No | Start date (YYYY-MM-DD). Defaults to the system's operational date. If earlier than operational date, operational date is used. |
| `end_date` | string | No | End date (YYYY-MM-DD). Defaults to yesterday if `start_date` is in the past; defaults to today if `start_date` is today. |
| `production` | string | No | When set to `"all"`, returns the merged time series plus separate microinverter and meter time series. Other values are ignored. |

---

## Responses

### 200 — Energy Lifetime (with `production=all`)

```json
{
  "system_id": 66,
  "start_date": "2013-01-01",
  "meter_start_date": "2013-01-04",
  "production": [
    15422,
    15421,
    17118,
    18505,
    18511,
    18487
  ],
  "micro_production": [
    15422,
    15421,
    17118,
    18513,
    18520,
    18494
  ],
  "meter_production": [
    0,
    0,
    11388,
    18505,
    18511,
    18487
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
| `start_date` | string | Start date of the time series (YYYY-MM-DD) |
| `meter_start_date` | string | Date when meter measurements begin to be used in the merged series |
| `production` | array of integers | Merged daily energy production in Wh (one entry per day) |
| `micro_production` | array of integers | Microinverter-measured daily energy in Wh (only with `production=all`) |
| `meter_production` | array of integers | Meter-measured daily energy in Wh (only with `production=all`) |
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
  "details": "Requested date range is invalid for this system",
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

- [Production Meter Readings](production-meter-readings.md) — Last known production meter readings
- [RGM Stats](rgm-stats.md) — Revenue-grade meter interval statistics
- [Consumption Lifetime](../consumption/consumption-lifetime.md) — Daily lifetime consumption time series
- [Back to API Index](../README.md)
