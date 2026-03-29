using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class EvChargerDeviceInfo
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("sku")] public string? Sku { get; set; }
    [JsonPropertyName("status")] public string? Status { get; set; }
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("model")] public string? Model { get; set; }
    [JsonPropertyName("part_number")] public string? PartNumber { get; set; }
    [JsonPropertyName("firmware")] public string? Firmware { get; set; }
    [JsonPropertyName("active")] public bool? Active { get; set; }
}
