using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single production meter telemetry interval.
/// </summary>
public class TelemetryProductionMeterInterval
{
    /// <summary>Gets or sets the date and time at which the interval ends.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("end_at")] public DateTimeOffset? EndAt { get; set; }

    /// <summary>Gets or sets the energy delivered (Wh) during the interval.</summary>
    [JsonPropertyName("wh_del")] public long? WhDel { get; set; }

    /// <summary>Gets or sets the number of devices reporting during the interval.</summary>
    [JsonPropertyName("devices_reporting")] public int? DevicesReporting { get; set; }
}
