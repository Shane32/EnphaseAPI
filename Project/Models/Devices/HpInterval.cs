using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single heat pump telemetry interval.
/// </summary>
public class HpInterval
{
    /// <summary>Gets or sets the date and time at which the interval ends.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("end_at")] public DateTimeOffset? EndAt { get; set; }

    /// <summary>Gets or sets the energy consumed by the heat pump in Wh during the interval.</summary>
    [JsonPropertyName("wh_consumed")] public long? WhConsumed { get; set; }
}
