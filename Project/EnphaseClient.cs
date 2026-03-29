using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Shane32.EnphaseAPI.Models;

namespace Shane32.EnphaseAPI;

public class EnphaseClient : IEnphaseClient
{
    private readonly HttpClient _httpClient;
    private readonly EnphaseClientOptions _options;

    public string AccessToken { get; set; } = string.Empty;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public EnphaseClient(HttpClient httpClient, IOptions<EnphaseClientOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    protected virtual async Task<TResponse> GetAsync<TResponse>(string url)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
        request.Headers.TryAddWithoutValidation("key", _options.ApiKey);
        using var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        return await ProcessResponseAsync<TResponse>(response).ConfigureAwait(false);
    }

    protected virtual async Task<TResponse> PostAsync<TResponse>(string url, object? body = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
        request.Headers.TryAddWithoutValidation("key", _options.ApiKey);
        if (body != null)
        {
            request.Content = new StringContent(JsonSerializer.Serialize(body, _jsonOptions), Encoding.UTF8, "application/json");
        }
        using var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        return await ProcessResponseAsync<TResponse>(response).ConfigureAwait(false);
    }

    protected virtual async Task<TResponse> PutAsync<TResponse>(string url, object? body = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Put, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
        request.Headers.TryAddWithoutValidation("key", _options.ApiKey);
        if (body != null)
        {
            request.Content = new StringContent(JsonSerializer.Serialize(body, _jsonOptions), Encoding.UTF8, "application/json");
        }
        using var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        return await ProcessResponseAsync<TResponse>(response).ConfigureAwait(false);
    }

    private static async Task<TResponse> ProcessResponseAsync<TResponse>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
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

