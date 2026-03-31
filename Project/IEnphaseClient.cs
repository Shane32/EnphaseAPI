using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shane32.EnphaseAPI.Models;

namespace Shane32.EnphaseAPI;

/// <summary>
/// Client interface for the Enphase solar energy API.
/// </summary>
public interface IEnphaseClient
{
    /// <summary>Gets or sets the OAuth 2.0 bearer access token used to authenticate requests.</summary>
    public string AccessToken { get; set; }

    // Systems

    /// <summary>Returns a paginated list of systems the caller has access to.</summary>
    /// <param name="page">One-based page number for pagination.</param>
    /// <param name="size">Number of systems per page.</param>
    /// <param name="sortBy">Field to sort results by.</param>
    /// <returns>A <see cref="GetSystemsResponse"/> containing the list of systems.</returns>
    public Task<GetSystemsResponse> GetSystemsAsync(int? page = null, int? size = null, string? sortBy = null);

    /// <summary>Searches for systems matching the supplied filter criteria.</summary>
    /// <param name="request">Search criteria and optional sort order.</param>
    /// <param name="page">One-based page number for pagination.</param>
    /// <param name="size">Number of systems per page.</param>
    /// <param name="liveStream">When <see langword="true"/>, restricts results to live-stream-enabled systems.</param>
    /// <returns>A <see cref="GetSystemsResponse"/> containing the matching systems.</returns>
    public Task<GetSystemsResponse> SearchSystemsAsync(SearchSystemsRequest request, int? page = null, int? size = null, bool? liveStream = null);

    /// <summary>Returns details for a single system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <returns>A <see cref="SystemInfo"/> for the requested system.</returns>
    public Task<SystemInfo> GetSystemAsync(int systemId);

    /// <summary>Returns a summary of energy production for a system on a given date.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="summaryDate">The date to summarize. Defaults to today when omitted.</param>
    /// <returns>A <see cref="SystemSummary"/> for the requested system and date.</returns>
    public Task<SystemSummary> GetSystemSummaryAsync(int systemId, DateTimeOffset? summaryDate = null);

    /// <summary>Returns the devices associated with a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <returns>A <see cref="SystemDevices"/> containing device information.</returns>
    public Task<SystemDevices> GetSystemDevicesAsync(int systemId);

    /// <summary>Retrieves a system ID by Envoy serial number.</summary>
    /// <param name="serialNum">The Envoy serial number.</param>
    /// <returns>A <see cref="RetrieveSystemIdResponse"/> containing the system ID.</returns>
    public Task<RetrieveSystemIdResponse> RetrieveSystemIdAsync(string serialNum);

    /// <summary>Returns events that occurred on a system within the specified time range.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startTime">Start of the time range.</param>
    /// <param name="endTime">End of the time range. Defaults to the current time when omitted.</param>
    /// <returns>A <see cref="GetSystemEventsResponse"/> containing the events.</returns>
    public Task<GetSystemEventsResponse> GetSystemEventsAsync(int systemId, DateTimeOffset startTime, DateTimeOffset? endTime = null);

    /// <summary>Returns alarms that occurred on a system within the specified time range.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startTime">Start of the time range.</param>
    /// <param name="endTime">End of the time range. Defaults to the current time when omitted.</param>
    /// <param name="cleared">When specified, filters alarms by their cleared status.</param>
    /// <returns>A <see cref="GetSystemAlarmsResponse"/> containing the alarms.</returns>
    public Task<GetSystemAlarmsResponse> GetSystemAlarmsAsync(int systemId, DateTimeOffset startTime, DateTimeOffset? endTime = null, bool? cleared = null);

    /// <summary>Returns the available event types, optionally filtered by ID.</summary>
    /// <param name="eventTypeId">When specified, returns only the event type with this ID.</param>
    /// <returns>A <see cref="GetEventTypesResponse"/> containing the event types.</returns>
    public Task<GetEventTypesResponse> GetEventTypesAsync(int? eventTypeId = null);

