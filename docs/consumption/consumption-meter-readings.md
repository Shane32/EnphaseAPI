# GET /api/v4/systems/{system_id}/consumption_meter_readings — Consumption Meter Readings

Returns the last known reading of each consumption meter on the system as of the requested time, regardless of whether the meter is currently in service or retired.

`read_at` is the time at which the reading was taken, and is always less than or equal to the requested `end_at`. Commonly, the reading will be within 30 minutes of the requested `end_at`. However, larger deltas can occur and do not necessarily mean there is a problem with the meter or the system.

- Systems configured to report infrequently can show large deltas on all meters, especially when `end_at` is close to the current time.
- Meters that have been retired from a system will show an `end_at` that doesn't change, and that eventually is far away from the current time.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/consumption_meter_readings`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. |

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `end_at` | integer | No | End of reporting period in Unix epoch time. Defaults to the time of the request. If later than the last reported interval, the response ends with the last reported interval. |

---

## Responses

### 200 — Consumption Meter Readings

```json
{
  "system_id": 66,
  "meter_readings": [
    {
      "serial_num": "123123123123EIM2",
      "value": 6180635,
      "read_at": 1473901200
    }
  ],
  "meta": {
    "status": "normal",
    "last_report_at": 1473902079,
    "last_energy_at": 1473901200,
    "operational_at": 1357023600
  }
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `meter_readings` | array | List of meter reading objects |
| `meter_readings[].serial_num` | string | Meter serial number |
| `meter_readings[].value` | integer | Meter reading value in Wh |
| `meter_readings[].read_at` | integer | Unix epoch when the reading was taken |
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
  "message": "Not Authorized",
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
  "details": "Failed to parse date 1613543106",
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

- [Production Meter Readings](../production/production-meter-readings.md) — Last known production meter readings
- [Storage Meter Readings](storage-meter-readings.md) — Last known storage meter readings
- [Consumption Lifetime](consumption-lifetime.md) — Daily lifetime consumption time series
- [Back to API Index](../README.md)
