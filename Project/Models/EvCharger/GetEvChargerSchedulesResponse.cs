using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetEvChargerSchedulesResponse
{
    [JsonPropertyName("system_id")] public int? SystemId { get; set; }
    [JsonPropertyName("charger_schedules")] public List<ChargerSchedule>? ChargerSchedules { get; set; }
}
