using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Battery telemetry data for a system.
/// </summary>
public class GetBatteryTelemetryResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the granularity of the telemetry intervals (e.g. "week", "day", "15mins").</summary>
    [JsonPropertyName("granularity")] public string? Granularity { get; set; }

    /// <summary>Gets or sets the total number of battery devices.</summary>
    [JsonPropertyName("total_devices")] public int? TotalDevices { get; set; }

    /// <summary>Gets or sets the Unix timestamp of the first interval in the response.</summary>
    [JsonPropertyName("start_at")] public long? StartAt { get; set; }

    /// <summary>Gets or sets the Unix timestamp of the last interval in the response.</summary>
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }

    /// <summary>Gets or sets the type of items contained in the intervals list.</summary>
    [JsonPropertyName("items")] public string? Items { get; set; }

    /// <summary>Gets or sets the list of battery telemetry intervals.</summary>
    [JsonPropertyName("intervals")] public List<BatteryTelemetryInterval>? Intervals { get; set; }

    /// <summary>Gets or sets the most recently reported aggregate state-of-charge across all batteries.</summary>
    [JsonPropertyName("last_reported_aggregate_soc")] public string? LastReportedAggregateSoc { get; set; }
}
