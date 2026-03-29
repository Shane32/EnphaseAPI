using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Energy data for a single Encharge telemetry interval.
/// </summary>
public class EnchargeEnergyData
{
    /// <summary>Gets or sets the energy in Wh for this interval.</summary>
    [JsonPropertyName("enwh")] public long? Enwh { get; set; }
}
