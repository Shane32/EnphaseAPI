# GET /api/v4/systems/config/{system_id}/grid_status — Get Grid Status

Returns the current grid status of a system.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/config/{system_id}/grid_status`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | string | Yes | The unique numeric ID of the system. |

---

## Responses

### 200 — Grid Status

```json
{
  "system_id": 1765,
  "grid_state": "On Grid",
  "last_report_date": 1676029267
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `grid_state` | string | Current grid state (e.g., `On Grid`, `Off Grid`) |
| `last_report_date` | integer | Unix epoch of the last grid status report |

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
  "details": "grid_state not available as the site has not reported yet",
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

- [Get Battery Settings](get-battery-settings.md) — Get current battery settings
- [Get Storm Guard](get-storm-guard.md) — Get storm guard settings
- [Get Load Control](get-load-control.md) — Get load control settings
- [Live Data](../streaming/live-data.md) — Real-time grid status via streaming
- [Back to API Index](../README.md)
