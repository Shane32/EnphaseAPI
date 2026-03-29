using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Response containing available event types.
/// </summary>
public class GetEventTypesResponse
{
    /// <summary>Gets or sets the list of available event types.</summary>
    [JsonPropertyName("event_types")] public List<EventType>? EventTypes { get; set; }
}
