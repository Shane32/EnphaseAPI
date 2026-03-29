# GET /api/v4/systems/{system_id}/ev_charger/{serial_no}/lifetime — EV Charger Daily Energy

Retrieves the daily telemetry for a single EV Charger. The number of data points returned corresponds to the days spanned between `start_date` and `end_date`.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/ev_charger/{serial_no}/lifetime`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. Example: `698989834` |
| `serial_no` | string | Yes | Serial number of the EV Charger. Example: `190179855` |

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `start_date` | string | Yes | Start date in `YYYYMMDD` format. Example: `20240101` |
| `end_date` | string | No | End date in `YYYYMMDD` format. Defaults to yesterday. Example: `20240106` |

---

## Responses

### 200 — Daily EV Charger Energy

```json
{
  "system_id": 698989834,
  "start_date": "2024-01-01",
  "consumption": [
    3494,
    21929,
    0,
    0,
    0,
    0
  ]
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `start_date` | string | Start date of the time series (YYYY-MM-DD) |
| `consumption` | array of integers | Daily energy consumed by the EV charger in Wh (one entry per day) |

### 400 — Bad Request

```json
{
  "message": "Bad request",
  "code": "400",
  "details": ["Invalid system_id", "Invalid start_date/end_date"]
}
```

### 401 — Unauthorized

```json
{
  "message": "Not Authorized",
  "code": "401",
  "details": "User is not authorized"
}
```

### 403 — Forbidden

```json
{
  "message": "Forbidden",
  "code": "403",
  "details": "Not authorized to access this resource"
}
```

### 405 — Method Not Allowed

```json
{
  "message": ["Method not allowed"],
  "reason": "405"
}
```

### 500 — Internal Server Error

```json
{
  "message": "Internal Server Error",
  "code": "500",
  "details": "Internal Server Error"
}
```

---

## See Also

- [EV Charger Telemetry](get-ev-charger-telemetry.md) — EV charger telemetry at regular intervals
- [EV Charger Sessions](get-ev-charger-sessions.md) — Charger session history
- [EVSE Lifetime](../devices/evse-lifetime.md) — Daily EVSE charger time series (3rd party)
- [Back to API Index](../README.md)