        try
        {
            using var doc = JsonDocument.Parse(content);
            var root = doc.RootElement;

            if (root.TryGetProperty("message", out var msgEl))
            {
                if (msgEl.ValueKind == JsonValueKind.String)
                    message = msgEl.GetString();
                else if (msgEl.ValueKind == JsonValueKind.Array && msgEl.GetArrayLength() > 0)
                    message = msgEl[0].GetString();
            }

            if (root.TryGetProperty("details", out var detEl))
            {
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
        }
        catch (JsonException)
        {
            message = content;
        }

        return (int)statusCode switch
        {
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

    public Task<GetSystemsResponse> GetSystemsAsync(int? page = null, int? size = null, string? sortBy = null)
        => GetAsync<GetSystemsResponse>(BuildUrl("/api/v4/systems",
            ("page", page?.ToString()),
            ("size", size?.ToString()),
            ("sort_by", sortBy)));

    public Task<GetSystemsResponse> SearchSystemsAsync(SearchSystemsRequest request, int? page = null, int? size = null, bool? liveStream = null)
        => PostAsync<GetSystemsResponse>(BuildUrl("/api/v4/systems/search",
            ("page", page?.ToString()),
            ("size", size?.ToString()),
            ("live_stream", liveStream?.ToString().ToLowerInvariant())), request);

    public Task<SystemInfo> GetSystemAsync(int systemId)
        => GetAsync<SystemInfo>($"/api/v4/systems/{systemId}");

    public Task<SystemSummary> GetSystemSummaryAsync(int systemId, string? summaryDate = null)
        => GetAsync<SystemSummary>(BuildUrl($"/api/v4/systems/{systemId}/summary",
            ("summary_date", summaryDate)));

    public Task<SystemDevices> GetSystemDevicesAsync(int systemId)
        => GetAsync<SystemDevices>($"/api/v4/systems/{systemId}/devices");

    public Task<RetrieveSystemIdResponse> RetrieveSystemIdAsync(string serialNum)
        => GetAsync<RetrieveSystemIdResponse>(BuildUrl("/api/v4/systems/retrieve_system_id",
            ("serial_num", serialNum)));

    public Task<GetSystemEventsResponse> GetSystemEventsAsync(int systemId, long startTime, long? endTime = null)
        => GetAsync<GetSystemEventsResponse>(BuildUrl($"/api/v4/systems/{systemId}/events",
            ("start_time", startTime.ToString()),
            ("end_time", endTime?.ToString())));

    public Task<GetSystemAlarmsResponse> GetSystemAlarmsAsync(int systemId, long startTime, long? endTime = null, bool? cleared = null)
        => GetAsync<GetSystemAlarmsResponse>(BuildUrl($"/api/v4/systems/{systemId}/alarms",
            ("start_time", startTime.ToString()),
            ("end_time", endTime?.ToString()),
            ("cleared", cleared?.ToString().ToLowerInvariant())));

    public Task<GetEventTypesResponse> GetEventTypesAsync(int? eventTypeId = null)
        => GetAsync<GetEventTypesResponse>(BuildUrl("/api/v4/systems/event_types",
            ("event_type_id", eventTypeId?.ToString())));

    public Task<GetOpenEventsResponse> GetOpenEventsAsync(int systemId)
        => GetAsync<GetOpenEventsResponse>($"/api/v4/systems/{systemId}/open_events");

    public Task<List<InvertersSummaryItem>> GetInvertersSummaryAsync(int? siteId = null, int? envoySerialNumber = null, int? page = null, int? size = null)
        => GetAsync<List<InvertersSummaryItem>>(BuildUrl("/api/v4/systems/inverters_summary_by_envoy_or_site",
            ("site_id", siteId?.ToString()),
            ("envoy_serial_number", envoySerialNumber?.ToString()),
            ("page", page?.ToString()),
            ("size", size?.ToString())));

    // === Production ===

    public Task<GetProductionMeterReadingsResponse> GetProductionMeterReadingsAsync(int systemId, long? endAt = null)
        => GetAsync<GetProductionMeterReadingsResponse>(BuildUrl($"/api/v4/systems/{systemId}/production_meter_readings",
            ("end_at", endAt?.ToString())));

    public Task<GetRgmStatsResponse> GetRgmStatsAsync(int systemId, long? startAt = null, long? endAt = null)
        => GetAsync<GetRgmStatsResponse>(BuildUrl($"/api/v4/systems/{systemId}/rgm_stats",
            ("start_at", startAt?.ToString()),
            ("end_at", endAt?.ToString())));

    public Task<GetEnergyLifetimeResponse> GetEnergyLifetimeAsync(int systemId, string? startDate = null, string? endDate = null, string? production = null)
        => GetAsync<GetEnergyLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/energy_lifetime",
            ("start_date", startDate),
            ("end_date", endDate),
            ("production", production)));

