# GET /api/v4/systems/{system_id}/battery_lifetime — Battery Lifetime

Returns a daily time series of energy charged into the batteries and energy discharged from the batteries for a system over its lifetime. All measurements are in Watt-hours.

- The time series includes one entry for each day from `start_date` to `end_date` with no gaps.
- Trailing zeroes (e.g., `[909, 4970, 0, 0, 0]`) indicate no energy was charged/discharged for those days.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/battery_lifetime`

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

---

## Responses

### 200 — Battery Lifetime

```json
{
  "system_id": 66,
  "start_date": "2016-08-01",
  "discharge": [
    15422,
    15421,
    17118,
    18505,
    18511,
    18487
  ],
  "charge": [
    15422,
    15421,
    17118,
    18505,
    18511,
    18487
  ],
  "meta": {
    "status": "normal",
    "last_report_at": 1470087000,
    "last_energy_at": 1470086106,
    "operational_at": 1357023600
  }
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `start_date` | string | Start date of the time series (YYYY-MM-DD) |
| `discharge` | array of integers | Daily energy discharged from batteries in Wh (one entry per day) |
| `charge` | array of integers | Daily energy charged into batteries in Wh (one entry per day) |
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
  "details": "Requested date is in the future",
  "code": 422
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

- [Storage Meter Readings](storage-meter-readings.md) — Last known storage meter readings
- [Telemetry: Battery](telemetry-battery.md) — Battery telemetry intervals
- [Encharge Telemetry](../devices/encharge-telemetry.md) — Telemetry for a single Encharge battery
- [Back to API Index](../README.md)
