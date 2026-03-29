using System.Collections.Generic;
using System.Threading.Tasks;
using Shane32.EnphaseAPI.Models;

namespace Shane32.EnphaseAPI;

public interface IEnphaseClient
{
    string AccessToken { get; set; }

    // Systems
    Task<GetSystemsResponse> GetSystemsAsync(int? page = null, int? size = null, string? sortBy = null);
    Task<GetSystemsResponse> SearchSystemsAsync(SearchSystemsRequest request, int? page = null, int? size = null, bool? liveStream = null);
    Task<SystemInfo> GetSystemAsync(int systemId);
    Task<SystemSummary> GetSystemSummaryAsync(int systemId, string? summaryDate = null);
    Task<SystemDevices> GetSystemDevicesAsync(int systemId);
    Task<RetrieveSystemIdResponse> RetrieveSystemIdAsync(string serialNum);
    Task<GetSystemEventsResponse> GetSystemEventsAsync(int systemId, long startTime, long? endTime = null);
    Task<GetSystemAlarmsResponse> GetSystemAlarmsAsync(int systemId, long startTime, long? endTime = null, bool? cleared = null);
    Task<GetEventTypesResponse> GetEventTypesAsync(int? eventTypeId = null);
    Task<GetOpenEventsResponse> GetOpenEventsAsync(int systemId);
    Task<List<InvertersSummaryItem>> GetInvertersSummaryAsync(int? siteId = null, int? envoySerialNumber = null, int? page = null, int? size = null);

    // Production
    Task<GetProductionMeterReadingsResponse> GetProductionMeterReadingsAsync(int systemId, long? endAt = null);
    Task<GetRgmStatsResponse> GetRgmStatsAsync(int systemId, long? startAt = null, long? endAt = null);
    Task<GetEnergyLifetimeResponse> GetEnergyLifetimeAsync(int systemId, string? startDate = null, string? endDate = null, string? production = null);
    Task<GetProductionMicroTelemetryResponse> GetProductionMicroTelemetryAsync(int systemId, long? startAt = null, string? granularity = null);
    Task<GetProductionMeterTelemetryResponse> GetProductionMeterTelemetryAsync(int systemId, long? startAt = null, string? granularity = null);

    // Consumption
    Task<GetConsumptionMeterReadingsResponse> GetConsumptionMeterReadingsAsync(int systemId, long? endAt = null);
    Task<GetStorageMeterReadingsResponse> GetStorageMeterReadingsAsync(int systemId, long? endAt = null);
    Task<GetConsumptionLifetimeResponse> GetConsumptionLifetimeAsync(int systemId, string? startDate = null, string? endDate = null);
    Task<GetBatteryLifetimeResponse> GetBatteryLifetimeAsync(int systemId, string? startDate = null, string? endDate = null);
    Task<GetEnergyImportLifetimeResponse> GetEnergyImportLifetimeAsync(int systemId, string? startDate = null, string? endDate = null);
    Task<GetEnergyExportLifetimeResponse> GetEnergyExportLifetimeAsync(int systemId, string? startDate = null, string? endDate = null);
    Task<GetBatteryTelemetryResponse> GetBatteryTelemetryAsync(int systemId, long? startAt = null, string? granularity = null);
    Task<GetConsumptionMeterTelemetryResponse> GetConsumptionMeterTelemetryAsync(int systemId, long? startAt = null, string? granularity = null);
    Task<GetEnergyImportTelemetryResponse> GetEnergyImportTelemetryAsync(int systemId, long? startAt = null, string? granularity = null);
    Task<GetEnergyExportTelemetryResponse> GetEnergyExportTelemetryAsync(int systemId, long? startAt = null, string? granularity = null);
    Task<GetLatestTelemetryResponse> GetLatestTelemetryAsync(int systemId);

    // Devices
    Task<GetMicroTelemetryResponse> GetMicroTelemetryAsync(int systemId, string serialNo, long? startAt = null, string? granularity = null);
    Task<GetEnchargeTelemetryResponse> GetEnchargeTelemetryAsync(int systemId, string serialNo, long? startAt = null, string? granularity = null);
    Task<GetEvseLifetimeResponse> GetEvseLifetimeAsync(int systemId, string serialNo, string? startDate = null, string? endDate = null);
    Task<GetEvseTelemetryResponse> GetEvseTelemtryAsync(int systemId, string serialNo, long? startAt = null, string? granularity = null, string? intervalDuration = null);
    Task<GetHpLifetimeResponse> GetHpLifetimeAsync(int systemId, string? startDate = null, string? endDate = null);
    Task<GetHpTelemetryResponse> GetHpTelemetryAsync(int systemId, long? startAt = null, string? startDate = null, string? granularity = null, string? intervalDuration = null);

    // Config
    Task<BatterySettings> GetBatterySettingsAsync(int systemId);
    Task<BatterySettings> UpdateBatterySettingsAsync(int systemId, UpdateBatterySettingsRequest request);
    Task<StormGuardSettings> GetStormGuardAsync(int systemId);
    Task<GridStatus> GetGridStatusAsync(int systemId);
    Task<GetLoadControlResponse> GetLoadControlAsync(int systemId);

    // EV Charger
    Task<EvChargerDevices> GetEvChargerDevicesAsync(int systemId);
    Task<GetEvChargerEventsResponse> GetEvChargerEventsAsync(int systemId, int? offset = null, string? serialNum = null, int? limit = null);
    Task<GetEvChargerSessionsResponse> GetEvChargerSessionsAsync(int systemId, string serialNo, int? offset = null, int? limit = null);
    Task<GetEvChargerSchedulesResponse> GetEvChargerSchedulesAsync(int systemId, string serialNo);
    Task<GetEvChargerLifetimeResponse> GetEvChargerLifetimeAsync(int systemId, string serialNo, string startDate, string? endDate = null);
    Task<GetEvChargerTelemetryResponse> GetEvChargerTelemetryAsync(int systemId, string serialNo, string? granularity = null, string? startDate = null, long? startAt = null);
    Task<ChargingCommandResponse> StartChargingAsync(int systemId, string serialNo, StartChargingRequest request);
    Task<ChargingCommandResponse> StopChargingAsync(int systemId, string serialNo);
}