    public Task<GetProductionMicroTelemetryResponse> GetProductionMicroTelemetryAsync(int systemId, long? startAt = null, string? granularity = null)
        => GetAsync<GetProductionMicroTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/telemetry/production_micro",
            ("start_at", startAt?.ToString()),
            ("granularity", granularity)));

    public Task<GetProductionMeterTelemetryResponse> GetProductionMeterTelemetryAsync(int systemId, long? startAt = null, string? granularity = null)
        => GetAsync<GetProductionMeterTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/telemetry/production_meter",
            ("start_at", startAt?.ToString()),
            ("granularity", granularity)));

    // === Consumption ===

    public Task<GetConsumptionMeterReadingsResponse> GetConsumptionMeterReadingsAsync(int systemId, long? endAt = null)
        => GetAsync<GetConsumptionMeterReadingsResponse>(BuildUrl($"/api/v4/systems/{systemId}/consumption_meter_readings",
            ("end_at", endAt?.ToString())));

    public Task<GetStorageMeterReadingsResponse> GetStorageMeterReadingsAsync(int systemId, long? endAt = null)
        => GetAsync<GetStorageMeterReadingsResponse>(BuildUrl($"/api/v4/systems/{systemId}/storage_meter_readings",
            ("end_at", endAt?.ToString())));

    public Task<GetConsumptionLifetimeResponse> GetConsumptionLifetimeAsync(int systemId, string? startDate = null, string? endDate = null)
        => GetAsync<GetConsumptionLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/consumption_lifetime",
            ("start_date", startDate),
            ("end_date", endDate)));

    public Task<GetBatteryLifetimeResponse> GetBatteryLifetimeAsync(int systemId, string? startDate = null, string? endDate = null)
        => GetAsync<GetBatteryLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/battery_lifetime",
            ("start_date", startDate),
            ("end_date", endDate)));

    public Task<GetEnergyImportLifetimeResponse> GetEnergyImportLifetimeAsync(int systemId, string? startDate = null, string? endDate = null)
        => GetAsync<GetEnergyImportLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/energy_import_lifetime",
            ("start_date", startDate),
            ("end_date", endDate)));

    public Task<GetEnergyExportLifetimeResponse> GetEnergyExportLifetimeAsync(int systemId, string? startDate = null, string? endDate = null)
        => GetAsync<GetEnergyExportLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/energy_export_lifetime",
            ("start_date", startDate),
            ("end_date", endDate)));

    public Task<GetBatteryTelemetryResponse> GetBatteryTelemetryAsync(int systemId, long? startAt = null, string? granularity = null)
        => GetAsync<GetBatteryTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/telemetry/battery",
            ("start_at", startAt?.ToString()),
            ("granularity", granularity)));

    public Task<GetConsumptionMeterTelemetryResponse> GetConsumptionMeterTelemetryAsync(int systemId, long? startAt = null, string? granularity = null)
        => GetAsync<GetConsumptionMeterTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/telemetry/consumption_meter",
            ("start_at", startAt?.ToString()),
            ("granularity", granularity)));

    public Task<GetEnergyImportTelemetryResponse> GetEnergyImportTelemetryAsync(int systemId, long? startAt = null, string? granularity = null)
        => GetAsync<GetEnergyImportTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/energy_import_telemetry",
            ("start_at", startAt?.ToString()),
            ("granularity", granularity)));

    public Task<GetEnergyExportTelemetryResponse> GetEnergyExportTelemetryAsync(int systemId, long? startAt = null, string? granularity = null)
        => GetAsync<GetEnergyExportTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/energy_export_telemetry",
            ("start_at", startAt?.ToString()),
            ("granularity", granularity)));

    public Task<GetLatestTelemetryResponse> GetLatestTelemetryAsync(int systemId)
        => GetAsync<GetLatestTelemetryResponse>($"/api/v4/systems/{systemId}/latest_telemetry");

    // === Devices ===

    public Task<GetMicroTelemetryResponse> GetMicroTelemetryAsync(int systemId, string serialNo, long? startAt = null, string? granularity = null)
        => GetAsync<GetMicroTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/devices/micros/{serialNo}/telemetry",
            ("start_at", startAt?.ToString()),
            ("granularity", granularity)));

    public Task<GetEnchargeTelemetryResponse> GetEnchargeTelemetryAsync(int systemId, string serialNo, long? startAt = null, string? granularity = null)
        => GetAsync<GetEnchargeTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/devices/encharges/{serialNo}/telemetry",
            ("start_at", startAt?.ToString()),
            ("granularity", granularity)));

    public Task<GetEvseLifetimeResponse> GetEvseLifetimeAsync(int systemId, string serialNo, string? startDate = null, string? endDate = null)
        => GetAsync<GetEvseLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/{serialNo}/evse_lifetime",
            ("start_date", startDate),
            ("end_date", endDate)));

    public Task<GetEvseTelemetryResponse> GetEvseTelemtryAsync(int systemId, string serialNo, long? startAt = null, string? granularity = null, string? intervalDuration = null)
        => GetAsync<GetEvseTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/{serialNo}/evse_telemetry",
            ("start_at", startAt?.ToString()),
            ("granularity", granularity),
            ("interval_duration", intervalDuration)));

    public Task<GetHpLifetimeResponse> GetHpLifetimeAsync(int systemId, string? startDate = null, string? endDate = null)
        => GetAsync<GetHpLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/hp_lifetime",
            ("start_date", startDate),
            ("end_date", endDate)));

    public Task<GetHpTelemetryResponse> GetHpTelemetryAsync(int systemId, long? startAt = null, string? startDate = null, string? granularity = null, string? intervalDuration = null)
        => GetAsync<GetHpTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/hp_telemetry",
            ("start_at", startAt?.ToString()),
            ("start_date", startDate),
            ("granularity", granularity),
            ("interval_duration", intervalDuration)));

    // === Config ===

    public Task<BatterySettings> GetBatterySettingsAsync(int systemId)
        => GetAsync<BatterySettings>($"/api/v4/systems/config/{systemId}/battery_settings");

    public Task<BatterySettings> UpdateBatterySettingsAsync(int systemId, UpdateBatterySettingsRequest request)
        => PutAsync<BatterySettings>($"/api/v4/systems/config/{systemId}/battery_settings", request);

    public Task<StormGuardSettings> GetStormGuardAsync(int systemId)
        => GetAsync<StormGuardSettings>($"/api/v4/systems/config/{systemId}/storm_guard");

    public Task<GridStatus> GetGridStatusAsync(int systemId)
        => GetAsync<GridStatus>($"/api/v4/systems/config/{systemId}/grid_status");

    public Task<GetLoadControlResponse> GetLoadControlAsync(int systemId)
        => GetAsync<GetLoadControlResponse>($"/api/v4/systems/config/{systemId}/load_control");

    // === EV Charger ===

    public Task<EvChargerDevices> GetEvChargerDevicesAsync(int systemId)
        => GetAsync<EvChargerDevices>($"/api/v4/systems/{systemId}/ev_charger/devices");

    public Task<GetEvChargerEventsResponse> GetEvChargerEventsAsync(int systemId, int? offset = null, string? serialNum = null, int? limit = null)
        => GetAsync<GetEvChargerEventsResponse>(BuildUrl($"/api/v4/systems/{systemId}/ev_charger/events",
            ("offset", offset?.ToString()),
            ("serial_num", serialNum),
            ("limit", limit?.ToString())));

    public Task<GetEvChargerSessionsResponse> GetEvChargerSessionsAsync(int systemId, string serialNo, int? offset = null, int? limit = null)
        => GetAsync<GetEvChargerSessionsResponse>(BuildUrl($"/api/v4/systems/{systemId}/ev_charger/{serialNo}/sessions",
            ("offset", offset?.ToString()),
            ("limit", limit?.ToString())));

    public Task<GetEvChargerSchedulesResponse> GetEvChargerSchedulesAsync(int systemId, string serialNo)
        => GetAsync<GetEvChargerSchedulesResponse>($"/api/v4/systems/{systemId}/ev_charger/{serialNo}/schedules");

    public Task<GetEvChargerLifetimeResponse> GetEvChargerLifetimeAsync(int systemId, string serialNo, string startDate, string? endDate = null)
        => GetAsync<GetEvChargerLifetimeResponse>(BuildUrl($"/api/v4/systems/{systemId}/ev_charger/{serialNo}/lifetime",
            ("start_date", startDate),
            ("end_date", endDate)));

    public Task<GetEvChargerTelemetryResponse> GetEvChargerTelemetryAsync(int systemId, string serialNo, string? granularity = null, string? startDate = null, long? startAt = null)
        => GetAsync<GetEvChargerTelemetryResponse>(BuildUrl($"/api/v4/systems/{systemId}/ev_charger/{serialNo}/telemetry",
            ("granularity", granularity),
            ("start_date", startDate),
            ("start_at", startAt?.ToString())));

    public Task<ChargingCommandResponse> StartChargingAsync(int systemId, string serialNo, StartChargingRequest request)
        => PostAsync<ChargingCommandResponse>($"/api/v4/systems/{systemId}/ev_charger/{serialNo}/start_charging", request);

    public Task<ChargingCommandResponse> StopChargingAsync(int systemId, string serialNo)
        => PostAsync<ChargingCommandResponse>($"/api/v4/systems/{systemId}/ev_charger/{serialNo}/stop_charging");
}
