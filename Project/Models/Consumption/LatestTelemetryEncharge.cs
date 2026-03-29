using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class LatestTelemetryEncharge
{
    [JsonPropertyName("id")] public long? Id { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("channel")] public int? Channel { get; set; }
    [JsonPropertyName("last_report_at")] public long? LastReportAt { get; set; }
    [JsonPropertyName("power")] public long? Power { get; set; }
    [JsonPropertyName("operational_mode")] public string? OperationalMode { get; set; }
}
