# GET /api/v4/systems/{system_id}/devices/micros/{serial_no}/telemetry — Micro Telemetry

Retrieves telemetry for a single microinverter (PCU).

- If no `start_at` is specified, defaults to midnight today in the system's timezone.
- If `start_at` is earlier than the system's first reported date, midnight of the first reported date is used.
- `end_at` is calculated as the minimum of the current time and `start_at + granularity`.
- The requested start date must be within 2 years from the current date.
- An empty list is returned if `last interval < requested start time < current time`.

**Granularity behavior (5-minute intervals):**
- `15mins`: maximum 3 intervals
- `day`: maximum 288 intervals

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/devices/micros/{serial_no}/telemetry`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. |
| `serial_no` | string | Yes | Serial number of the individual solar microinverter. |

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `start_at` | integer | No | Start time in Unix epoch format. Alternatively use `start_date` (YYYY-MM-DD). Defaults to midnight today in the system's timezone. |
| `granularity` | string | No | Granularity of telemetry data. One of `5mins`, `15mins`, `day`, `week`. Default is `day`. |

---

## Responses

### 200 — Telemetry for Micro

```json
{
  "system_id": 1765,
  "serial_number": "12345",
  "granularity": "day",
  "total_devices": 1,
  "start_at": 1496526300,
  "end_at": 1496529300,
  "items": "intervals",
  "intervals": [
    {
      "end_at": 1496526300,
      "powr": 30,
      "enwh": 40
    },
    {
      "end_at": 1496526600,
      "powr": 20,
      "enwh": 40
    }
  ]
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `serial_number` | string | The microinverter serial number |
| `granularity` | string | Granularity of the returned data |
| `total_devices` | integer | Always `1` for a single device |
| `start_at` | integer | Unix epoch of the start of the period (or `start_date` if that parameter was used) |
| `end_at` | integer | Unix epoch of the end of the period (or `end_date` if that parameter was used) |
| `items` | string | Always `"intervals"` |
| `intervals` | array | List of telemetry interval objects |
| `intervals[].end_at` | integer | Unix epoch of interval end |
| `intervals[].powr` | integer | Average power in Watts during this interval |
| `intervals[].enwh` | integer | Energy produced in Wh during this interval |

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
  "details": "Micro not found for {:id=>\"1\"}",
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

- [Telemetry: Production Micro](../production/telemetry-production-micro.md) — Aggregate telemetry for all production micros
- [Inverters Summary](../systems/inverters-summary.md) — Microinverter summary by Envoy or site
- [Get System Devices](../systems/get-system-devices.md) — List all devices including microinverters
- [Back to API Index](../README.md)
