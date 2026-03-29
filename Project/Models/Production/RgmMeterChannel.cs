using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single RGM channel reading from a meter device.
/// </summary>
public class RgmMeterChannel
{
    /// <summary>Gets or sets the channel number on the meter.</summary>
    [JsonPropertyName("channel")] public int? Channel { get; set; }

    /// <summary>Gets or sets the Unix timestamp at which the interval ends.</summary>
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }

    /// <summary>Gets or sets the energy delivered (Wh) during the interval.</summary>
    [JsonPropertyName("wh_del")] public long? WhDel { get; set; }

    /// <summary>Gets or sets the instantaneous power reading in watts at the end of the interval.</summary>
    [JsonPropertyName("curr_w")] public long? CurrW { get; set; }
}
