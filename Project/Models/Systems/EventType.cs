using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Metadata describing a type of system event.
/// </summary>
public class EventType
{
    /// <summary>Gets or sets the numeric identifier for this event type.</summary>
    [JsonPropertyName("event_type_id")] public int? EventTypeId { get; set; }

    /// <summary>Gets or sets the machine-readable key for this event type.</summary>
    [JsonPropertyName("event_type_key")] public string? EventTypeKey { get; set; }

    /// <summary>Gets or sets whether this event type is stateful (has a distinct start and end).</summary>
    [JsonPropertyName("stateful")] public bool? Stateful { get; set; }

    /// <summary>Gets or sets the human-readable name of the event type.</summary>
    [JsonPropertyName("event_name")] public string? EventName { get; set; }

    /// <summary>Gets or sets a description of what this event type represents.</summary>
    [JsonPropertyName("event_description")] public string? EventDescription { get; set; }

    /// <summary>Gets or sets the recommended action to take when this event occurs.</summary>
    [JsonPropertyName("recommended_action")] public string? RecommendedAction { get; set; }
}
