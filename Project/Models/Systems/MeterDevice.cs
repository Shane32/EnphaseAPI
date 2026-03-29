using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class MeterDevice : DeviceInfo
{
    [JsonPropertyName("state")] public string? State { get; set; }
    [JsonPropertyName("config_type")] public string? ConfigType { get; set; }
}
