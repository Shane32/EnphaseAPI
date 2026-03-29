using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetHpTelemetryResponse
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("granularity")] public string? Granularity { get; set; }
    [JsonPropertyName("interval_duration")] public string? IntervalDuration { get; set; }
    [JsonPropertyName("start_at")] public long? StartAt { get; set; }
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }
    [JsonPropertyName("items")] public string? Items { get; set; }
    [JsonPropertyName("intervals")] public List<HpInterval>? Intervals { get; set; }
}
