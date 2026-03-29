# GET /api/v4/systems/{system_id}/ev_charger/{serial_no}/schedules — Get EV Charger Schedules

Fetches all EV Charger Schedules for a device by system ID and serial number.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/ev_charger/{serial_no}/schedules`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. Example: `698989834` |
| `serial_no` | string | Yes | Serial number of the EV Charger. Example: `202320010308` |

---

## Responses

### 200 — Charger Schedules

```json
{
  "system_id": 698989834,
  "charger_schedules": [
    {
      "schedules": [
        {
          "days": [1, 2],
          "start_time": "1:00",
          "end_time": "2:00",
          "charging_level": 70
        },
        {
          "days": [3, 4, 5, 6, 7],
          "start_time": "2:00",
          "end_time": "3:00",
          "charging_level": 80
        }
      ],
      "type": "Custom",
      "is_active": false,
      "reminder_flag": true,
      "reminder_timer": 10
    }
  ]
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The system ID |
| `charger_schedules` | array | List of schedule configurations |
| `charger_schedules[].type` | string | Schedule type (e.g., `Custom`) |
| `charger_schedules[].is_active` | boolean | Whether this schedule is currently active |
| `charger_schedules[].reminder_flag` | boolean | Whether reminders are enabled |
| `charger_schedules[].reminder_timer` | integer | Reminder timer in minutes |
| `charger_schedules[].schedules` | array | List of individual schedule time blocks |
| `charger_schedules[].schedules[].days` | array of integers | Days of the week (1=Monday, 7=Sunday) |
| `charger_schedules[].schedules[].start_time` | string | Start time in `H:MM` format |
| `charger_schedules[].schedules[].end_time` | string | End time in `H:MM` format |
| `charger_schedules[].schedules[].charging_level` | integer | Charging level percentage (0–100) |

### 400 — Bad Request

```json
{
  "message": "Bad request",
  "code": "400",
  "details": "Invalid system_id or serial_no"
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
- [Start Charging](start-charging.md) — Send start charging command
- [Stop Charging](stop-charging.md) — Send stop charging command
- [Back to API Index](../README.md)
