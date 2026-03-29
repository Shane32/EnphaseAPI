using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single Encharge battery telemetry interval containing charge, discharge, and state-of-charge data.
/// </summary>
public class EnchargeInterval
{
    /// <summary>Gets or sets the Unix timestamp at which the interval ends.</summary>
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }

    /// <summary>Gets or sets the charge energy data for the interval.</summary>
    [JsonPropertyName("charge")] public EnchargeEnergyData? Charge { get; set; }

    /// <summary>Gets or sets the discharge energy data for the interval.</summary>
    [JsonPropertyName("discharge")] public EnchargeEnergyData? Discharge { get; set; }

    /// <summary>Gets or sets the state-of-charge data for the interval.</summary>
    [JsonPropertyName("soc")] public EnchargeSocData? Soc { get; set; }
}
