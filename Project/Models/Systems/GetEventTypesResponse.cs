using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetEventTypesResponse
{
    [JsonPropertyName("event_types")] public List<EventType>? EventTypes { get; set; }
}
