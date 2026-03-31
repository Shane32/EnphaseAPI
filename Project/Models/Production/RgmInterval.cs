using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// An aggregated RGM production interval for a system.
/// </summary>
public class RgmInterval
{
    /// <summary>Gets or sets the date and time at which the interval ends.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("end_at")] public DateTimeOffset? EndAt { get; set; }

    /// <summary>Gets or sets the energy delivered (Wh) during the interval.</summary>
    [JsonPropertyName("wh_del")] public long? WhDel { get; set; }

    /// <summary>Gets or sets the number of devices reporting during the interval.</summary>
    [JsonPropertyName("devices_reporting")] public int? DevicesReporting { get; set; }
}
