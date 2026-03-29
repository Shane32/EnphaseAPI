using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Device information for an Envoy gateway device.
/// </summary>
public class GatewayDevice : DeviceInfo
{
    /// <summary>Gets or sets the EMU software version installed on the gateway.</summary>
    [JsonPropertyName("emu_sw_version")] public string? EmuSwVersion { get; set; }

    /// <summary>Gets or sets information about the cellular modem attached to the gateway, if any.</summary>
    [JsonPropertyName("cellular_modem")] public CellularModem? CellularModem { get; set; }
}
