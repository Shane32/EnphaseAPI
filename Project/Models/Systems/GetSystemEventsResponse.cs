using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetSystemEventsResponse
{
    [JsonPropertyName("events")] public List<SystemEvent>? Events { get; set; }
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
}
