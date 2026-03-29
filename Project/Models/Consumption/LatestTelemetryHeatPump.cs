using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class LatestTelemetryHeatPump
{
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("last_report_at")] public long? LastReportAt { get; set; }
    [JsonPropertyName("operational_mode")] public string? OperationalMode { get; set; }
}
