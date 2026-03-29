# Enphase Monitoring API Documentation

This documentation covers the **Enphase Monitoring API v4**, which provides access to solar system data, device telemetry, energy production/consumption metrics, EV charger monitoring and control, and system configuration.

## Authentication

All API requests require **OAuth 2.0** authentication. Each request must include:

- An OAuth 2.0 access token in the `Authorization` header using the `Bearer` scheme
- Your application's API key in a header named `key`

```
Authorization: Bearer <access_token>
key: <your_api_key>
```

## Base URL

```
https://api.enphaseenergy.com
```

All endpoints are prefixed with `/api/v4/`.

## Common Response Codes

| Code | Meaning |
|------|---------|
| `200` | Success |
| `401` | Authentication Error — token missing, invalid, or expired |
| `403` | Forbidden — not authorized to access this resource |
| `404` | Not Found — system or device not found |
| `405` | Method Not Allowed |
| `422` | Unprocessable Entity — invalid request parameters |
| `429` | Too Many Requests — rate limit exceeded |
| `500` | Internal Server Error |
| `501` | Not Implemented |

## Rate Limiting

When the rate limit is exceeded, the API returns a `429` response with details about the limit period:

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

---

## API Reference

### System Details

Endpoints for fetching, searching, and retrieving metadata about systems and their devices.

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | [/api/v4/systems](systems/get-systems.md) | Fetch systems (paginated list) |
| `POST` | [/api/v4/systems/search](systems/search-systems.md) | Search and filter systems |
| `GET` | [/api/v4/systems/{system_id}](systems/get-system-by-id.md) | Retrieve a system by ID |
| `GET` | [/api/v4/systems/{system_id}/summary](systems/get-system-summary.md) | Retrieve system summary |
| `GET` | [/api/v4/systems/{system_id}/devices](systems/get-system-devices.md) | Retrieve devices for a system |
| `GET` | [/api/v4/systems/retrieve_system_id](systems/retrieve-system-id.md) | Retrieve system ID by Envoy serial number |
| `GET` | [/api/v4/systems/{system_id}/events](systems/get-system-events.md) | Retrieve events for a site |
| `GET` | [/api/v4/systems/{system_id}/alarms](systems/get-system-alarms.md) | Retrieve alarms for a site |
| `GET` | [/api/v4/systems/event_types](systems/get-event-types.md) | Retrieve event type definitions |
| `GET` | [/api/v4/systems/{system_id}/open_events](systems/get-open-events.md) | Retrieve all open events for a site |
| `GET` | [/api/v4/systems/inverters_summary_by_envoy_or_site](systems/inverters-summary.md) | Microinverter summary by Envoy or site |

---

### Site Level Production Monitoring

Endpoints for monitoring solar energy production at the site level.

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | [/api/v4/systems/{system_id}/production_meter_readings](production/production-meter-readings.md) | Last known production meter readings |
| `GET` | [/api/v4/systems/{system_id}/rgm_stats](production/rgm-stats.md) | Revenue-grade meter statistics |
| `GET` | [/api/v4/systems/{system_id}/energy_lifetime](production/energy-lifetime.md) | Daily lifetime energy production time series |
| `GET` | [/api/v4/systems/{system_id}/telemetry/production_micro](production/telemetry-production-micro.md) | Telemetry for all production micros |
| `GET` | [/api/v4/systems/{system_id}/telemetry/production_meter](production/telemetry-production-meter.md) | Telemetry for all production meters |

---

### Site Level Consumption Monitoring

Endpoints for monitoring energy consumption, storage, import, and export at the site level.

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | [/api/v4/systems/{system_id}/consumption_meter_readings](consumption/consumption-meter-readings.md) | Last known consumption meter readings |
| `GET` | [/api/v4/systems/{system_id}/storage_meter_readings](consumption/storage-meter-readings.md) | Last known storage meter readings |
| `GET` | [/api/v4/systems/{system_id}/consumption_lifetime](consumption/consumption-lifetime.md) | Daily lifetime consumption time series |
| `GET` | [/api/v4/systems/{system_id}/battery_lifetime](consumption/battery-lifetime.md) | Daily battery charge/discharge time series |
| `GET` | [/api/v4/systems/{system_id}/energy_import_lifetime](consumption/energy-import-lifetime.md) | Daily lifetime grid import time series |
| `GET` | [/api/v4/systems/{system_id}/energy_export_lifetime](consumption/energy-export-lifetime.md) | Daily lifetime grid export time series |
| `GET` | [/api/v4/systems/{system_id}/telemetry/battery](consumption/telemetry-battery.md) | Battery telemetry intervals |
| `GET` | [/api/v4/systems/{system_id}/telemetry/consumption_meter](consumption/telemetry-consumption-meter.md) | Consumption meter telemetry intervals |
| `GET` | [/api/v4/systems/{system_id}/energy_import_telemetry](consumption/energy-import-telemetry.md) | Grid import telemetry intervals |
| `GET` | [/api/v4/systems/{system_id}/energy_export_telemetry](consumption/energy-export-telemetry.md) | Grid export telemetry intervals |
| `GET` | [/api/v4/systems/{system_id}/latest_telemetry](consumption/latest-telemetry.md) | Latest real-time power readings |

