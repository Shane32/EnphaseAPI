# GET /api/v4/systems/{system_id}/energy_import_lifetime — Energy Import Lifetime

Returns a daily time series of energy imported from the grid by the system over its lifetime. All measurements are in Watt-hours.

- The time series includes one entry for each day from `start_date` to `end_date` with no gaps.
- Trailing zeroes (e.g., `[909, 4970, 0, 0, 0]`) indicate no energy was imported for those days.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/energy_import_lifetime`

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

### 200 — Energy Import Lifetime

```json
{
  "system_id": 66,
  "start_date": "2016-08-01",
  "import": [
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
| `import` | array of integers | Daily energy imported from the grid in Wh (one entry per day) |
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

- [Energy Export Lifetime](energy-export-lifetime.md) — Daily lifetime grid export time series
- [Energy Import Telemetry](energy-import-telemetry.md) — Grid import telemetry intervals
- [Consumption Lifetime](consumption-lifetime.md) — Daily lifetime consumption time series
- [Back to API Index](../README.md)
