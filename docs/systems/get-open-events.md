# GET /api/v4/systems/{system_id}/open_events — Retrieve Open Events for a Site

Retrieves all currently open events for a site.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/open_events`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. |

---

## Responses

### 200 — List of Open Events

```json
{
  "events": [
    {
      "status": "open",
      "event_type_id": 28,
      "event_start_time": 1740213328,
      "event_end_time": null,
      "serial_number": "202241095486"
    }
  ],
  "system_id": 701644354
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `events` | array | List of open event objects |
| `events[].status` | string | Always `"open"` for this endpoint |
| `events[].event_type_id` | integer | ID of the event type. See [Get Event Types](get-event-types.md). |
| `events[].event_start_time` | integer | Unix epoch when the event started |
| `events[].event_end_time` | null | Always `null` since the event is still open |
| `events[].serial_number` | string | Serial number of the device that triggered the event |

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

- [Get System Events](get-system-events.md) — Retrieve all events (open and closed) within a time range
- [Get System Alarms](get-system-alarms.md) — Retrieve alarms for a site
- [Get Event Types](get-event-types.md) — Look up event type descriptions and recommended actions
- [Back to API Index](../README.md)
