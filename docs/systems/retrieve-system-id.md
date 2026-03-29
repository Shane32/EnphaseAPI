# GET /api/v4/systems/retrieve_system_id — Retrieve System ID by Envoy Serial Number

Get system ID by passing an Envoy serial number. If the serial number of a retired Envoy is passed, a 404 Not Found response will be returned.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/retrieve_system_id`

---

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `serial_num` | string | Yes | The Envoy serial number. |

---

## Responses

### 200 — System ID Found

```json
{
  "system_id": 123
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | The unique numeric ID of the system associated with the given Envoy serial number. |

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
  "details": "Envoy not found with this serial number",
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
  "details": "Provide envoy serial number",
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

- [Get System by ID](get-system-by-id.md) — Retrieve full system details using the returned system ID
- [Get System Devices](get-system-devices.md) — List devices including gateways with serial numbers
- [Back to API Index](../README.md)
