using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class EvChargerDevice : DeviceInfo
{
    [JsonPropertyName("firmware")] public string? Firmware { get; set; }
}
