using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class EventType
{
    [JsonPropertyName("event_type_id")] public int? EventTypeId { get; set; }
    [JsonPropertyName("event_type_key")] public string? EventTypeKey { get; set; }
    [JsonPropertyName("stateful")] public bool? Stateful { get; set; }
    [JsonPropertyName("event_name")] public string? EventName { get; set; }
    [JsonPropertyName("event_description")] public string? EventDescription { get; set; }
    [JsonPropertyName("recommended_action")] public string? RecommendedAction { get; set; }
}
