using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Latest telemetry data for an Encharge battery device.
/// </summary>
public class LatestTelemetryEncharge
{
    /// <summary>Gets or sets the device identifier.</summary>
    [JsonPropertyName("id")] public long? Id { get; set; }

    /// <summary>Gets or sets the human-readable device name.</summary>
    [JsonPropertyName("name")] public string? Name { get; set; }

    /// <summary>Gets or sets the channel number of the device.</summary>
    [JsonPropertyName("channel")] public int? Channel { get; set; }

    /// <summary>Gets or sets the date and time of the most recent report from this device.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("last_report_at")] public DateTimeOffset? LastReportAt { get; set; }

    /// <summary>Gets or sets the current power output in watts.</summary>
    [JsonPropertyName("power")] public long? Power { get; set; }

    /// <summary>Gets or sets the current operational mode of the device.</summary>
    [JsonPropertyName("operational_mode")] public string? OperationalMode { get; set; }
}
