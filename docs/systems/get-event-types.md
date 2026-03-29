# GET /api/v4/systems/event_types — Retrieve Event Types

Retrieves the list of all available event types, including the `event_type_id`, `event_description`, and `recommended_action`. If an `event_type_id` is passed, this endpoint returns the detail of that specific event type.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/event_types`

---

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `event_type_id` | integer | No | The unique numeric ID of the event type. If omitted, all event types are returned. |

---

## Responses

### 200 — List of Event Types

```json
{
  "event_types": [
    {
      "event_type_id": 28,
      "event_type_key": "envoy_no_report",
      "stateful": true,
      "event_name": "Gateway not reporting",
      "event_description": "The broadband Internet connection that the Enphase gateway uses to communicate to the Enphase servers is experiencing a problem.",
      "recommended_action": "Check that your gateway and Internet router are plugged in and that the site's Internet service is not experiencing an outage."
    },
    {
      "event_type_id": 4781,
      "event_type_key": "acb_sleeping",
      "stateful": false,
      "event_name": "AC Battery Sleeping",
      "event_description": "AC Battery has entered the target state of charge band. Cleared when the battery exits the state of charge target, or sleep mode is removed.",
      "recommended_action": "No action is required."
    }
  ]
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `event_types` | array | List of event type objects |
| `event_types[].event_type_id` | integer | Unique numeric ID of the event type |
| `event_types[].event_type_key` | string | Machine-readable key for the event type |
| `event_types[].stateful` | boolean | Whether the event type tracks open/closed state |
| `event_types[].event_name` | string | Human-readable name of the event type |
| `event_types[].event_description` | string | Description of what causes this event |
| `event_types[].recommended_action` | string | Suggested action to resolve the event |

### 401 — Authentication Error

```json
{
  "message": "Not Authorized",
  "details": "User is not authorized",
  "code": 401
}
```

### 404 — Not Found

```json
{
  "message": "Not Found",
  "details": "Event type not found for {:id=>\"12345\"}",
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

- [Get System Events](get-system-events.md) — Retrieve events for a site (references `event_type_id`)
- [Get System Alarms](get-system-alarms.md) — Retrieve alarms for a site (references `event_type_id`)
- [Get Open Events](get-open-events.md) — Retrieve currently open events
- [Back to API Index](../README.md)
