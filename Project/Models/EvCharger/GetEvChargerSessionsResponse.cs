using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetEvChargerSessionsResponse
{
    [JsonPropertyName("count")] public int Count { get; set; }
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("sessions")] public List<ChargingSession> Sessions { get; set; } = new();
}
