using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Telemetry data for a specific EVSE device.
/// </summary>
public class GetEvseTelemetryResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the granularity of the telemetry intervals (e.g. "week", "day", "15mins").</summary>
    [JsonPropertyName("granularity")] public string? Granularity { get; set; }

    /// <summary>Gets or sets the duration of each telemetry interval in minutes.</summary>
    [JsonPropertyName("interval_duration")] public string? IntervalDuration { get; set; }

    /// <summary>Gets or sets the date and time of the first interval in the response.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("start_at")] public DateTimeOffset? StartAt { get; set; }

    /// <summary>Gets or sets the date and time of the last interval in the response.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("end_at")] public DateTimeOffset? EndAt { get; set; }

    /// <summary>Gets or sets the type of items contained in the intervals list.</summary>
    [JsonPropertyName("items")] public string? Items { get; set; }

    /// <summary>Gets or sets the list of EVSE telemetry intervals.</summary>
    [JsonPropertyName("intervals")] public List<EvseInterval>? Intervals { get; set; }
}
