using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Device information for an EV charger device reported within a system's device list.
/// </summary>
public class EvChargerDevice : DeviceInfo
{
    /// <summary>Gets or sets the firmware version installed on the EV charger.</summary>
    [JsonPropertyName("firmware")] public string? Firmware { get; set; }
}