    /// <summary>Returns currently open (uncleared) events for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <returns>A <see cref="GetOpenEventsResponse"/> containing open events.</returns>
    public Task<GetOpenEventsResponse> GetOpenEventsAsync(int systemId);

    /// <summary>Returns a summary of all micro-inverters grouped by Envoy, optionally filtered by site or Envoy.</summary>
    /// <param name="siteId">Filter by site (system) ID.</param>
    /// <param name="envoySerialNumber">Filter by Envoy serial number.</param>
    /// <param name="page">One-based page number for pagination.</param>
    /// <param name="size">Number of items per page.</param>
    /// <returns>A list of <see cref="InvertersSummaryItem"/> objects.</returns>
    public Task<List<InvertersSummaryItem>> GetInvertersSummaryAsync(int? siteId = null, int? envoySerialNumber = null, int? page = null, int? size = null);

    // Production

    /// <summary>Returns the most recent production meter readings for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="endAt">Return readings up to this point in time. Defaults to the current time when omitted.</param>
    /// <returns>A <see cref="GetProductionMeterReadingsResponse"/> containing meter readings.</returns>
    public Task<GetProductionMeterReadingsResponse> GetProductionMeterReadingsAsync(int systemId, DateTimeOffset? endAt = null);

    /// <summary>Returns revenue-grade meter (RGM) statistics for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startAt">Start of the time range.</param>
    /// <param name="endAt">End of the time range.</param>
    /// <returns>A <see cref="GetRgmStatsResponse"/> containing RGM interval data.</returns>
    public Task<GetRgmStatsResponse> GetRgmStatsAsync(int systemId, DateTimeOffset? startAt = null, DateTimeOffset? endAt = null);

    /// <summary>Returns daily lifetime energy production values for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startDate">Start date of the range.</param>
    /// <param name="endDate">End date of the range.</param>
    /// <param name="production">When set to <c>"all"</c>, includes all production sources.</param>
    /// <returns>A <see cref="GetEnergyLifetimeResponse"/> containing daily production in Wh.</returns>
    public Task<GetEnergyLifetimeResponse> GetEnergyLifetimeAsync(int systemId, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, string? production = null);

    /// <summary>Returns micro-inverter production telemetry intervals for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startAt">Start of the telemetry window.</param>
    /// <param name="granularity">Interval granularity (e.g. <c>"week"</c>, <c>"day"</c>, <c>"15mins"</c>).</param>
    /// <returns>A <see cref="GetProductionMicroTelemetryResponse"/> containing telemetry intervals.</returns>
    public Task<GetProductionMicroTelemetryResponse> GetProductionMicroTelemetryAsync(int systemId, DateTimeOffset? startAt = null, string? granularity = null);

    /// <summary>Returns production meter telemetry intervals for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startAt">Start of the telemetry window.</param>
    /// <param name="granularity">Interval granularity (e.g. <c>"week"</c>, <c>"day"</c>, <c>"15mins"</c>).</param>
    /// <returns>A <see cref="GetProductionMeterTelemetryResponse"/> containing telemetry intervals.</returns>
    public Task<GetProductionMeterTelemetryResponse> GetProductionMeterTelemetryAsync(int systemId, DateTimeOffset? startAt = null, string? granularity = null);

    // Consumption

    /// <summary>Returns the most recent consumption meter readings for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="endAt">Return readings up to this point in time.</param>
    /// <returns>A <see cref="GetConsumptionMeterReadingsResponse"/> containing meter readings.</returns>
    public Task<GetConsumptionMeterReadingsResponse> GetConsumptionMeterReadingsAsync(int systemId, DateTimeOffset? endAt = null);

    /// <summary>Returns the most recent storage meter readings for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="endAt">Return readings up to this point in time.</param>
    /// <returns>A <see cref="GetStorageMeterReadingsResponse"/> containing meter readings.</returns>
    public Task<GetStorageMeterReadingsResponse> GetStorageMeterReadingsAsync(int systemId, DateTimeOffset? endAt = null);

