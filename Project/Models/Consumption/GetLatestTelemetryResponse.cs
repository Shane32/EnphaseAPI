using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetLatestTelemetryResponse
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("items")] public string? Items { get; set; }
    [JsonPropertyName("devices")] public LatestTelemetryDevices? Devices { get; set; }
}
