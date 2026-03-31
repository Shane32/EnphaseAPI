using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single consumption meter telemetry interval.
/// </summary>
public class ConsumptionMeterInterval
{
    /// <summary>Gets or sets the date and time at which the interval ends.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("end_at")] public DateTimeOffset? EndAt { get; set; }

    /// <summary>Gets or sets the number of consumption meter devices reporting during the interval.</summary>
    [JsonPropertyName("devices_reporting")] public int? DevicesReporting { get; set; }

    /// <summary>Gets or sets the energy consumed in Wh during the interval.</summary>
    [JsonPropertyName("enwh")] public long? Enwh { get; set; }
}
