# GET /api/v4/systems/{system_id}/alarms — Retrieve Alarms for a Site

Retrieves the alarms for a site. `start_time` is mandatory and cannot be older than 9 months from the current time. Maximum 1 week of data can be retrieved in a single call.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/alarms`

---

## Background

An **Alarm** is always tied to an **Event**. The relationship between them can be one-to-one or one-to-many:

- An alarm can be associated with **multiple events**.
- An event can be associated with **only one alarm**.

**Example:** If a site has multiple batteries and all fall below the State of Charge (SOC) threshold, individual events are created for each battery. If the low SOC condition persists across all batteries for the defined time period, a single alarm may be triggered for all of them.

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
| `cleared` | boolean | No | Filter by alarm status. Set to `true` to return cleared alarms. Defaults to `false` (active alarms only). |

---

## Responses

### 200 — List of Alarms

```json
{
  "alarms": [
    {
      "id": "1082701398",
      "cleared": false,
      "severity": 4,
      "events": [
        {
          "serial_number": "202241095486",
          "start_date": 1737750626,
          "end_date": 1737866703
        }
      ],
      "event_type_id": 28,
      "alarm_start_time": 1737750626,
      "alarm_end_time": null
    }
  ],
  "system_id": 701644354
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `alarms` | array | List of alarm objects |
| `alarms[].id` | string | Unique alarm ID |
| `alarms[].cleared` | boolean | Whether the alarm has been cleared |
| `alarms[].severity` | integer | Alarm severity level |
| `alarms[].event_type_id` | integer | ID of the associated event type. See [Get Event Types](get-event-types.md). |
| `alarms[].alarm_start_time` | integer | Unix epoch when the alarm started |
| `alarms[].alarm_end_time` | integer\|null | Unix epoch when the alarm ended (`null` if still active) |
| `alarms[].events` | array | Associated events that triggered this alarm |
| `alarms[].events[].serial_number` | string | Serial number of the device involved |
| `alarms[].events[].start_date` | integer | Unix epoch when the event started |
| `alarms[].events[].end_date` | integer | Unix epoch when the event ended |

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

- [Get System Events](get-system-events.md) — Retrieve events that may have triggered alarms
- [Get Open Events](get-open-events.md) — Retrieve currently open events
- [Get Event Types](get-event-types.md) — Look up event type descriptions and recommended actions
- [Back to API Index](../README.md)