---

### Device Level Monitoring

Endpoints for monitoring individual devices such as microinverters, batteries, EV chargers, and heat pumps.

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | [/api/v4/systems/{system_id}/devices/micros/{serial_no}/telemetry](devices/micro-telemetry.md) | Telemetry for a single microinverter |
| `GET` | [/api/v4/systems/{system_id}/devices/encharges/{serial_no}/telemetry](devices/encharge-telemetry.md) | Telemetry for a single Encharge battery |
| `GET` | [/api/v4/systems/{system_id}/{serial_no}/evse_lifetime](devices/evse-lifetime.md) | Daily EVSE charger time series |
| `GET` | [/api/v4/systems/{system_id}/hp_lifetime](devices/hp-lifetime.md) | Daily heat pump time series |
| `GET` | [/api/v4/systems/{system_id}/{serial_no}/evse_telemetry](devices/evse-telemetry.md) | EVSE charger telemetry intervals |
| `GET` | [/api/v4/systems/{system_id}/hp_telemetry](devices/hp-telemetry.md) | Heat pump telemetry intervals |

---

### System Configurations

Endpoints for reading and updating system configuration settings.

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | [/api/v4/systems/config/{system_id}/battery_settings](config/get-battery-settings.md) | Get current battery settings |
| `PUT` | [/api/v4/systems/config/{system_id}/battery_settings](config/put-battery-settings.md) | Update battery settings |
| `GET` | [/api/v4/systems/config/{system_id}/storm_guard](config/get-storm-guard.md) | Get storm guard settings |
| `GET` | [/api/v4/systems/config/{system_id}/grid_status](config/get-grid-status.md) | Get current grid status |
| `GET` | [/api/v4/systems/config/{system_id}/load_control](config/get-load-control.md) | Get load control settings |

---

### Streaming APIs

Server-Sent Events (SSE) endpoint for real-time live data streaming.

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | [/api/v4/systems/{system_id}/live_data](streaming/live-data.md) | Site-level live status stream |

---

### EV Charger Monitoring

Endpoints for monitoring EV charger devices, sessions, schedules, and energy data.

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | [/api/v4/systems/{system_id}/ev_charger/devices](ev-charger/get-ev-charger-devices.md) | Fetch active EV chargers |
| `GET` | [/api/v4/systems/{system_id}/ev_charger/events](ev-charger/get-ev-charger-events.md) | Fetch EV charger events |
| `GET` | [/api/v4/systems/{system_id}/ev_charger/{serial_no}/sessions](ev-charger/get-ev-charger-sessions.md) | Charger session history |
| `GET` | [/api/v4/systems/{system_id}/ev_charger/{serial_no}/schedules](ev-charger/get-ev-charger-schedules.md) | Get charger schedules |
| `GET` | [/api/v4/systems/{system_id}/ev_charger/{serial_no}/lifetime](ev-charger/get-ev-charger-lifetime.md) | Daily EV charger energy time series |
| `GET` | [/api/v4/systems/{system_id}/ev_charger/{serial_no}/telemetry](ev-charger/get-ev-charger-telemetry.md) | EV charger telemetry intervals |

---

### EV Charger Control

Endpoints for sending commands to EV chargers.

> **Note:** These endpoints are illustrative only. Production access is via the VPP API for partners.

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | [/api/v4/systems/{system_id}/ev_charger/{serial_no}/start_charging](ev-charger/start-charging.md) | Start charging |
| `POST` | [/api/v4/systems/{system_id}/ev_charger/{serial_no}/stop_charging](ev-charger/stop-charging.md) | Stop charging |
