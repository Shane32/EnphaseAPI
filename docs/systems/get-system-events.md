# GET /api/v4/systems/{system_id}/events — Retrieve Events for a Site

Retrieves the events for a site. `start_time` is mandatory and cannot be older than 9 months from the current time. Maximum 1 week of data can be retrieved in a single call.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/events`

---

## Background

An **Event** is triggered when a site or device meets a predefined set of conditions called an **Event type**. Events can be triggered at both site and device level. Each event is associated with an event type.

Many event types come with predefined escalation criteria. When an event meets these configurations (e.g., status remains "Open" beyond a time threshold), it can trigger an **Alarm**.

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. |

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `start_time` | integer | Yes | Start time in Unix epoch format. Cannot be older than 9 months from now. |
| `end_time` | integer | No | End time in Unix epoch format. Defaults to `min(start_time + 1 day, current time)`. |

---

## Responses

### 200 — List of Events

```json
{
  "events": [
    {
      "status": "Closed",
      "event_type_id": 28,
      "event_start_time": 1740213328,
      "event_end_time": 1740373425,
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
| `events` | array | List of event objects |
| `events[].status` | string | Event status (e.g., `Open`, `Closed`) |
| `events[].event_type_id` | integer | ID of the event type. See [Get Event Types](get-event-types.md). |
| `events[].event_start_time` | integer | Unix epoch when the event started |
| `events[].event_end_time` | integer\|null | Unix epoch when the event ended (`null` if still open) |
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

### 422 — Unprocessable Entity

```json
{
  "message": "Unprocessable Entity",
  "details": "start_time is required",
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

- [Get Open Events](get-open-events.md) — Retrieve only currently open events
- [Get System Alarms](get-system-alarms.md) — Retrieve alarms triggered by events
- [Get Event Types](get-event-types.md) — Look up event type descriptions and recommended actions
- [Back to API Index](../README.md)
