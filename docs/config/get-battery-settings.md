# GET /api/v4/systems/config/{system_id}/battery_settings — Get Battery Settings

Returns the current battery settings of a system.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/config/{system_id}/battery_settings`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | string | Yes | The unique numeric ID of the system. |

---

## Responses

### 200 — Battery Settings

```json
{
  "system_id": 1765,
  "battery_mode": "Self - Consumption",
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
| `battery_mode` | string | Current battery operating mode (e.g., `Self - Consumption`, `Savings Mode`) |
| `reserve_soc` | integer | Reserve state of charge percentage (0–100) |
| `energy_independence` | string | Energy independence setting (`enabled` or `disabled`) |
| `charge_from_grid` | string | Whether charging from the grid is allowed (`enabled` or `disabled`) |
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

- [Update Battery Settings](put-battery-settings.md) — Update the current battery settings
- [Get Storm Guard](get-storm-guard.md) — Get storm guard settings
- [Get Grid Status](get-grid-status.md) — Get current grid status
- [Back to API Index](../README.md)
