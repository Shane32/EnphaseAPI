# GET /api/v4/systems/{system_id}/ev_charger/events — Fetch EV Charger Events

Fetches all events related to EV Chargers for a system.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/ev_charger/events`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. Example: `701052773` |

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `offset` | integer | No | Pagination offset. Default: `0`. |
| `serial_num` | string | No | Filter events by EV charger serial number. Example: `202312100006` |
| `limit` | integer | No | Maximum number of events to return. Maximum: `100`. Default: `20`. |

---

## Responses

### 200 — List of Events

```json
{
  "count": 1,
  "events": [
    {
      "status": "Info",
      "triggered_date": 1705399759,
      "cleared_date": 1705399759,
      "details": "Charging started on IQ EV Charger (SNo. 202312100006)."
    }
  ],
  "system_id": 701052773
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `count` | integer | Total number of events returned |
| `system_id` | integer | The system ID |
| `events` | array | List of event objects |
| `events[].status` | string | Event status (e.g., `Info`, `Warning`, `Error`) |
| `events[].triggered_date` | integer | Unix epoch when the event was triggered |
| `events[].cleared_date` | integer | Unix epoch when the event was cleared |
| `events[].details` | string | Human-readable description of the event |

### 400 — Bad Request

```json
{
  "message": "Bad request",
  "code": "400",
  "details": ["Invalid system_id", "Invalid offset/limit"]
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
- [EV Charger Sessions](get-ev-charger-sessions.md) — Charger session history
- [Get System Events](../systems/get-system-events.md) — System-level events
- [Back to API Index](../README.md)
