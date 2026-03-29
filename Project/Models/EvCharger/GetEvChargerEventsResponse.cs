using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Events reported by EV chargers on a system.
/// </summary>
public class GetEvChargerEventsResponse
{
    /// <summary>Gets or sets the total number of events returned.</summary>
    [JsonPropertyName("count")] public int? Count { get; set; }

    /// <summary>Gets or sets the list of EV charger events.</summary>
    [JsonPropertyName("events")] public List<EvChargerEvent>? Events { get; set; }

    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int? SystemId { get; set; }
}
