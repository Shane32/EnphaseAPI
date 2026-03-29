# GET /api/v4/systems/{system_id}/ev_charger/devices — Fetch Active EV Chargers

Fetches all active EV Chargers for a system.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/ev_charger/devices`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. Example: `698989834` |

---

## Responses

### 200 — Active Chargers

```json
{
  "items": "devices",
  "total_devices": 1,
  "system_id": 698989834,
  "devices": {
    "ev_chargers": [
      {
        "id": "202320010308:698989834",
        "sku": "IQ-EVSE-NA-1040-0110-0100",
        "status": "normal",
        "serial_number": "202320010308",
        "name": "IQ EV Charger NACS",
        "model": "IQ-EVSE-40R",
        "part_number": "800-00555 0303",
        "last_report_at": 1700074065,
        "firmware": "v0.04.22",
        "active": true
      }
    ]
  }
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `items` | string | Always `"devices"` |
| `total_devices` | integer | Total number of active EV chargers |
| `system_id` | integer | The system ID |
| `devices.ev_chargers` | array | List of active EV charger objects |
| `devices.ev_chargers[].id` | string | Composite device ID (`serial_number:system_id`) |
| `devices.ev_chargers[].sku` | string | SKU identifier |
| `devices.ev_chargers[].status` | string | Device status (e.g., `normal`) |
| `devices.ev_chargers[].serial_number` | string | EV charger serial number |
| `devices.ev_chargers[].name` | string | Device display name |
| `devices.ev_chargers[].model` | string | Device model |
| `devices.ev_chargers[].part_number` | string | Part number |
| `devices.ev_chargers[].last_report_at` | integer | Unix epoch of last report |
| `devices.ev_chargers[].firmware` | string | Firmware version |
| `devices.ev_chargers[].active` | boolean | Whether the device is active |

### 400 — Bad Request

```json
{
  "message": "Bad request",
  "code": "400",
  "details": "Invalid system_id"
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

- [EV Charger Events](get-ev-charger-events.md) — Fetch EV charger events
- [EV Charger Sessions](get-ev-charger-sessions.md) — Charger session history
- [EV Charger Schedules](get-ev-charger-schedules.md) — Get charger schedules
- [Get System Devices](../systems/get-system-devices.md) — Full device list including EV chargers
- [Back to API Index](../README.md)
