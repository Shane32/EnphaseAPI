# PUT /api/v4/systems/config/{system_id}/battery_settings — Update Battery Settings

Updates the current battery settings of a system.

**Method:** `PUT`  
**Endpoint:** `/api/v4/systems/config/{system_id}/battery_settings`  
**Content-Type:** `application/json`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | string | Yes | The unique numeric ID of the system. |

---

## Request Body

```json
{
  "battery_mode": "string",
  "reserve_soc": 0,
  "energy_independence": "string"
}
```

### Request Body Fields

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `battery_mode` | string | No | Battery operating mode (e.g., `self-consumption`, `savings-mode`) |
| `reserve_soc` | integer | No | Reserve state of charge percentage (0–100) |
| `energy_independence` | string | No | Energy independence setting (`enabled` or `disabled`) |

---

## Responses

### 200 — Battery Settings Updated

```json
{
  "system_id": 1765,
  "battery_mode": "self-consumption",
  "reserve_soc": 95,
  "energy_independence": "enabled",
  "charge_from_grid": "disabled",
  "battery_shutdown_level": 13
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `battery_mode` | string | Updated battery operating mode |
| `reserve_soc` | integer | Updated reserve state of charge percentage |
| `energy_independence` | string | Updated energy independence setting |
| `charge_from_grid` | string | Whether charging from the grid is allowed |
| `battery_shutdown_level` | integer | Battery shutdown level percentage |

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
  "details": "System doesn't have encharge",
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

- [Get Battery Settings](get-battery-settings.md) — Retrieve the current battery settings
- [Get Storm Guard](get-storm-guard.md) — Get storm guard settings
- [Get Grid Status](get-grid-status.md) — Get current grid status
- [Back to API Index](../README.md)
