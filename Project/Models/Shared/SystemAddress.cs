using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class SystemAddress
{
    [JsonPropertyName("city")] public string? City { get; set; }
    [JsonPropertyName("state")] public string? State { get; set; }
    [JsonPropertyName("country")] public string? Country { get; set; }
    [JsonPropertyName("postal_code")] public string? PostalCode { get; set; }
}
