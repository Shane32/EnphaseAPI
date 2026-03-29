using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class StartChargingRequest
{
    [JsonPropertyName("connectorId")] public string? ConnectorId { get; set; }
    [JsonPropertyName("chargingLevel")] public string? ChargingLevel { get; set; }
}
