using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class EvChargerDevices
{
    [JsonPropertyName("items")] public string? Items { get; set; }
    [JsonPropertyName("total_devices")] public int? TotalDevices { get; set; }
    [JsonPropertyName("system_id")] public int? SystemId { get; set; }
    [JsonPropertyName("devices")] public EvChargerDevicesContainer? Devices { get; set; }
}
