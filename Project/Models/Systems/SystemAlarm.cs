using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class SystemAlarm
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("cleared")] public bool? Cleared { get; set; }
    [JsonPropertyName("severity")] public int? Severity { get; set; }
    [JsonPropertyName("events")] public List<AlarmEvent>? Events { get; set; }
    [JsonPropertyName("event_type_id")] public int? EventTypeId { get; set; }
    [JsonPropertyName("alarm_start_time")] public long? AlarmStartTime { get; set; }
    [JsonPropertyName("alarm_end_time")] public long? AlarmEndTime { get; set; }
}
