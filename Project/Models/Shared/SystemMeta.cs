using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Metadata about a system's reporting and operational state.
/// </summary>
public class SystemMeta
{
    /// <summary>Gets or sets the system operational status.</summary>
    [JsonPropertyName("status")] public string? Status { get; set; }

    /// <summary>Gets or sets the date and time of the most recent data report.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("last_report_at")] public DateTimeOffset? LastReportAt { get; set; }

    /// <summary>Gets or sets the date and time of the most recent energy production measurement.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("last_energy_at")] public DateTimeOffset? LastEnergyAt { get; set; }

    /// <summary>Gets or sets the date and time when the system first became operational.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("operational_at")] public DateTimeOffset? OperationalAt { get; set; }
}
