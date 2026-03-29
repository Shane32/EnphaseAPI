# GET /api/v4/systems/{system_id}/rgm_stats — RGM Stats

Returns performance statistics as measured by the revenue-grade meters (RGM) installed on the specified system.

- If the total duration requested is more than one week, returns one week of intervals.
- Intervals are 15 minutes in length and start at the top of the hour.
- Requests for times that do not fall on the 15-minute marks are rounded down (e.g., 08:01, 08:08, 08:11, or 08:14 are all treated as 08:00).
- Intervals are listed by their end times in Unix epoch format.
- The requested date range in one API call cannot be more than 7 days.
- The requested `start_at` must be within 2 years from the current time.
- If `start_at` is earlier than the system's first reported date, midnight of the first reported date is used.
- An empty list is returned if `last interval < requested start time < current time`.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/rgm_stats`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The numeric ID of the system. |

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `start_at` | integer | No | Start of period in Unix epoch time. Defaults to midnight today in the system's timezone. |
| `end_at` | integer | No | End of reporting period in Unix epoch time. Defaults to the earlier of the current time or `start_at + 1 week`. |

---

## Responses

### 200 — RGM Stats

```json
{
  "system_id": 66,
  "total_devices": 2,
  "meta": {
    "status": "normal",
    "last_report_at": 1470087000,
    "last_energy_at": 1470086106,
    "operational_at": 1357023600
  },
  "intervals": [
    {
      "end_at": 1384122700,
      "wh_del": 50,
      "devices_reporting": 2
    },
    {
      "end_at": 1384123600,
      "wh_del": 100,
      "devices_reporting": 2
    }
  ],
  "meter_intervals": [
    {
      "meter_serial_number": "1218676784",
      "envoy_serial_number": "1218347675",
      "intervals": [
        {
          "channel": 1,
          "end_at": 1384122700,
          "wh_del": 30,
          "curr_w": 120
        },
        {
          "channel": 1,
          "end_at": 1384123600,
          "wh_del": 50,
          "curr_w": 200
        }
      ]
    }
  ]
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `total_devices` | integer | Total number of RGM devices |
| `meta.status` | string | System status |
| `meta.last_report_at` | integer | Unix epoch of last system report |
| `meta.last_energy_at` | integer | Unix epoch of last energy report |
| `meta.operational_at` | integer | Unix epoch when system became operational |
| `intervals` | array | Aggregate 15-minute intervals across all meters |
| `intervals[].end_at` | integer | Unix epoch of interval end |
| `intervals[].wh_del` | integer | Watt-hours delivered in this interval |
| `intervals[].devices_reporting` | integer | Number of devices reporting in this interval |
| `meter_intervals` | array | Per-meter interval data |
| `meter_intervals[].meter_serial_number` | string | Meter serial number |
| `meter_intervals[].envoy_serial_number` | string | Envoy serial number |
| `meter_intervals[].intervals` | array | Intervals for this meter |
| `meter_intervals[].intervals[].channel` | integer | Meter channel number |
| `meter_intervals[].intervals[].end_at` | integer | Unix epoch of interval end |
| `meter_intervals[].intervals[].wh_del` | integer | Watt-hours delivered |
| `meter_intervals[].intervals[].curr_w` | integer | Current power in Watts |

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

### 500 — Data Temporarily Unavailable

```json
{
  "errorCode": 7,
  "errorMessages": ["Data is temporarily unavailable"]
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
- [Energy Lifetime](energy-lifetime.md) — Daily lifetime energy production time series
- [Telemetry: Production Meter](telemetry-production-meter.md) — Production meter telemetry intervals
- [Back to API Index](../README.md)
