using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class DeviceInfo
{
    [JsonPropertyName("id")] public long Id { get; set; }
    [JsonPropertyName("last_report_at")] public long? LastReportAt { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }
    [JsonPropertyName("part_number")] public string? PartNumber { get; set; }
    [JsonPropertyName("sku")] public string? Sku { get; set; }
    [JsonPropertyName("model")] public string? Model { get; set; }
    [JsonPropertyName("status")] public string? Status { get; set; }
    [JsonPropertyName("active")] public JsonElement? Active { get; set; }
    [JsonPropertyName("product_name")] public string? ProductName { get; set; }
}
