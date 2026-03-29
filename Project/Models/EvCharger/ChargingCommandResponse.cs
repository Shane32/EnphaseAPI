using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class ChargingCommandResponse
{
    [JsonPropertyName("message")] public string? Message { get; set; }
}
