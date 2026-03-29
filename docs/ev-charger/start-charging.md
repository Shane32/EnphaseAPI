# POST /api/v4/systems/{system_id}/ev_charger/{serial_no}/start_charging — Start Charging

Sends a start charging command to the EV Charger.

> **Note:** This endpoint is illustrative only. Production access is via the VPP API for partners.

**Method:** `POST`  
**Endpoint:** `/api/v4/systems/{system_id}/ev_charger/{serial_no}/start_charging`  
**Content-Type:** `application/json`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. Example: `701052773` |
| `serial_no` | string | Yes | Serial number of the EV Charger. Example: `202312100006` |

---

## Request Body

```json
{
  "connectorId": "1",
  "chargingLevel": "40"
}
```

### Request Body Fields

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `connectorId` | string | Yes | The connector ID to start charging on. Must be greater than 0. |
| `chargingLevel` | string | Yes | Charging level percentage (0–100). |

---

## Responses

### 202 — Request Accepted

```json
{
  "message": "Request sent successfully"
}
```

### 400 — Bad Request

```json
{
  "message": "Bad request",
  "code": "400",
  "details": [
    "Invalid system_id or serial_no",
    "Connector Id must be greater than 0",
    "Charging level should be in the range [0-100]"
  ]
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

- [Stop Charging](stop-charging.md) — Send stop charging command
- [EV Charger Devices](get-ev-charger-devices.md) — Fetch active EV chargers
- [EV Charger Sessions](get-ev-charger-sessions.md) — View charging session history
- [Back to API Index](../README.md)
