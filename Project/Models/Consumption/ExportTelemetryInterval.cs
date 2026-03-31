using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single energy export telemetry interval.
/// </summary>
public class ExportTelemetryInterval
{
    /// <summary>Gets or sets the date and time at which the interval ends.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("end_at")] public DateTimeOffset? EndAt { get; set; }

    /// <summary>Gets or sets the energy exported to the grid in Wh during the interval.</summary>
    [JsonPropertyName("wh_exported")] public long? WhExported { get; set; }
}
