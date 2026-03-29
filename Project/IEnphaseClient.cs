using System.Collections.Generic;
using System.Threading.Tasks;
using Shane32.EnphaseAPI.Models;

namespace Shane32.EnphaseAPI;

public interface IEnphaseClient
{
    public string AccessToken { get; set; }

    // Systems
    public Task<GetSystemsResponse> GetSystemsAsync(int? page = null, int? size = null, string? sortBy = null);
    public Task<GetSystemsResponse> SearchSystemsAsync(SearchSystemsRequest request, int? page = null, int? size = null, bool? liveStream = null);
    public Task<SystemInfo> GetSystemAsync(int systemId);
    public Task<SystemSummary> GetSystemSummaryAsync(int systemId, string? summaryDate = null);
    public Task<SystemDevices> GetSystemDevicesAsync(int systemId);
    public Task<RetrieveSystemIdResponse> RetrieveSystemIdAsync(string serialNum);
    public Task<GetSystemEventsResponse> GetSystemEventsAsync(int systemId, long startTime, long? endTime = null);
    public Task<GetSystemAlarmsResponse> GetSystemAlarmsAsync(int systemId, long startTime, long? endTime = null, bool? cleared = null);
    public Task<GetEventTypesResponse> GetEventTypesAsync(int? eventTypeId = null);
    public Task<GetOpenEventsResponse> GetOpenEventsAsync(int systemId);
    public Task<List<InvertersSummaryItem>> GetInvertersSummaryAsync(int? siteId = null, int? envoySerialNumber = null, int? page = null, int? size = null);

    // Production
    public Task<GetProductionMeterReadingsResponse> GetProductionMeterReadingsAsync(int systemId, long? endAt = null);
    public Task<GetRgmStatsResponse> GetRgmStatsAsync(int systemId, long? startAt = null, long? endAt = null);
    public Task<GetEnergyLifetimeResponse> GetEnergyLifetimeAsync(int systemId, string? startDate = null, string? endDate = null, string? production = null);
    public Task<GetProductionMicroTelemetryResponse> GetProductionMicroTelemetryAsync(int systemId, long? startAt = null, string? granularity = null);
    public Task<GetProductionMeterTelemetryResponse> GetProductionMeterTelemetryAsync(int systemId, long? startAt = null, string? granularity = null);

    // Consumption
    public Task<GetConsumptionMeterReadingsResponse> GetConsumptionMeterReadingsAsync(int systemId, long? endAt = null);
    public Task<GetStorageMeterReadingsResponse> GetStorageMeterReadingsAsync(int systemId, long? endAt = null);
    public Task<GetConsumptionLifetimeResponse> GetConsumptionLifetimeAsync(int systemId, string? startDate = null, string? endDate = null);
    public Task<GetBatteryLifetimeResponse> GetBatteryLifetimeAsync(int systemId, string? startDate = null, string? endDate = null);
    public Task<GetEnergyImportLifetimeResponse> GetEnergyImportLifetimeAsync(int systemId, string? startDate = null, string? endDate = null);
    public Task<GetEnergyExportLifetimeResponse> GetEnergyExportLifetimeAsync(int systemId, string? startDate = null, string? endDate = null);
    public Task<GetBatteryTelemetryResponse> GetBatteryTelemetryAsync(int systemId, long? startAt = null, string? granularity = null);
    public Task<GetConsumptionMeterTelemetryResponse> GetConsumptionMeterTelemetryAsync(int systemId, long? startAt = null, string? granularity = null);
    public Task<GetEnergyImportTelemetryResponse> GetEnergyImportTelemetryAsync(int systemId, long? startAt = null, string? granularity = null);
    public Task<GetEnergyExportTelemetryResponse> GetEnergyExportTelemetryAsync(int systemId, long? startAt = null, string? granularity = null);
    public Task<GetLatestTelemetryResponse> GetLatestTelemetryAsync(int systemId);

    // Devices
    public Task<GetMicroTelemetryResponse> GetMicroTelemetryAsync(int systemId, string serialNo, long? startAt = null, string? granularity = null);
    public Task<GetEnchargeTelemetryResponse> GetEnchargeTelemetryAsync(int systemId, string serialNo, long? startAt = null, string? granularity = null);
    public Task<GetEvseLifetimeResponse> GetEvseLifetimeAsync(int systemId, string serialNo, string? startDate = null, string? endDate = null);
    public Task<GetEvseTelemetryResponse> GetEvseTelemetryAsync(int systemId, string serialNo, long? startAt = null, string? granularity = null, string? intervalDuration = null);
    public Task<GetHpLifetimeResponse> GetHpLifetimeAsync(int systemId, string? startDate = null, string? endDate = null);
    public Task<GetHpTelemetryResponse> GetHpTelemetryAsync(int systemId, long? startAt = null, string? startDate = null, string? granularity = null, string? intervalDuration = null);

    // Config
    public Task<BatterySettings> GetBatterySettingsAsync(int systemId);
    public Task<BatterySettings> UpdateBatterySettingsAsync(int systemId, UpdateBatterySettingsRequest request);
    public Task<StormGuardSettings> GetStormGuardAsync(int systemId);
    public Task<GridStatus> GetGridStatusAsync(int systemId);
    public Task<GetLoadControlResponse> GetLoadControlAsync(int systemId);

    // EV Charger
    public Task<EvChargerDevices> GetEvChargerDevicesAsync(int systemId);
    public Task<GetEvChargerEventsResponse> GetEvChargerEventsAsync(int systemId, int? offset = null, string? serialNum = null, int? limit = null);
    public Task<GetEvChargerSessionsResponse> GetEvChargerSessionsAsync(int systemId, string serialNo, int? offset = null, int? limit = null);
    public Task<GetEvChargerSchedulesResponse> GetEvChargerSchedulesAsync(int systemId, string serialNo);
    public Task<GetEvChargerLifetimeResponse> GetEvChargerLifetimeAsync(int systemId, string serialNo, string startDate, string? endDate = null);
    public Task<GetEvChargerTelemetryResponse> GetEvChargerTelemetryAsync(int systemId, string serialNo, string? granularity = null, string? startDate = null, long? startAt = null);
    public Task<ChargingCommandResponse> StartChargingAsync(int systemId, string serialNo, StartChargingRequest request);
    public Task<ChargingCommandResponse> StopChargingAsync(int systemId, string serialNo);
}
