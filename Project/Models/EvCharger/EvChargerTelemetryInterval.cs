using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single EV charger telemetry interval.
/// </summary>
public class EvChargerTelemetryInterval
{
    /// <summary>Gets or sets the energy consumed by the EV charger in Wh during the interval.</summary>
    [JsonPropertyName("consumption")] public int? Consumption { get; set; }

    /// <summary>Gets or sets the date and time at which the interval ends.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("end_at")] public DateTimeOffset? EndAt { get; set; }
}
