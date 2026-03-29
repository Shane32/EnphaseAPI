using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Device information for a revenue-grade meter device.
/// </summary>
public class MeterDevice : DeviceInfo
{
    /// <summary>Gets or sets the operational state of the meter.</summary>
    [JsonPropertyName("state")] public string? State { get; set; }

    /// <summary>Gets or sets the configuration type of the meter (e.g. "production", "consumption").</summary>
    [JsonPropertyName("config_type")] public string? ConfigType { get; set; }
}
