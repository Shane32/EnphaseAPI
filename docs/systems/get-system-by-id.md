# GET /api/v4/systems/{system_id} — Retrieve a System by ID

Retrieves a System by ID.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}`

> **Note:** If an empty value is passed in the ID, this endpoint behaves as the [Fetch Systems](get-systems.md) endpoint.

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. |

---

## Responses

### 200 — System Fetched

```json
{
  "system_id": 72,
  "name": "Enphase System",
  "public_name": "Residential System",
  "timezone": "America/Los_Angeles",
  "address": {
    "city": "Los Angeles",
    "state": "CA",
    "country": "US",
    "postal_code": "94954"
  },
  "connection_type": "cellular",
  "energy_lifetime": -1,
  "energy_today": -1,
  "system_size": -1,
  "status": "normal",
  "last_report_at": 1445619615,
  "last_energy_at": 1445619033,
  "operational_at": 1357023600,
  "attachment_type": "acm",
  "interconnect_date": "2012-10-13",
  "reference": "106015287",
  "other_references": ["106015287"]
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `system_id` | integer | Unique numeric ID of the system |
| `name` | string | System name |
| `public_name` | string | Public-facing system name |
| `timezone` | string | System timezone |
| `address` | object | System address (city, state, country, postal_code) |
| `connection_type` | string | Connection type (e.g., `ethernet`, `cellular`) |
| `energy_lifetime` | integer | Lifetime energy in Wh (-1 if unavailable) |
| `energy_today` | integer | Today's energy in Wh (-1 if unavailable) |
| `system_size` | integer | System size in W (-1 if unavailable) |
| `status` | string | System status (e.g., `normal`, `micro`) |
| `last_report_at` | integer | Unix epoch of last report |
| `last_energy_at` | integer | Unix epoch of last energy report |
| `operational_at` | integer | Unix epoch when system became operational |
| `attachment_type` | string\|null | Attachment type (e.g., `acm`) |
| `interconnect_date` | string\|null | Interconnect date (YYYY-MM-DD) |
| `reference` | string | Primary reference identifier |
| `other_references` | array | Additional reference identifiers |

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

- [Fetch Systems](get-systems.md) — Paginated list of all systems
- [Get System Summary](get-system-summary.md) — Summary data for a system
- [Get System Devices](get-system-devices.md) — Devices attached to a system
- [Back to API Index](../README.md)
