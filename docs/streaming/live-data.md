# GET /api/v4/systems/{system_id}/live_data — Site Level Live Status

API users can get real-time live status data on demand for a given system. Retrieves real-time power for PV Production, Grid Import/Export, Consumption, Battery Charge/Discharge, and Generator. Also retrieves Grid Status, Battery Mode, and Battery State of Charge.

The stream runs for a duration of 30 seconds by default. Configure the duration by passing the `duration` header parameter (minimum 30 seconds, maximum 300 seconds).

**Method:** `GET`  
**Endpoint:** `/api/v4/systems/{system_id}/live_data`  
**Response Type:** `text/event-stream` (Server-Sent Events)

---

## Requirements

- The Envoy must be active and running firmware version **6.0.0 or later**.
- For Ensemble sites (sites with battery or system controller):
  - If the site has an active battery or active system controller, it must have an active production meter **and** an active consumption meter.
  - Otherwise, the site must have an active production meter.

---

## Path Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `system_id` | integer | Yes | The unique numeric ID of the system. |

## Header Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `duration` | integer | No | Duration of the stream in seconds. Default=30, Min=30, Max=300. |

---

## Responses

### 200 — Successful Stream

The response is a Server-Sent Events (SSE) stream. Each event is a JSON object:

```json
{
  "data": {
    "data": {
      "battery_mode": "Savings Mode",
      "battery_power": 5,
      "battery_soc": 40,
      "consumption_power": 15,
      "envoy_serial_number": ["1234"],
      "generator_power": 0,
      "grid_power": -10,
      "grid_status": "On Grid",
      "pv_power": 30,
      "system_id": 123
    },
    "timestamp_epoch": 1679041508556,
    "timestamp_utc": "2023-03-17 08:25:08.556",
    "type": "response"
  }
}
```

### Stream Data Fields

| Field | Type | Description |
|-------|------|-------------|
| `data.data.battery_mode` | string | Current battery operating mode |
| `data.data.battery_power` | integer | Battery power in Watts (positive = discharging, negative = charging) |
| `data.data.battery_soc` | integer | Battery state of charge percentage |
| `data.data.consumption_power` | integer | Current consumption power in Watts |
| `data.data.envoy_serial_number` | array | Serial numbers of active Envoys |
| `data.data.generator_power` | integer | Generator power in Watts |
| `data.data.grid_power` | integer | Grid power in Watts (positive = importing, negative = exporting) |
| `data.data.grid_status` | string | Current grid status (e.g., `On Grid`, `Off Grid`) |
| `data.data.pv_power` | integer | PV production power in Watts |
| `data.data.system_id` | integer | The system ID |
| `data.timestamp_epoch` | integer | Unix epoch timestamp in milliseconds |
| `data.timestamp_utc` | string | UTC timestamp string |
| `data.type` | string | Event type (e.g., `response`) |

### 401 — Authentication Error

```json
{
  "error": {
    "code": 401,
    "details": [{}],
    "message": "API Key is invalid",
    "status": "NOT_AUTHENTICATED"
  },
  "timestamp_epoch": 1679041508556,
  "timestamp_utc": "2023-03-17 08:25:08.556",
  "type": "authentication_error"
}
```

### 403 — Forbidden

```json
{
  "error": {
    "code": 403,
    "details": [{}],
    "message": "User is not authorized",
    "status": "FORBIDDEN"
  },
  "timestamp_epoch": 1679041508556,
  "timestamp_utc": "2023-03-17 08:25:08.556",
  "type": "authorization_error"
}
```

### 429 — Too Many Requests

```json
{
  "error": {
    "code": 429,
    "details": [{}],
    "message": "Too Many Requests",
    "status": "TOO_MANY_REQUESTS"
  },
  "timestamp_epoch": 1679041508556,
  "timestamp_utc": "2023-03-17 08:25:08.556",
  "type": "request_exceeded_error"
}
```

### Custom Error Codes

| Code | Status | Description |
|------|--------|-------------|
| `461` | `INVALID_DURATION` | Duration is less than 30 seconds |
| `462` | `INVALID_DURATION` | Duration is greater than 900 seconds |
| `463` | `INVALID_DURATION` | Duration is not an integer |
| `466` | `UNSUPPORTED_ENVOY` | Envoy must be active and version ≥ 6.0.0 |
| `468` | `INVALID_SYSTEM_ID` | System ID does not exist |
| `472` | `LIVE_STREAM_NOT_SUPPORTED` | System doesn't support live stream |
| `473` | `IQ_GATEWAY_NOT_REPORTING` | IQ gateway is not reporting |
| `550` | `SERVICE_UNREACHABLE` | Service unreachable, please try again |
| `551` | `SERVICE_UNREACHABLE` | Service unreachable, please try again |
| `552` | `CONNECTION_NOT_ESTABLISHED` | Unable to connect, please try again |

---

## See Also

- [Latest Telemetry](../consumption/latest-telemetry.md) — Last reported power readings (non-streaming)
- [Get System Summary](../systems/get-system-summary.md) — System summary including current power
- [Get Grid Status](../config/get-grid-status.md) — Current grid status
- [Back to API Index](../README.md)