    /// <summary>Returns daily lifetime energy consumption values for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startDate">Start date of the range.</param>
    /// <param name="endDate">End date of the range.</param>
    /// <returns>A <see cref="GetConsumptionLifetimeResponse"/> containing daily consumption in Wh.</returns>
    public Task<GetConsumptionLifetimeResponse> GetConsumptionLifetimeAsync(int systemId, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null);

    /// <summary>Returns daily lifetime battery charge and discharge values for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startDate">Start date of the range.</param>
    /// <param name="endDate">End date of the range.</param>
    /// <returns>A <see cref="GetBatteryLifetimeResponse"/> containing daily charge/discharge in Wh.</returns>
    public Task<GetBatteryLifetimeResponse> GetBatteryLifetimeAsync(int systemId, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null);

    /// <summary>Returns daily lifetime energy import values for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startDate">Start date of the range.</param>
    /// <param name="endDate">End date of the range.</param>
    /// <returns>A <see cref="GetEnergyImportLifetimeResponse"/> containing daily import in Wh.</returns>
    public Task<GetEnergyImportLifetimeResponse> GetEnergyImportLifetimeAsync(int systemId, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null);

    /// <summary>Returns daily lifetime energy export values for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startDate">Start date of the range.</param>
    /// <param name="endDate">End date of the range.</param>
    /// <returns>A <see cref="GetEnergyExportLifetimeResponse"/> containing daily export in Wh.</returns>
    public Task<GetEnergyExportLifetimeResponse> GetEnergyExportLifetimeAsync(int systemId, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null);

    /// <summary>Returns battery telemetry intervals for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startAt">Start of the telemetry window.</param>
    /// <param name="granularity">Interval granularity (e.g. <c>"week"</c>, <c>"day"</c>, <c>"15mins"</c>).</param>
    /// <returns>A <see cref="GetBatteryTelemetryResponse"/> containing battery telemetry intervals.</returns>
    public Task<GetBatteryTelemetryResponse> GetBatteryTelemetryAsync(int systemId, DateTimeOffset? startAt = null, string? granularity = null);

    /// <summary>Returns consumption meter telemetry intervals for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startAt">Start of the telemetry window.</param>
    /// <param name="granularity">Interval granularity (e.g. <c>"week"</c>, <c>"day"</c>, <c>"15mins"</c>).</param>
    /// <returns>A <see cref="GetConsumptionMeterTelemetryResponse"/> containing telemetry intervals.</returns>
    public Task<GetConsumptionMeterTelemetryResponse> GetConsumptionMeterTelemetryAsync(int systemId, DateTimeOffset? startAt = null, string? granularity = null);

    /// <summary>Returns energy import telemetry intervals for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startAt">Start of the telemetry window.</param>
    /// <param name="granularity">Interval granularity (e.g. <c>"week"</c>, <c>"day"</c>, <c>"15mins"</c>).</param>
    /// <returns>A <see cref="GetEnergyImportTelemetryResponse"/> containing import telemetry intervals.</returns>
    public Task<GetEnergyImportTelemetryResponse> GetEnergyImportTelemetryAsync(int systemId, DateTimeOffset? startAt = null, string? granularity = null);

    /// <summary>Returns energy export telemetry intervals for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startAt">Start of the telemetry window.</param>
    /// <param name="granularity">Interval granularity (e.g. <c>"week"</c>, <c>"day"</c>, <c>"15mins"</c>).</param>
    /// <returns>A <see cref="GetEnergyExportTelemetryResponse"/> containing export telemetry intervals.</returns>
    public Task<GetEnergyExportTelemetryResponse> GetEnergyExportTelemetryAsync(int systemId, DateTimeOffset? startAt = null, string? granularity = null);

    /// <summary>Returns the most recent telemetry data point for all devices on a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <returns>A <see cref="GetLatestTelemetryResponse"/> containing the latest readings.</returns>
    public Task<GetLatestTelemetryResponse> GetLatestTelemetryAsync(int systemId);

    // Devices

