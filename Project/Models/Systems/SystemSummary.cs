using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Summary of a system's current energy production and configuration.
/// </summary>
public class SystemSummary
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the current power output of the system in W.</summary>
    [JsonPropertyName("current_power")] public long? CurrentPower { get; set; }

    /// <summary>Gets or sets the total lifetime energy produced by the system in Wh.</summary>
    [JsonPropertyName("energy_lifetime")] public long? EnergyLifetime { get; set; }

    /// <summary>Gets or sets the energy produced today in Wh.</summary>
    [JsonPropertyName("energy_today")] public long? EnergyToday { get; set; }

    /// <summary>Gets or sets the date and time at which the last telemetry interval ended.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("last_interval_end_at")] public DateTimeOffset? LastIntervalEndAt { get; set; }

    /// <summary>Gets or sets the date and time of the most recent data report.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("last_report_at")] public DateTimeOffset? LastReportAt { get; set; }

    /// <summary>Gets or sets the total number of installed modules (micro-inverters).</summary>
    [JsonPropertyName("modules")] public int? Modules { get; set; }

    /// <summary>Gets or sets the date and time when the system first became operational.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("operational_at")] public DateTimeOffset? OperationalAt { get; set; }

    /// <summary>Gets or sets the total installed system size in W.</summary>
    [JsonPropertyName("size_w")] public int? SizeW { get; set; }

    /// <summary>Gets or sets the National Meter Identifier (NMI) for the system.</summary>
    [JsonPropertyName("nmi")] public string? Nmi { get; set; }

    /// <summary>Gets or sets the data source for this summary.</summary>
    [JsonPropertyName("source")] public string? Source { get; set; }

    /// <summary>Gets or sets the operational status of the system.</summary>
    [JsonPropertyName("status")] public string? Status { get; set; }

    /// <summary>Gets or sets the date this summary covers.</summary>
    [JsonConverter(typeof(NullableDateTimeOffsetDateConverter))]
    [JsonPropertyName("summary_date")] public DateTimeOffset? SummaryDate { get; set; }

    /// <summary>Gets or sets the current battery charging rate in W.</summary>
    [JsonPropertyName("battery_charge_w")] public long? BatteryChargeW { get; set; }

    /// <summary>Gets or sets the current battery discharging rate in W.</summary>
    [JsonPropertyName("battery_discharge_w")] public long? BatteryDischargeW { get; set; }

    /// <summary>Gets or sets the total usable battery capacity in Wh.</summary>
    [JsonPropertyName("battery_capacity_wh")] public long? BatteryCapacityWh { get; set; }

    /// <summary>Gets or sets the raw JSON value representing the EVSE power reading.</summary>
    [JsonPropertyName("evse_power")] public JsonElement? EvsePower { get; set; }
}
