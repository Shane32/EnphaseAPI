# GET /api/v4/systems/{system_id}/devices/encharges/{serial_no}/telemetry — Encharge Telemetry

Retrieves telemetry for a single Encharge (IQ Battery) device.

- If no `start_at` is specified, defaults to midnight today in the system's timezone.
- If `start_at` is earlier than the system's first reported date, midnight of the first reported date is used.
- `end_at` is calculated as the minimum of the current time and `start_at + granularity`.
- The requested start date must be within 2 years from the current date.
- An empty list is returned if `last interval < requested start time < current time`.

**Granularity behavior (15-minute intervals):**
- `15mins`: maximum 1 interval
- `day`: maximum 96 intervals
- `week`: maximum 672 intervals

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/devices/encharges/{serial_no}/telemetry`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. |
| `serial_no` | string | Yes | Serial number of the Encharge battery. |

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `start_at` | integer | No | Start time in Unix epoch format. Alternatively use `start_date` (YYYY-MM-DD). Defaults to midnight today in the system's timezone. |
| `granularity` | string | No | Granularity of telemetry data. One of `5mins`, `15mins`, `day`, `week`. Default is `day`. |

---

## Responses

### 200 — Telemetry for the Encharge

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
      "end_at": 1384122700,
      "charge": {
        "enwh": 40
      },
      "discharge": {
        "enwh": 0
      },
      "soc": {
        "percent": 25
      }
    }
  ],
  "last_reported_time": 1650349170,
  "last_reported_soc": "99%"
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `serial_number` | string | The Encharge serial number |
| `granularity` | string | Granularity of the returned data |
| `total_devices` | integer | Always `1` for a single device |
| `start_at` | integer | Unix epoch of the start of the period (or `start_date` if that parameter was used) |
| `end_at` | integer | Unix epoch of the end of the period (or `end_date` if that parameter was used) |
| `items` | string | Always `"intervals"` |
| `intervals` | array | List of telemetry interval objects |
| `intervals[].end_at` | integer | Unix epoch of interval end |
| `intervals[].charge.enwh` | integer | Energy charged into the battery in Wh during this interval |
| `intervals[].discharge.enwh` | integer | Energy discharged from the battery in Wh during this interval |
| `intervals[].soc.percent` | integer | State of charge percentage at end of interval |
| `last_reported_time` | integer | Unix epoch of the last reported time |
| `last_reported_soc` | string | Last reported state of charge as a percentage string |

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
  "details": "Encharge not found for {:id=>\"1\"}",
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

- [Telemetry: Battery](../consumption/telemetry-battery.md) — Aggregate battery telemetry for all batteries
- [Battery Lifetime](../consumption/battery-lifetime.md) — Daily battery charge/discharge time series
- [Get System Devices](../systems/get-system-devices.md) — List all devices including Encharges
- [Back to API Index](../README.md)