    /// <summary>Returns telemetry intervals for a specific micro-inverter.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="serialNo">The micro-inverter serial number.</param>
    /// <param name="startAt">Start of the telemetry window.</param>
    /// <param name="granularity">Interval granularity (e.g. <c>"week"</c>, <c>"day"</c>, <c>"15mins"</c>).</param>
    /// <returns>A <see cref="GetMicroTelemetryResponse"/> containing telemetry intervals.</returns>
    public Task<GetMicroTelemetryResponse> GetMicroTelemetryAsync(int systemId, string serialNo, DateTimeOffset? startAt = null, string? granularity = null);

    /// <summary>Returns telemetry intervals for a specific Encharge battery device.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="serialNo">The Encharge serial number.</param>
    /// <param name="startAt">Start of the telemetry window.</param>
    /// <param name="granularity">Interval granularity (e.g. <c>"week"</c>, <c>"day"</c>, <c>"15mins"</c>).</param>
    /// <returns>A <see cref="GetEnchargeTelemetryResponse"/> containing telemetry intervals.</returns>
    public Task<GetEnchargeTelemetryResponse> GetEnchargeTelemetryAsync(int systemId, string serialNo, DateTimeOffset? startAt = null, string? granularity = null);

    /// <summary>Returns daily lifetime energy consumption for a specific EVSE device.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="serialNo">The EVSE serial number.</param>
    /// <param name="startDate">Start date of the range.</param>
    /// <param name="endDate">End date of the range.</param>
    /// <returns>A <see cref="GetEvseLifetimeResponse"/> containing daily consumption in Wh.</returns>
    public Task<GetEvseLifetimeResponse> GetEvseLifetimeAsync(int systemId, string serialNo, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null);

    /// <summary>Returns telemetry intervals for a specific EVSE device.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="serialNo">The EVSE serial number.</param>
    /// <param name="startAt">Start of the telemetry window.</param>
    /// <param name="granularity">Interval granularity (e.g. <c>"week"</c>, <c>"day"</c>, <c>"15mins"</c>).</param>
    /// <param name="intervalDuration">Duration of each interval in minutes.</param>
    /// <returns>A <see cref="GetEvseTelemetryResponse"/> containing telemetry intervals.</returns>
    public Task<GetEvseTelemetryResponse> GetEvseTelemetryAsync(int systemId, string serialNo, DateTimeOffset? startAt = null, string? granularity = null, string? intervalDuration = null);

    /// <summary>Returns daily lifetime energy consumption for heat pump devices on a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startDate">Start date of the range.</param>
    /// <param name="endDate">End date of the range.</param>
    /// <returns>A <see cref="GetHpLifetimeResponse"/> containing daily consumption in Wh.</returns>
    public Task<GetHpLifetimeResponse> GetHpLifetimeAsync(int systemId, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null);

    /// <summary>Returns telemetry intervals for heat pump devices on a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="startAt">Start of the telemetry window.</param>
    /// <param name="startDate">Start date of the telemetry window.</param>
    /// <param name="granularity">Interval granularity (e.g. <c>"week"</c>, <c>"day"</c>, <c>"15mins"</c>).</param>
    /// <param name="intervalDuration">Duration of each interval in minutes.</param>
    /// <returns>A <see cref="GetHpTelemetryResponse"/> containing telemetry intervals.</returns>
    public Task<GetHpTelemetryResponse> GetHpTelemetryAsync(int systemId, DateTimeOffset? startAt = null, DateTimeOffset? startDate = null, string? granularity = null, string? intervalDuration = null);

    // Config

    /// <summary>Returns the battery settings for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <returns>A <see cref="BatterySettings"/> containing the current battery configuration.</returns>
    public Task<BatterySettings> GetBatterySettingsAsync(int systemId);

    /// <summary>Updates the battery settings for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="request">The battery settings to apply.</param>
    /// <returns>A <see cref="BatterySettings"/> reflecting the updated configuration.</returns>
    public Task<BatterySettings> UpdateBatterySettingsAsync(int systemId, UpdateBatterySettingsRequest request);

    /// <summary>Returns the Storm Guard settings for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <returns>A <see cref="StormGuardSettings"/> containing the Storm Guard status.</returns>
    public Task<StormGuardSettings> GetStormGuardAsync(int systemId);

