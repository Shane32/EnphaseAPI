using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Currently open (uncleared) events for a system.
/// </summary>
public class GetOpenEventsResponse
{
    /// <summary>Gets or sets the list of open events.</summary>
    [JsonPropertyName("events")] public List<SystemEvent>? Events { get; set; }

    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
}
