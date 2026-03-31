using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Telemetry data for a specific EV charger.
/// </summary>
public class GetEvChargerTelemetryResponse
{
    /// <summary>Gets or sets the granularity of the telemetry intervals (e.g. "week", "day", "15mins").</summary>
    [JsonPropertyName("granularity")] public string? Granularity { get; set; }

    /// <summary>Gets or sets the list of EV charger telemetry intervals.</summary>
    [JsonPropertyName("consumption")] public List<EvChargerTelemetryInterval>? Consumption { get; set; }

    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int? SystemId { get; set; }

    /// <summary>Gets or sets the start date of the telemetry window.</summary>
    [JsonConverter(typeof(NullableDateTimeOffsetDateConverter))]
    [JsonPropertyName("start_date")] public DateTimeOffset? StartDate { get; set; }

    /// <summary>Gets or sets the end date of the telemetry window.</summary>
    [JsonConverter(typeof(NullableDateTimeOffsetDateConverter))]
    [JsonPropertyName("end_date")] public DateTimeOffset? EndDate { get; set; }

    /// <summary>Gets or sets the date and time of the first interval in the response.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("start_at")] public DateTimeOffset? StartAt { get; set; }

    /// <summary>Gets or sets the date and time of the last interval in the response.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("end_at")] public DateTimeOffset? EndAt { get; set; }
}
