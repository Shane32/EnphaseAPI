using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single heat pump telemetry interval.
/// </summary>
public class HpInterval
{
    /// <summary>Gets or sets the Unix timestamp at which the interval ends.</summary>
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }

    /// <summary>Gets or sets the energy consumed by the heat pump in Wh during the interval.</summary>
    [JsonPropertyName("wh_consumed")] public long? WhConsumed { get; set; }
}
