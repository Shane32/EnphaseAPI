using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Response containing load control channel data for a system.
/// </summary>
public class GetLoadControlResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the list of load control channels.</summary>
    [JsonPropertyName("load_control_data")] public List<LoadControlChannel>? LoadControlData { get; set; }
}
