using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single EV charger telemetry interval.
/// </summary>
public class EvChargerTelemetryInterval
{
    /// <summary>Gets or sets the energy consumed by the EV charger in Wh during the interval.</summary>
    [JsonPropertyName("consumption")] public int? Consumption { get; set; }

    /// <summary>Gets or sets the Unix timestamp at which the interval ends.</summary>
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }
}
