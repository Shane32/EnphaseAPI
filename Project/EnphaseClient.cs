using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Shane32.EnphaseAPI.Models;

namespace Shane32.EnphaseAPI;

/// <summary>
/// Client that wraps the Enphase solar energy API.
/// </summary>
public class EnphaseClient : IEnphaseClient
{
    private readonly HttpClient _httpClient;
    private readonly EnphaseClientOptions _options;
    private readonly TimeProvider _timeProvider;

    /// <inheritdoc/>
    public string AccessToken { get; set; } = string.Empty;

    private static readonly JsonSerializerOptions _jsonOptions = new() {
        PropertyNameCaseInsensitive = true,
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
    };

    /// <summary>
    /// Initializes a new instance of <see cref="EnphaseClient"/>.
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> used to send requests.</param>
    /// <param name="options">Configuration options for the client.</param>
    /// <param name="timeProvider">Time provider used for retry delays.</param>
    public EnphaseClient(HttpClient httpClient, IOptions<EnphaseClientOptions> options, TimeProvider timeProvider)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _timeProvider = timeProvider;
    }

    private static bool IsRetriableException(Exception ex)
        => ex is EnphaseRateLimitException || ex is HttpRequestException;

    private async Task<TResponse> ExecuteWithRetryAsync<TResponse>(Func<Task<TResponse>> operation)
    {
        var maxRetries = _options.RetryCount;
        var delay = _options.RetryDelay;
        var backoffMultiplier = _options.RetryBackoffMultiplier;

        int attempt = 0;
        while (true) {
            try {
                return await operation().ConfigureAwait(false);
            } catch (Exception ex) when (attempt < maxRetries && IsRetriableException(ex)) {
                attempt++;
                if (delay > TimeSpan.Zero) {
                    await DelayAsync(delay).ConfigureAwait(false);
                    delay = TimeSpan.FromMilliseconds(delay.TotalMilliseconds * backoffMultiplier);
                }
            }
        }
    }

    private async Task DelayAsync(TimeSpan delay)
    {
        var tcs = new TaskCompletionSource<bool>();
        // The 'using' disposes the timer only after tcs.Task completes (i.e. after the callback fires).
        using var timer = _timeProvider.CreateTimer(
            _ => tcs.TrySetResult(true), null, delay, System.Threading.Timeout.InfiniteTimeSpan);
        await tcs.Task.ConfigureAwait(false);
    }

    /// <summary>
    /// Sends an authenticated HTTP GET request to the specified URL and deserializes the response.
    /// </summary>
    /// <typeparam name="TResponse">The type to deserialize the response body into.</typeparam>
    /// <param name="url">The request URL (relative to the base address).</param>
    /// <returns>The deserialized response.</returns>
    protected virtual Task<TResponse> GetAsync<TResponse>(string url)
        => ExecuteWithRetryAsync(async () => {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            request.Headers.TryAddWithoutValidation("key", _options.ApiKey);
            using var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            return await ProcessResponseAsync<TResponse>(response).ConfigureAwait(false);
        });

    /// <summary>
    /// Sends an authenticated HTTP POST request to the specified URL with an optional JSON body and deserializes the response.
    /// </summary>
    /// <typeparam name="TResponse">The type to deserialize the response body into.</typeparam>
    /// <param name="url">The request URL (relative to the base address).</param>
    /// <param name="body">Optional object to serialize as the request body.</param>
    /// <returns>The deserialized response.</returns>
    protected virtual Task<TResponse> PostAsync<TResponse>(string url, object? body = null)
        => ExecuteWithRetryAsync(async () => {
            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            request.Headers.TryAddWithoutValidation("key", _options.ApiKey);
            if (body != null) {
                request.Content = new StringContent(JsonSerializer.Serialize(body, _jsonOptions), Encoding.UTF8, "application/json");
            }
            using var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            return await ProcessResponseAsync<TResponse>(response).ConfigureAwait(false);
        });

    /// <summary>
    /// Sends an authenticated HTTP PUT request to the specified URL with an optional JSON body and deserializes the response.
    /// </summary>
    /// <typeparam name="TResponse">The type to deserialize the response body into.</typeparam>
    /// <param name="url">The request URL (relative to the base address).</param>
    /// <param name="body">Optional object to serialize as the request body.</param>
    /// <returns>The deserialized response.</returns>
    protected virtual Task<TResponse> PutAsync<TResponse>(string url, object? body = null)
        => ExecuteWithRetryAsync(async () => {
            using var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            request.Headers.TryAddWithoutValidation("key", _options.ApiKey);
            if (body != null) {
                request.Content = new StringContent(JsonSerializer.Serialize(body, _jsonOptions), Encoding.UTF8, "application/json");
            }
            using var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            return await ProcessResponseAsync<TResponse>(response).ConfigureAwait(false);
        });

    private static async Task<TResponse> ProcessResponseAsync<TResponse>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        if (response.IsSuccessStatusCode) {
            return JsonSerializer.Deserialize<TResponse>(content, _jsonOptions)!;
        }
        throw CreateException(response.StatusCode, content);
    }

    private static EnphaseException CreateException(HttpStatusCode statusCode, string content)
    {
        string? message = null;
        string? details = null;
        string? period = null;
        long? periodStart = null;
        long? periodEnd = null;
        int? limit = null;

        try {
            using var doc = JsonDocument.Parse(content);
            var root = doc.RootElement;

            if (root.TryGetProperty("message", out var msgEl)) {
                if (msgEl.ValueKind == JsonValueKind.String)
                    message = msgEl.GetString();
                else if (msgEl.ValueKind == JsonValueKind.Array && msgEl.GetArrayLength() > 0)
                    message = msgEl[0].GetString();
            }

            if (root.TryGetProperty("details", out var detEl)) {
                if (detEl.ValueKind == JsonValueKind.String)
                    details = detEl.GetString();
                else if (detEl.ValueKind == JsonValueKind.Array && detEl.GetArrayLength() > 0)
                    details = string.Join(", ", detEl.EnumerateArray().Select(e => e.GetString()));
            }

            if (message == null && root.TryGetProperty("errorMessages", out var errMsgs) && errMsgs.GetArrayLength() > 0)
                message = errMsgs[0].GetString();

            if (root.TryGetProperty("period", out var periodEl))
                period = periodEl.GetString();
            if (root.TryGetProperty("period_start", out var psEl) && psEl.ValueKind == JsonValueKind.Number)
                periodStart = psEl.GetInt64();
            if (root.TryGetProperty("period_end", out var peEl) && peEl.ValueKind == JsonValueKind.Number)
                periodEnd = peEl.GetInt64();
            if (root.TryGetProperty("limit", out var limEl) && limEl.ValueKind == JsonValueKind.Number)
                limit = limEl.GetInt32();
        } catch (JsonException) {
            message = content;
        }

        return (int)statusCode switch {
            400 => new EnphaseBadRequestException(message, details),
            401 => new EnphaseAuthenticationException(message, details),
            403 => new EnphaseForbiddenException(message, details),
            404 => new EnphaseNotFoundException(message, details),
            405 => new EnphaseMethodNotAllowedException(message, details),
            422 => new EnphaseUnprocessableException(message, details),
            429 => new EnphaseRateLimitException(message, details, period, periodStart, periodEnd, limit),
            500 => new EnphaseServerException(message, details),
            501 => new EnphaseNotImplementedException(message, details),
            _ => new EnphaseException((int)statusCode, message, details),
        };
    }

    private static string BuildUrl(string path, params (string name, string? value)[] queryParams)
    {
        var query = string.Join("&", queryParams
            .Where(p => p.value != null)
            .Select(p => $"{p.name}={Uri.EscapeDataString(p.value!)}"));
        return query.Length > 0 ? $"{path}?{query}" : path;
    }

    // === Systems ===

    /// <inheritdoc/>
    public Task<GetSystemsResponse> GetSystemsAsync(int? page = null, int? size = null, string? sortBy = null)
        => GetAsync<GetSystemsResponse>(BuildUrl("/api/v4/systems",
            ("page", page?.ToString(CultureInfo.InvariantCulture)),
            ("size", size?.ToString(CultureInfo.InvariantCulture)),
            ("sort_by", sortBy)));

    /// <inheritdoc/>
    public Task<GetSystemsResponse> SearchSystemsAsync(SearchSystemsRequest request, int? page = null, int? size = null, bool? liveStream = null)
        => PostAsync<GetSystemsResponse>(BuildUrl("/api/v4/systems/search",
            ("page", page?.ToString(CultureInfo.InvariantCulture)),
            ("size", size?.ToString(CultureInfo.InvariantCulture)),
            ("live_stream", liveStream?.ToString().ToLowerInvariant())), request);

    /// <inheritdoc/>
    public Task<SystemInfo> GetSystemAsync(int systemId)
        => GetAsync<SystemInfo>($"/api/v4/systems/{systemId}");

    /// <inheritdoc/>
    public Task<SystemSummary> GetSystemSummaryAsync(int systemId, DateTimeOffset? summaryDate = null)
        => GetAsync<SystemSummary>(BuildUrl($"/api/v4/systems/{systemId}/summary",
            ("summary_date", summaryDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<SystemDevices> GetSystemDevicesAsync(int systemId)
        => GetAsync<SystemDevices>($"/api/v4/systems/{systemId}/devices");

    /// <inheritdoc/>
    public Task<RetrieveSystemIdResponse> RetrieveSystemIdAsync(string serialNum)
        => GetAsync<RetrieveSystemIdResponse>(BuildUrl("/api/v4/systems/retrieve_system_id",
            ("serial_num", serialNum)));

    /// <inheritdoc/>
    public Task<GetSystemEventsResponse> GetSystemEventsAsync(int systemId, DateTimeOffset startTime, DateTimeOffset? endTime = null)
        => GetAsync<GetSystemEventsResponse>(BuildUrl($"/api/v4/systems/{systemId}/events",
            ("start_time", startTime.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture)),
            ("end_time", endTime?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<GetSystemAlarmsResponse> GetSystemAlarmsAsync(int systemId, DateTimeOffset startTime, DateTimeOffset? endTime = null, bool? cleared = null)
        => GetAsync<GetSystemAlarmsResponse>(BuildUrl($"/api/v4/systems/{systemId}/alarms",
            ("start_time", startTime.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture)),
            ("end_time", endTime?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture)),
            ("cleared", cleared?.ToString().ToLowerInvariant())));

    /// <inheritdoc/>
    public Task<GetEventTypesResponse> GetEventTypesAsync(int? eventTypeId = null)
        => GetAsync<GetEventTypesResponse>(BuildUrl("/api/v4/systems/event_types",
            ("event_type_id", eventTypeId?.ToString(CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<GetOpenEventsResponse> GetOpenEventsAsync(int systemId)
        => GetAsync<GetOpenEventsResponse>($"/api/v4/systems/{systemId}/open_events");

    /// <inheritdoc/>
    public Task<List<InvertersSummaryItem>> GetInvertersSummaryAsync(int? siteId = null, int? envoySerialNumber = null, int? page = null, int? size = null)
        => GetAsync<List<InvertersSummaryItem>>(BuildUrl("/api/v4/systems/inverters_summary_by_envoy_or_site",
            ("site_id", siteId?.ToString(CultureInfo.InvariantCulture)),
            ("envoy_serial_number", envoySerialNumber?.ToString(CultureInfo.InvariantCulture)),
            ("page", page?.ToString(CultureInfo.InvariantCulture)),
            ("size", size?.ToString(CultureInfo.InvariantCulture))));

    // === Production ===

    /// <inheritdoc/>
    public Task<GetProductionMeterReadingsResponse> GetProductionMeterReadingsAsync(int systemId, DateTimeOffset? endAt = null)
        => GetAsync<GetProductionMeterReadingsResponse>(BuildUrl($"/api/v4/systems/{systemId}/production_meter_readings",
            ("end_at", endAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<GetRgmStatsResponse> GetRgmStatsAsync(int systemId, DateTimeOffset? startAt = null, DateTimeOffset? endAt = null)
        => GetAsync<GetRgmStatsResponse>(BuildUrl($"/api/v4/systems/{systemId}/rgm_stats",
            ("start_at", startAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture)),
            ("end_at", endAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<GetEnergyLifetimeResponse> GetEnergyLifetimeAsync(int systemId, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, string? production = null)
        => GetAsync<GetEnergyLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/energy_lifetime",
            ("start_date", startDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            ("end_date", endDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            ("production", production)));

    /// <inheritdoc/>
    public Task<GetProductionMicroTelemetryResponse> GetProductionMicroTelemetryAsync(int systemId, DateTimeOffset? startAt = null, string? granularity = null)
        => GetAsync<GetProductionMicroTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/telemetry/production_micro",
            ("start_at", startAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture)),
            ("granularity", granularity)));

    /// <inheritdoc/>
    public Task<GetProductionMeterTelemetryResponse> GetProductionMeterTelemetryAsync(int systemId, DateTimeOffset? startAt = null, string? granularity = null)
        => GetAsync<GetProductionMeterTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/telemetry/production_meter",
            ("start_at", startAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture)),
            ("granularity", granularity)));

    // === Consumption ===

    /// <inheritdoc/>
    public Task<GetConsumptionMeterReadingsResponse> GetConsumptionMeterReadingsAsync(int systemId, DateTimeOffset? endAt = null)
        => GetAsync<GetConsumptionMeterReadingsResponse>(BuildUrl($"/api/v4/systems/{systemId}/consumption_meter_readings",
            ("end_at", endAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<GetStorageMeterReadingsResponse> GetStorageMeterReadingsAsync(int systemId, DateTimeOffset? endAt = null)
        => GetAsync<GetStorageMeterReadingsResponse>(BuildUrl($"/api/v4/systems/{systemId}/storage_meter_readings",
            ("end_at", endAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<GetConsumptionLifetimeResponse> GetConsumptionLifetimeAsync(int systemId, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
        => GetAsync<GetConsumptionLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/consumption_lifetime",
            ("start_date", startDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            ("end_date", endDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<GetBatteryLifetimeResponse> GetBatteryLifetimeAsync(int systemId, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
        => GetAsync<GetBatteryLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/battery_lifetime",
            ("start_date", startDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            ("end_date", endDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<GetEnergyImportLifetimeResponse> GetEnergyImportLifetimeAsync(int systemId, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
        => GetAsync<GetEnergyImportLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/energy_import_lifetime",
            ("start_date", startDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            ("end_date", endDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<GetEnergyExportLifetimeResponse> GetEnergyExportLifetimeAsync(int systemId, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
        => GetAsync<GetEnergyExportLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/energy_export_lifetime",
            ("start_date", startDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            ("end_date", endDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<GetBatteryTelemetryResponse> GetBatteryTelemetryAsync(int systemId, DateTimeOffset? startAt = null, string? granularity = null)
        => GetAsync<GetBatteryTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/telemetry/battery",
            ("start_at", startAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture)),
            ("granularity", granularity)));

    /// <inheritdoc/>
    public Task<GetConsumptionMeterTelemetryResponse> GetConsumptionMeterTelemetryAsync(int systemId, DateTimeOffset? startAt = null, string? granularity = null)
        => GetAsync<GetConsumptionMeterTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/telemetry/consumption_meter",
            ("start_at", startAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture)),
            ("granularity", granularity)));

    /// <inheritdoc/>
    public Task<GetEnergyImportTelemetryResponse> GetEnergyImportTelemetryAsync(int systemId, DateTimeOffset? startAt = null, string? granularity = null)
        => GetAsync<GetEnergyImportTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/energy_import_telemetry",
            ("start_at", startAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture)),
            ("granularity", granularity)));

    /// <inheritdoc/>
    public Task<GetEnergyExportTelemetryResponse> GetEnergyExportTelemetryAsync(int systemId, DateTimeOffset? startAt = null, string? granularity = null)
        => GetAsync<GetEnergyExportTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/energy_export_telemetry",
            ("start_at", startAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture)),
            ("granularity", granularity)));

    /// <inheritdoc/>
    public Task<GetLatestTelemetryResponse> GetLatestTelemetryAsync(int systemId)
        => GetAsync<GetLatestTelemetryResponse>($"/api/v4/systems/{systemId}/latest_telemetry");

    // === Devices ===

    /// <inheritdoc/>
    public Task<GetMicroTelemetryResponse> GetMicroTelemetryAsync(int systemId, string serialNo, DateTimeOffset? startAt = null, string? granularity = null)
        => GetAsync<GetMicroTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/devices/micros/{serialNo}/telemetry",
            ("start_at", startAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture)),
            ("granularity", granularity)));

    /// <inheritdoc/>
    public Task<GetEnchargeTelemetryResponse> GetEnchargeTelemetryAsync(int systemId, string serialNo, DateTimeOffset? startAt = null, string? granularity = null)
        => GetAsync<GetEnchargeTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/devices/encharges/{serialNo}/telemetry",
            ("start_at", startAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture)),
            ("granularity", granularity)));

    /// <inheritdoc/>
    public Task<GetEvseLifetimeResponse> GetEvseLifetimeAsync(int systemId, string serialNo, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
        => GetAsync<GetEvseLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/{serialNo}/evse_lifetime",
            ("start_date", startDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            ("end_date", endDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<GetEvseTelemetryResponse> GetEvseTelemetryAsync(int systemId, string serialNo, DateTimeOffset? startAt = null, string? granularity = null, string? intervalDuration = null)
        => GetAsync<GetEvseTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/{serialNo}/evse_telemetry",
            ("start_at", startAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture)),
            ("granularity", granularity),
            ("interval_duration", intervalDuration)));

    /// <inheritdoc/>
    public Task<GetHpLifetimeResponse> GetHpLifetimeAsync(int systemId, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
        => GetAsync<GetHpLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/hp_lifetime",
            ("start_date", startDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            ("end_date", endDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<GetHpTelemetryResponse> GetHpTelemetryAsync(int systemId, DateTimeOffset? startAt = null, DateTimeOffset? startDate = null, string? granularity = null, string? intervalDuration = null)
        => GetAsync<GetHpTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/hp_telemetry",
            ("start_at", startAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture)),
            ("start_date", startDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            ("granularity", granularity),
            ("interval_duration", intervalDuration)));

    // === Config ===

    /// <inheritdoc/>
    public Task<BatterySettings> GetBatterySettingsAsync(int systemId)
        => GetAsync<BatterySettings>($"/api/v4/systems/config/{systemId}/battery_settings");

    /// <inheritdoc/>
    public Task<BatterySettings> UpdateBatterySettingsAsync(int systemId, UpdateBatterySettingsRequest request)
        => PutAsync<BatterySettings>($"/api/v4/systems/config/{systemId}/battery_settings", request);

    /// <inheritdoc/>
    public Task<StormGuardSettings> GetStormGuardAsync(int systemId)
        => GetAsync<StormGuardSettings>($"/api/v4/systems/config/{systemId}/storm_guard");

    /// <inheritdoc/>
    public Task<GridStatus> GetGridStatusAsync(int systemId)
        => GetAsync<GridStatus>($"/api/v4/systems/config/{systemId}/grid_status");

    /// <inheritdoc/>
    public Task<GetLoadControlResponse> GetLoadControlAsync(int systemId)
        => GetAsync<GetLoadControlResponse>($"/api/v4/systems/config/{systemId}/load_control");

    // === EV Charger ===

    /// <inheritdoc/>
    public Task<EvChargerDevices> GetEvChargerDevicesAsync(int systemId)
        => GetAsync<EvChargerDevices>($"/api/v4/systems/{systemId}/ev_charger/devices");

    /// <inheritdoc/>
    public Task<GetEvChargerEventsResponse> GetEvChargerEventsAsync(int systemId, int? offset = null, string? serialNum = null, int? limit = null)
        => GetAsync<GetEvChargerEventsResponse>(BuildUrl($"/api/v4/systems/{systemId}/ev_charger/events",
            ("offset", offset?.ToString(CultureInfo.InvariantCulture)),
            ("serial_num", serialNum),
            ("limit", limit?.ToString(CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<GetEvChargerSessionsResponse> GetEvChargerSessionsAsync(int systemId, string serialNo, int? offset = null, int? limit = null)
        => GetAsync<GetEvChargerSessionsResponse>(BuildUrl($"/api/v4/systems/{systemId}/ev_charger/{serialNo}/sessions",
            ("offset", offset?.ToString(CultureInfo.InvariantCulture)),
            ("limit", limit?.ToString(CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<GetEvChargerSchedulesResponse> GetEvChargerSchedulesAsync(int systemId, string serialNo)
        => GetAsync<GetEvChargerSchedulesResponse>($"/api/v4/systems/{systemId}/ev_charger/{serialNo}/schedules");

    /// <inheritdoc/>
    public Task<GetEvChargerLifetimeResponse> GetEvChargerLifetimeAsync(int systemId, string serialNo, DateTimeOffset startDate, DateTimeOffset? endDate = null)
        => GetAsync<GetEvChargerLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/ev_charger/{serialNo}/lifetime",
            ("start_date", startDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            ("end_date", endDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<GetEvChargerTelemetryResponse> GetEvChargerTelemetryAsync(int systemId, string serialNo, string? granularity = null, DateTimeOffset? startDate = null, DateTimeOffset? startAt = null)
        => GetAsync<GetEvChargerTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/ev_charger/{serialNo}/telemetry",
            ("granularity", granularity),
            ("start_date", startDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
            ("start_at", startAt?.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture))));

    /// <inheritdoc/>
    public Task<ChargingCommandResponse> StartChargingAsync(int systemId, string serialNo, StartChargingRequest request)
        => PostAsync<ChargingCommandResponse>($"/api/v4/systems/{systemId}/ev_charger/{serialNo}/start_charging", request);

    /// <inheritdoc/>
    public Task<ChargingCommandResponse> StopChargingAsync(int systemId, string serialNo)
        => PostAsync<ChargingCommandResponse>($"/api/v4/systems/{systemId}/ev_charger/{serialNo}/stop_charging");
}
