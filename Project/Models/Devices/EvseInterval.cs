using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single EVSE telemetry interval.
/// </summary>
public class EvseInterval
{
    /// <summary>Gets or sets the Unix timestamp at which the interval ends.</summary>
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }

    /// <summary>Gets or sets the energy consumed by the EV charger in Wh during the interval.</summary>
    [JsonPropertyName("wh_consumed")] public long? WhConsumed { get; set; }
}
