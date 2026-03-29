# Shane32.EnphaseAPI

A .NET client library for the [Enphase Monitoring API v4](https://developer-v4.enphase.com/), providing a strongly-typed, async-first interface with full dependency injection support.

## Installation

```
dotnet add package Shane32.EnphaseAPI
```

## Authentication

All Enphase API requests require **OAuth 2.0** authentication. You must supply:

- An **OAuth 2.0 access token** obtained through the Enphase OAuth flow
- Your application's **API key** (obtained from the [Enphase Developer Portal](https://developer-v4.enphase.com/))

To obtain an access token:

1. Register your application at [developer-v4.enphase.com](https://developer-v4.enphase.com/) to get a client ID and client secret.
2. Direct the user to the Enphase authorization URL to grant consent.
3. Exchange the returned authorization code for an access token and refresh token via the token endpoint.
4. Use the access token in the `AccessToken` property of `IEnphaseClient` for each API call. Refresh the token as needed using the refresh token.

Each `IEnphaseClient` instance carries a single `AccessToken`, making it straightforward to manage tokens for multiple end users by creating separate client instances per user.

## Setup

Register the client in your dependency injection container:

```csharp
services.AddEnphaseClient(options =>
{
    options.ApiKey = "your-api-key";
});
```

Then inject `IEnphaseClient` where needed, set the `AccessToken` for the active user, and call API methods:

```csharp
public class MyService(IEnphaseClient enphaseClient)
{
    public async Task<GetSystemsResponse> GetSystemsAsync(string userAccessToken)
    {
        enphaseClient.AccessToken = userAccessToken;
        return await enphaseClient.GetSystemsAsync();
    }
}
```

## Configuration

### Options

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `ApiKey` | `string` | `""` | Your Enphase API key |
| `RetryCount` | `int` | `0` | Number of retry attempts on rate-limit or connectivity errors (0 = no retry) |
| `RetryDelay` | `TimeSpan` | `TimeSpan.Zero` | Initial delay before each retry |
| `RetryBackoffMultiplier` | `double` | `1.0` | Multiplier applied to the delay after each successive retry (1.0 = fixed delay, 2.0 = exponential backoff) |

Retries are triggered only by `EnphaseRateLimitException` (HTTP 429) or `HttpRequestException` (transient connectivity problems). All other exceptions are propagated immediately.

### Configuration via `IConfiguration`

Settings can be bound directly from `appsettings.json` or any other configuration source. Pass the desired `IConfiguration` section to `AddEnphaseClient` (the method binds directly to the provided section, giving you full control over the section name):

```csharp
services.AddEnphaseClient(configuration.GetSection("EnphaseAPI"));
```

Corresponding `appsettings.json`:

```json
{
  "EnphaseAPI": {
    "ApiKey": "your-api-key",
    "RetryCount": 3,
    "RetryDelay": "00:00:01",
    "RetryBackoffMultiplier": 2.0
  }
}
```

This example retries up to 3 times, with a 1-second delay that doubles after each attempt (1 s → 2 s → 4 s).

## Exceptions

All errors thrown by `IEnphaseClient` derive from `EnphaseException`.

| Exception | HTTP Status | Description |
|-----------|-------------|-------------|
| `EnphaseException` | any | Base class; exposes `HttpStatusCode` and `Details` |
| `EnphaseBadRequestException` | 400 | Invalid request |
| `EnphaseAuthenticationException` | 401 | Missing or invalid access token |
| `EnphaseForbiddenException` | 403 | Access denied |
| `EnphaseNotFoundException` | 404 | Resource not found |
| `EnphaseMethodNotAllowedException` | 405 | HTTP method not allowed |
| `EnphaseUnprocessableException` | 422 | Unprocessable entity |
| `EnphaseRateLimitException` | 429 | Rate limit exceeded; also exposes `Period`, `PeriodStart`, `PeriodEnd`, and `Limit` |
| `EnphaseServerException` | 500 | Enphase server error |
| `EnphaseNotImplementedException` | 501 | Endpoint not implemented |

## API Reference

### System Details

| Method | Description |
|--------|-------------|
| `GetSystemsAsync` | Fetch a paginated list of systems |
| `SearchSystemsAsync` | Search and filter systems |
| `GetSystemAsync` | Retrieve a system by ID |
| `GetSystemSummaryAsync` | Retrieve system summary |
| `GetSystemDevicesAsync` | Retrieve devices for a system |
| `RetrieveSystemIdAsync` | Retrieve a system ID by Envoy serial number |
| `GetSystemEventsAsync` | Retrieve events for a site |
| `GetSystemAlarmsAsync` | Retrieve alarms for a site |
| `GetEventTypesAsync` | Retrieve event type definitions |
| `GetOpenEventsAsync` | Retrieve all open events for a site |
| `GetInvertersSummaryAsync` | Microinverter summary by Envoy or site |

### Site Level Production Monitoring

| Method | Description |
|--------|-------------|
| `GetProductionMeterReadingsAsync` | Last known production meter readings |
| `GetRgmStatsAsync` | Revenue-grade meter statistics |
| `GetEnergyLifetimeAsync` | Daily lifetime energy production time series |
| `GetProductionMicroTelemetryAsync` | Telemetry for all production micros |
| `GetProductionMeterTelemetryAsync` | Telemetry for all production meters |

### Site Level Consumption Monitoring

| Method | Description |
|--------|-------------|
| `GetConsumptionMeterReadingsAsync` | Last known consumption meter readings |
| `GetStorageMeterReadingsAsync` | Last known storage meter readings |
| `GetConsumptionLifetimeAsync` | Daily lifetime consumption time series |
| `GetBatteryLifetimeAsync` | Daily battery charge/discharge time series |
| `GetEnergyImportLifetimeAsync` | Daily lifetime grid import time series |
| `GetEnergyExportLifetimeAsync` | Daily lifetime grid export time series |
| `GetBatteryTelemetryAsync` | Battery telemetry intervals |
| `GetConsumptionMeterTelemetryAsync` | Consumption meter telemetry intervals |
| `GetEnergyImportTelemetryAsync` | Grid import telemetry intervals |
| `GetEnergyExportTelemetryAsync` | Grid export telemetry intervals |
| `GetLatestTelemetryAsync` | Latest real-time power readings |

### Device Level Monitoring

| Method | Description |
|--------|-------------|
| `GetMicroTelemetryAsync` | Telemetry for a single microinverter |
| `GetEnchargeTelemetryAsync` | Telemetry for a single Encharge battery |
| `GetEvseLifetimeAsync` | Daily EVSE charger time series |
| `GetEvseTelemtryAsync` | EVSE charger telemetry intervals |
| `GetHpLifetimeAsync` | Daily heat pump time series |
| `GetHpTelemetryAsync` | Heat pump telemetry intervals |

### System Configurations

| Method | Description |
|--------|-------------|
| `GetBatterySettingsAsync` | Get current battery settings |
| `UpdateBatterySettingsAsync` | Update battery settings |
| `GetStormGuardAsync` | Get storm guard settings |
| `GetGridStatusAsync` | Get current grid status |
| `GetLoadControlAsync` | Get load control settings |

### EV Charger Monitoring

| Method | Description |
|--------|-------------|
| `GetEvChargerDevicesAsync` | Fetch active EV chargers |
| `GetEvChargerEventsAsync` | Fetch EV charger events |
| `GetEvChargerSessionsAsync` | Charger session history |
| `GetEvChargerSchedulesAsync` | Get charger schedules |
| `GetEvChargerLifetimeAsync` | Daily EV charger energy time series |
| `GetEvChargerTelemetryAsync` | EV charger telemetry intervals |

### EV Charger Control

| Method | Description |
|--------|-------------|
| `StartChargingAsync` | Send a start charging command |
| `StopChargingAsync` | Send a stop charging command |

## License

This library is licensed under the [MIT License](LICENSE.txt).

## Credits

Glory to Jehovah, Lord of Lords and King of Kings, creator of Heaven and Earth, who through his Son Jesus Christ, has redeemed me to become a child of God. -[Shane32](https://github.com/Shane32)
