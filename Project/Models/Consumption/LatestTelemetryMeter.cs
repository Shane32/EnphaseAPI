using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Latest telemetry data for a meter device.
/// </summary>
public class LatestTelemetryMeter
{
    /// <summary>Gets or sets the device identifier.</summary>
    [JsonPropertyName("id")] public long? Id { get; set; }

    /// <summary>Gets or sets the human-readable device name.</summary>
    [JsonPropertyName("name")] public string? Name { get; set; }

    /// <summary>Gets or sets the channel number of the meter.</summary>
    [JsonPropertyName("channel")] public int? Channel { get; set; }

    /// <summary>Gets or sets the Unix timestamp of the most recent report from this device.</summary>
    [JsonPropertyName("last_report_at")] public long? LastReportAt { get; set; }

    /// <summary>Gets or sets the current power reading in watts.</summary>
    [JsonPropertyName("power")] public long? Power { get; set; }
}
