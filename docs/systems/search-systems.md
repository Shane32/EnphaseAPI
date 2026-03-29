# POST /api/v4/systems/search — Search and Filter Systems

Search and filter systems. Provide only valid values in request parameters. Empty values will be ignored. Invalid keys will be rejected.

**Method:** `POST`  
**Endpoint:** `/api/v4/systems/search`  
**Content-Type:** `application/json`

---

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `page` | integer | No | The page to be returned. Default=1, Min=1. |
| `size` | integer | No | Maximum number of records shown per page. Default=10, Min=1, Max=1000. |
| `live_stream` | boolean | No | When `true`, includes the `live_stream` field in each system in the response. |

---

## Request Body

```json
{
  "sort_by": "id",
  "system": {
    "ids": [0],
    "name": "string",
    "reference": "string",
    "other_reference": "string",
    "statuses": ["normal"]
  }
}
```

### Request Body Fields

| Field | Type | Description |
|-------|------|-------------|
| `sort_by` | string | Sort field. Use `id` for ascending, `-id` for descending. |
| `system.ids` | array of integers | Filter by specific system IDs. |
| `system.name` | string | Filter by system name (partial match). |
| `system.reference` | string | Filter by primary reference identifier. |
| `system.other_reference` | string | Filter by secondary reference identifier. |
| `system.statuses` | array of strings | Filter by system status values (e.g., `normal`, `micro`). |

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
      "timezone": null,
      "address": {
        "state": null,
        "country": null,
        "postal_code": null
      },
      "connection_type": "ethernet",
      "status": "micro",
      "last_report_at": 1557400231,
      "last_energy_at": null,
      "operational_at": null,
      "attachment_type": null,
      "interconnect_date": null,
      "energy_lifetime": -1,
      "energy_today": -1,
      "system_size": -1,
      "live_stream": "enabled"
    },
    {
      "system_id": 698906018,
      "name": "Enphase Public System",
      "public_name": "Residential System",
      "timezone": "US/Pacific",
      "address": {
        "state": "CA",
        "country": "US",
        "postal_code": "94954"
      },
      "connection_type": "ethernet",
      "status": "normal",
      "last_report_at": 1508174262,
      "last_energy_at": 1508174172,
      "operational_at": 1497445200,
      "attachment_type": null,
      "interconnect_date": null,
      "energy_lifetime": -1,
      "energy_today": -1,
      "system_size": -1,
      "reference": "106015287",
      "other_references": ["106015287"],
      "live_stream": "disabled"
    }
  ]
}
```

> **Note:** The `live_stream` field is only included when the `live_stream` query parameter is set to `true`.

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
  "details": "system is missing at Json body location",
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

- [Fetch Systems](get-systems.md) — Simple paginated list of systems
- [Get System by ID](get-system-by-id.md) — Retrieve a single system
- [Back to API Index](../README.md)
