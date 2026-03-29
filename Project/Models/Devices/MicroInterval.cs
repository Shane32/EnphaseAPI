using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single micro-inverter telemetry interval.
/// </summary>
public class MicroInterval
{
    /// <summary>Gets or sets the Unix timestamp at which the interval ends.</summary>
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }

    /// <summary>Gets or sets the average power output in watts during the interval.</summary>
    [JsonPropertyName("powr")] public long? Powr { get; set; }

    /// <summary>Gets or sets the energy produced in Wh during the interval.</summary>
    [JsonPropertyName("enwh")] public long? Enwh { get; set; }
}
