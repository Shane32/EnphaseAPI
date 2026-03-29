using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Events for a system within a requested time range.
/// </summary>
public class GetSystemEventsResponse
{
    /// <summary>Gets or sets the list of system events.</summary>
    [JsonPropertyName("events")] public List<SystemEvent>? Events { get; set; }

    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
}
