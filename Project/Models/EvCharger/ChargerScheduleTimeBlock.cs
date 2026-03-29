using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class ChargerScheduleTimeBlock
{
    [JsonPropertyName("days")] public List<int>? Days { get; set; }
    [JsonPropertyName("start_time")] public string? StartTime { get; set; }
    [JsonPropertyName("end_time")] public string? EndTime { get; set; }
    [JsonPropertyName("charging_level")] public int? ChargingLevel { get; set; }
}
