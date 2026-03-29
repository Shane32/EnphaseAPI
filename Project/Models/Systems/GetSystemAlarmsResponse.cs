using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetSystemAlarmsResponse
{
    [JsonPropertyName("alarms")] public List<SystemAlarm>? Alarms { get; set; }
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
}
