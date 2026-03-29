using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetEvChargerTelemetryResponse
{
    [JsonPropertyName("granularity")] public string? Granularity { get; set; }
    [JsonPropertyName("consumption")] public List<EvChargerTelemetryInterval>? Consumption { get; set; }
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("start_date")] public string? StartDate { get; set; }
    [JsonPropertyName("end_date")] public string? EndDate { get; set; }
    [JsonPropertyName("start_at")] public long? StartAt { get; set; }
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }
}
