# GET /api/v4/systems/{system_id}/ev_charger/{serial_no}/telemetry — EV Charger Interval Telemetry

Retrieves telemetry for a single EV Charger at regular intervals.

- If no `start_date` or `start_at` is specified, defaults to the previous day in the system's timezone.
- `end_at` is calculated as the minimum of the current time and `start_at + granularity`.
- If granularity is `day`, maximum 392 intervals will appear in response where each interval is 15 minutes in duration.

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/ev_charger/{serial_no}/telemetry`

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. Example: `700460094` |
| `serial_no` | string | Yes | Serial number of the EV Charger. Example: `202109116909` |

## Query Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `granularity` | string | No | Granularity of telemetry data. One of `week`, `day`. Default is `day`. Example: `day` |
| `start_date` | string | No | Start date in `YYYYMMDD` format. If neither `start_date` nor `start_at` is provided, defaults to the previous date. Example: `20240116` |
| `start_at` | string | No | Start time in Unix epoch format. Alternatively use `start_date`. Example: `1705425848` |

---

## Responses

### 200 — EV Charger Telemetry

```json
{
  "granularity": "day",
  "consumption": [
    {
      "consumption": 0,
      "end_at": 1705385700
    },
    {
      "consumption": 38,
      "end_at": 1705400100
    },
    {
      "consumption": 201,
      "end_at": 1705401000
    },
    {
      "consumption": 202,
      "end_at": 1705401900
    }
  ],
  "system_id": 700460094,
  "start_date": "2024-01-16",
  "end_date": "2024-01-16",
  "start_at": 0,
  "end_at": 0
}
```

### Response Fields

| Field | Type | Description |
|-------|------|-------------|
| `granularity` | string | Granularity of the returned data |
| `system_id` | integer | The system ID |
| `start_date` | string | Start date of the data (YYYY-MM-DD) |
| `end_date` | string | End date of the data (YYYY-MM-DD) |
| `start_at` | integer | Unix epoch of the start of the period |
| `end_at` | integer | Unix epoch of the end of the period |
| `consumption` | array | List of interval objects |
| `consumption[].end_at` | integer | Unix epoch of interval end |
| `consumption[].consumption` | integer | Energy consumed by the EV charger in Wh during this interval |

### 400 — Bad Request

```json
{
  "message": "Bad request",
  "code": "400",
  "details": ["Invalid granularity", "Invalid system_id", "Invalid start_date"]
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

- [EV Charger Lifetime](get-ev-charger-lifetime.md) — Daily EV charger energy time series
- [EV Charger Sessions](get-ev-charger-sessions.md) — Charger session history
- [EVSE Telemetry](../devices/evse-telemetry.md) — Telemetry for 3rd party EVSE chargers
- [Back to API Index](../README.md)
