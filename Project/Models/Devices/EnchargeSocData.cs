using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// State-of-charge data for a single Encharge telemetry interval.
/// </summary>
public class EnchargeSocData
{
    /// <summary>Gets or sets the Encharge battery state-of-charge as a percentage.</summary>
    [JsonPropertyName("percent")] public int? Percent { get; set; }
}
