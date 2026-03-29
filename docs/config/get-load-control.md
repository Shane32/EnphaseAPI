# GET /api/v4/systems/config/{system_id}/load_control — Get Load Control Settings

Returns the current load control settings of a system.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/config/{system_id}/load_control`

---

## Background

Load control allows the system to manage non-essential loads (e.g., air conditioning units) based on battery state of charge (SOC). Each load control channel can be configured with SOC thresholds and operating schedules.

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | string | Yes | The unique numeric ID of the system. |

---

## Responses

### 200 — Load Control Settings

```json
{
  "system_id": 1932237,
  "load_control_data": [
    {
      "name": "NC1",
      "load_name": "Downstairs A/C",
      "owner_can_override": true,
      "mode": "Basic",
      "soc_low": 30,
      "soc_high": 50,
      "status": "enabled",
      "essential_start_time": 32400,
      "essential_end_time": 57600
    },
    {
      "name": "NC2",
      "load_name": "Upstairs A/C",
      "owner_can_override": true,
      "mode": "Basic",
      "soc_low": 30,
      "soc_high": 50,
      "status": "enabled",
      "essential_start_time": 32400,
      "essential_end_time": 57600
    }
  ]
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `load_control_data` | array | List of load control channel configurations |
| `load_control_data[].name` | string | Channel identifier (e.g., `NC1`, `NC2`) |
| `load_control_data[].load_name` | string | Human-readable name for the load |
| `load_control_data[].owner_can_override` | boolean | Whether the system owner can override the load control |
| `load_control_data[].mode` | string | Control mode (e.g., `Basic`) |
| `load_control_data[].soc_low` | integer | Lower SOC threshold percentage — load is shed below this level |
| `load_control_data[].soc_high` | integer | Upper SOC threshold percentage — load is restored above this level |
| `load_control_data[].status` | string | Whether this channel is `enabled` or `disabled` |
| `load_control_data[].essential_start_time` | integer | Start of essential period in seconds from midnight |
| `load_control_data[].essential_end_time` | integer | End of essential period in seconds from midnight |

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
- [Get Grid Status](get-grid-status.md) — Get current grid status
- [Get Storm Guard](get-storm-guard.md) — Get storm guard settings
- [Back to API Index](../README.md)
