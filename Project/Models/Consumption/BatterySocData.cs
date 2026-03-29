using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Battery state-of-charge data for a telemetry interval.
/// </summary>
public class BatterySocData
{
    /// <summary>Gets or sets the battery state-of-charge as a percentage.</summary>
    [JsonPropertyName("percent")] public int? Percent { get; set; }

    /// <summary>Gets or sets the number of battery devices reporting during the interval.</summary>
    [JsonPropertyName("devices_reporting")] public int? DevicesReporting { get; set; }
}
