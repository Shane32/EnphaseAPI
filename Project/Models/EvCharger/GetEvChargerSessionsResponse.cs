using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Charging sessions for a specific EV charger.
/// </summary>
public class GetEvChargerSessionsResponse
{
    /// <summary>Gets or sets the total number of sessions returned.</summary>
    [JsonPropertyName("count")] public int Count { get; set; }

    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the list of charging sessions.</summary>
    [JsonPropertyName("sessions")] public List<ChargingSession> Sessions { get; set; } = new();
}
