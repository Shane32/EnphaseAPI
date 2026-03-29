# GET /api/v4/systems/config/{system_id}/storm_guard — Get Storm Guard Settings

Returns the current storm guard settings of a system.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/config/{system_id}/storm_guard`

---

## Background

Storm Guard is a feature that automatically charges the battery to full capacity when a storm is detected in the area. This ensures maximum backup power is available before a potential outage.

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | string | Yes | The unique numeric ID of the system. |

---

## Responses

### 200 — Storm Guard Settings

```json
{
  "system_id": 1765,
  "storm_guard_status": "enabled",
  "storm_alert": "false"
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `storm_guard_status` | string | Whether storm guard is `enabled` or `disabled` |
| `storm_alert` | string | Whether a storm alert is currently active (`"true"` or `"false"`) |

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
  "details": "System doesn't have encharges",
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

- [Get Battery Settings](get-battery-settings.md) — Get current battery settings
- [Update Battery Settings](put-battery-settings.md) — Update battery settings
- [Get Grid Status](get-grid-status.md) — Get current grid status
- [Back to API Index](../README.md)
