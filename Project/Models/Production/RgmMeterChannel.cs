using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single RGM channel reading from a meter device.
/// </summary>
public class RgmMeterChannel
{
    /// <summary>Gets or sets the channel number on the meter.</summary>
    [JsonPropertyName("channel")] public int? Channel { get; set; }

    /// <summary>Gets or sets the date and time at which the interval ends.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("end_at")] public DateTimeOffset? EndAt { get; set; }

    /// <summary>Gets or sets the energy delivered (Wh) during the interval.</summary>
    [JsonPropertyName("wh_del")] public long? WhDel { get; set; }

    /// <summary>Gets or sets the instantaneous power reading in watts at the end of the interval.</summary>
    [JsonPropertyName("curr_w")] public long? CurrW { get; set; }
}
