using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Production meter telemetry data for a system.
/// </summary>
public class GetProductionMeterTelemetryResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the granularity of the telemetry intervals (e.g. "week", "day", "15mins").</summary>
    [JsonPropertyName("granularity")] public string? Granularity { get; set; }

    /// <summary>Gets or sets the total number of production meter devices.</summary>
    [JsonPropertyName("total_devices")] public int? TotalDevices { get; set; }

    /// <summary>Gets or sets the date and time of the first interval in the response.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("start_at")] public DateTimeOffset? StartAt { get; set; }

    /// <summary>Gets or sets the date and time of the last interval in the response.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("end_at")] public DateTimeOffset? EndAt { get; set; }

    /// <summary>Gets or sets the type of items contained in the intervals list.</summary>
    [JsonPropertyName("items")] public string? Items { get; set; }

    /// <summary>Gets or sets the list of production meter telemetry intervals.</summary>
    [JsonPropertyName("intervals")] public List<TelemetryProductionMeterInterval>? Intervals { get; set; }

    /// <summary>Gets or sets system metadata associated with this response.</summary>
    [JsonPropertyName("meta")] public SystemMeta? Meta { get; set; }
}
