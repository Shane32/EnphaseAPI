# GET /api/v4/systems/{system_id}/ev_charger/{serial_no}/sessions — Charger Session History

Get the list of charging sessions for a single EV Charger.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/ev_charger/{serial_no}/sessions`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. Example: `698989834` |
| `serial_no` | string | Yes | Serial number of the EV Charger. Example: `202320010308` |

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `offset` | integer | No | Pagination offset. Default: `0`. |
| `limit` | integer | No | Maximum number of sessions to return. Maximum: `100`. Default: `20`. |

---

## Responses

### 200 — Charger Sessions List

```json
{
  "count": 1,
  "system_id": 698989834,
  "sessions": [
    {
      "start_time": 1700059683,
      "end_time": 1700071180,
      "duration": 11497,
      "energy_added": 14.83,
      "charge_time": 7080,
      "miles_added": 1.2,
      "cost": 0.5
    }
  ]
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `count` | integer | Total number of sessions returned |
| `system_id` | integer | The system ID |
| `sessions` | array | List of charging session objects |
| `sessions[].start_time` | integer | Unix epoch when the session started |
| `sessions[].end_time` | integer | Unix epoch when the session ended |
| `sessions[].duration` | integer | Total session duration in seconds |
| `sessions[].energy_added` | number | Energy added to the vehicle in kWh |
| `sessions[].charge_time` | integer | Active charging time in seconds |
| `sessions[].miles_added` | number | Estimated miles added to the vehicle |
| `sessions[].cost` | number | Estimated cost of the charging session |

### 400 — Bad Request

```json
{
  "message": "Bad request",
  "code": "400",
  "details": ["Invalid system_id or serial_no", "Invalid offset/limit"]
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

- [EV Charger Devices](get-ev-charger-devices.md) — Fetch active EV chargers
- [EV Charger Events](get-ev-charger-events.md) — Fetch EV charger events
- [EV Charger Lifetime](get-ev-charger-lifetime.md) — Daily energy time series
- [Back to API Index](../README.md)
