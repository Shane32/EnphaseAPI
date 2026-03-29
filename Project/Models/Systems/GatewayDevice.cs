using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GatewayDevice : DeviceInfo
{
    [JsonPropertyName("emu_sw_version")] public string? EmuSwVersion { get; set; }
    [JsonPropertyName("cellular_modem")] public CellularModem? CellularModem { get; set; }
}
