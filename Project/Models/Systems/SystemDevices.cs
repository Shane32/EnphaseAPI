using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class SystemDevices
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("total_devices")] public int? TotalDevices { get; set; }
    [JsonPropertyName("items")] public string? Items { get; set; }
    [JsonPropertyName("devices")] public DeviceGroup? Devices { get; set; }
}
