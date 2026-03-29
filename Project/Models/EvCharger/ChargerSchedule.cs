using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class ChargerSchedule
{
    [JsonPropertyName("schedules")] public List<ChargerScheduleTimeBlock>? Schedules { get; set; }
    [JsonPropertyName("type")] public string? Type { get; set; }
    [JsonPropertyName("is_active")] public bool? IsActive { get; set; }
    [JsonPropertyName("reminder_flag")] public bool? ReminderFlag { get; set; }
    [JsonPropertyName("reminder_timer")] public int? ReminderTimer { get; set; }
}
