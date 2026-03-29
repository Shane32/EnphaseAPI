# GET /api/v4/systems — Fetch Systems

Returns a list of systems for which the user can make API requests. By default, systems are returned in batches of 10. The maximum size is 100.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems`

---

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `page` | integer | No | The page to be returned. Default=1, Min=1. |
| `size` | integer | No | Maximum number of records shown per page. Default=10, Min=1, Max=100. |
| `sort_by` | string | No | Sort field. Use `id` for ascending order, `-id` for descending order. Default is ascending by system ID. |

### `sort_by` Available Values

| Value | Description |
|-------|-------------|
| `id` | Sort ascending by system ID |
| `-id` | Sort descending by system ID |

---

## Responses

### 200 — List of Systems

```json
{
  "total": 28,
  "current_page": 1,
  "size": 2,
  "count": 2,
  "items": "systems",
  "systems": [
    {
      "system_id": 698910067,
      "name": "Enphase System",
      "public_name": "Residential System",
      "timezone": "Australia/Sydney",
      "address": {
        "city": "Sydney",
        "state": "NSW",
        "country": "AU",
        "postal_code": "2127"
      },
      "connection_type": "ethernet",
      "energy_lifetime": -1,
      "energy_today": -1,
      "system_size": -1,
      "status": "micro",
      "last_report_at": 1508174262,
      "last_energy_at": 1508174172,
      "operational_at": 1497445200,
      "attachment_type": null,
      "interconnect_date": null,
      "reference": "106015287",
      "other_references": ["106015287"]
    },
    {
      "system_id": 698906018,
      "name": "Enphase Public System",
      "public_name": "Residential System",
      "timezone": "US/Pacific",
      "address": {
        "city": "Los Angeles",
        "state": "CA",
        "country": "US",
        "postal_code": "94954"
      },
      "connection_type": "ethernet",
      "energy_lifetime": -1,
      "energy_today": -1,
      "system_size": -1,
      "status": "normal",
      "last_report_at": 1508174262,
      "last_energy_at": 1508174172,
      "operational_at": 1497445200,
      "attachment_type": null,
      "interconnect_date": null
    }
  ]
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `total` | integer | Total number of systems available |
| `current_page` | integer | Current page number |
| `size` | integer | Page size requested |
| `count` | integer | Number of systems in this response |
| `items` | string | Always `"systems"` |
| `systems` | array | Array of system objects |
| `systems[].system_id` | integer | Unique numeric ID of the system |
| `systems[].name` | string | System name |
| `systems[].public_name` | string | Public-facing system name |
| `systems[].timezone` | string | System timezone |
| `systems[].address` | object | System address (city, state, country, postal_code) |
| `systems[].connection_type` | string | Connection type (e.g., `ethernet`, `cellular`) |
| `systems[].energy_lifetime` | integer | Lifetime energy in Wh (-1 if unavailable) |
| `systems[].energy_today` | integer | Today's energy in Wh (-1 if unavailable) |
| `systems[].system_size` | integer | System size in W (-1 if unavailable) |
| `systems[].status` | string | System status (e.g., `normal`, `micro`) |
| `systems[].last_report_at` | integer | Unix epoch of last report |
| `systems[].last_energy_at` | integer | Unix epoch of last energy report |
| `systems[].operational_at` | integer | Unix epoch when system became operational |
| `systems[].attachment_type` | string\|null | Attachment type |
| `systems[].interconnect_date` | string\|null | Interconnect date |
| `systems[].reference` | string | Primary reference identifier |
| `systems[].other_references` | array | Additional reference identifiers |

### 401 — Authentication Error

```json
{
  "message": "Not Authorized",
  "details": "User is not authorized",
  "code": 401
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
  "details": "Invalid request because of 'The sorting parameter is not supported. Please use id for sorting by Asc or -id for sorting by Desc'",
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

- [Search Systems](search-systems.md) — Filter systems with advanced criteria
- [Get System by ID](get-system-by-id.md) — Retrieve a single system
- [Back to API Index](../README.md)
