using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Response containing the system ID retrieved by Envoy serial number.
/// </summary>
public class RetrieveSystemIdResponse
{
    /// <summary>Gets or sets the system identifier associated with the queried Envoy serial number.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
}
