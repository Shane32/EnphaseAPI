# GET /api/v4/systems/{system_id}/{serial_no}/evse_lifetime — EVSE Lifetime

Retrieves daily time-series telemetry data of the 3rd party EVSE charger. The number of data points returned corresponds to the days spanned between `start_date` and `end_date`.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/{serial_no}/evse_lifetime`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. |
| `serial_no` | string | Yes | Serial number of the EV Charger. |

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `start_date` | string | No | Start date (YYYY-MM-DD). Defaults to the system's operational date. If earlier than operational date, operational date is used. |
| `end_date` | string | No | End date (YYYY-MM-DD). Defaults to today. |

---

## Responses

### 200 — EVSE Lifetime Telemetry

```json
{
  "system_id": 698905955,
  "start_date": "2024-11-22",
  "end_date": "2024-11-28",
  "consumption": [
    40,
    35,
    40,
    20,
    15,
    10,
    2
  ]
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `start_date` | string | Start date of the time series (YYYY-MM-DD) |
| `end_date` | string | End date of the time series (YYYY-MM-DD) |
| `consumption` | array of integers | Daily energy consumed by the EVSE in Wh (one entry per day) |

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

- [EVSE Telemetry](evse-telemetry.md) — EVSE charger telemetry at regular intervals
- [EV Charger Lifetime](../ev-charger/get-ev-charger-lifetime.md) — Daily energy for IQ EV Charger
- [HP Lifetime](hp-lifetime.md) — Daily heat pump time series
- [Back to API Index](../README.md)
