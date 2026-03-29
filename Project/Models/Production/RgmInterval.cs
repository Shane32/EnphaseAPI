using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// An aggregated RGM production interval for a system.
/// </summary>
public class RgmInterval
{
    /// <summary>Gets or sets the Unix timestamp at which the interval ends.</summary>
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }

    /// <summary>Gets or sets the energy delivered (Wh) during the interval.</summary>
    [JsonPropertyName("wh_del")] public long? WhDel { get; set; }

    /// <summary>Gets or sets the number of devices reporting during the interval.</summary>
    [JsonPropertyName("devices_reporting")] public int? DevicesReporting { get; set; }
}