    /// <summary>Returns the current grid status for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <returns>A <see cref="GridStatus"/> containing the grid connection state.</returns>
    public Task<GridStatus> GetGridStatusAsync(int systemId);

    /// <summary>Returns the load control configuration for a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <returns>A <see cref="GetLoadControlResponse"/> containing load control channel data.</returns>
    public Task<GetLoadControlResponse> GetLoadControlAsync(int systemId);

    // EV Charger

    /// <summary>Returns EV charger devices associated with a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <returns>An <see cref="EvChargerDevices"/> containing device information.</returns>
    public Task<EvChargerDevices> GetEvChargerDevicesAsync(int systemId);

    /// <summary>Returns events for EV chargers on a system.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="offset">Zero-based offset for pagination.</param>
    /// <param name="serialNum">Filter events to a specific charger serial number.</param>
    /// <param name="limit">Maximum number of events to return.</param>
    /// <returns>A <see cref="GetEvChargerEventsResponse"/> containing the events.</returns>
    public Task<GetEvChargerEventsResponse> GetEvChargerEventsAsync(int systemId, int? offset = null, string? serialNum = null, int? limit = null);

    /// <summary>Returns charging sessions for a specific EV charger.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="serialNo">The EV charger serial number.</param>
    /// <param name="offset">Zero-based offset for pagination.</param>
    /// <param name="limit">Maximum number of sessions to return.</param>
    /// <returns>A <see cref="GetEvChargerSessionsResponse"/> containing charging sessions.</returns>
    public Task<GetEvChargerSessionsResponse> GetEvChargerSessionsAsync(int systemId, string serialNo, int? offset = null, int? limit = null);

    /// <summary>Returns the charging schedules configured for a specific EV charger.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="serialNo">The EV charger serial number.</param>
    /// <returns>A <see cref="GetEvChargerSchedulesResponse"/> containing the schedules.</returns>
    public Task<GetEvChargerSchedulesResponse> GetEvChargerSchedulesAsync(int systemId, string serialNo);

    /// <summary>Returns daily lifetime energy consumption for a specific EV charger.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="serialNo">The EV charger serial number.</param>
    /// <param name="startDate">Start date of the range.</param>
    /// <param name="endDate">End date of the range.</param>
    /// <returns>A <see cref="GetEvChargerLifetimeResponse"/> containing daily consumption in Wh.</returns>
    public Task<GetEvChargerLifetimeResponse> GetEvChargerLifetimeAsync(int systemId, string serialNo, DateTimeOffset startDate, DateTimeOffset? endDate = null);

    /// <summary>Returns telemetry intervals for a specific EV charger.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="serialNo">The EV charger serial number.</param>
    /// <param name="granularity">Interval granularity (e.g. <c>"week"</c>, <c>"day"</c>, <c>"15mins"</c>).</param>
    /// <param name="startDate">Start date of the telemetry window.</param>
    /// <param name="startAt">Start of the telemetry window.</param>
    /// <returns>A <see cref="GetEvChargerTelemetryResponse"/> containing telemetry intervals.</returns>
    public Task<GetEvChargerTelemetryResponse> GetEvChargerTelemetryAsync(int systemId, string serialNo, string? granularity = null, DateTimeOffset? startDate = null, DateTimeOffset? startAt = null);

    /// <summary>Sends a command to start charging on a specific EV charger.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="serialNo">The EV charger serial number.</param>
    /// <param name="request">Parameters for the start charging command.</param>
    /// <returns>A <see cref="ChargingCommandResponse"/> confirming the command.</returns>
    public Task<ChargingCommandResponse> StartChargingAsync(int systemId, string serialNo, StartChargingRequest request);

    /// <summary>Sends a command to stop charging on a specific EV charger.</summary>
    /// <param name="systemId">The system identifier.</param>
    /// <param name="serialNo">The EV charger serial number.</param>
    /// <returns>A <see cref="ChargingCommandResponse"/> confirming the command.</returns>
    public Task<ChargingCommandResponse> StopChargingAsync(int systemId, string serialNo);
}
