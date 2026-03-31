using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single energy import telemetry interval.
/// </summary>
public class ImportTelemetryInterval
{
    /// <summary>Gets or sets the date and time at which the interval ends.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("end_at")] public DateTimeOffset? EndAt { get; set; }

    /// <summary>Gets or sets the energy imported from the grid in Wh during the interval.</summary>
    [JsonPropertyName("wh_imported")] public long? WhImported { get; set; }
}
