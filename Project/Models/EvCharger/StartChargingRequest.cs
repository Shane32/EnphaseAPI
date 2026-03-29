using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Request body for starting a charging session on an EV charger.
/// </summary>
public class StartChargingRequest
{
    /// <summary>Gets or sets the connector ID to use for the charging session.</summary>
    [JsonPropertyName("connectorId")] public string? ConnectorId { get; set; }

    /// <summary>Gets or sets the desired charging level for the session.</summary>
    [JsonPropertyName("chargingLevel")] public string? ChargingLevel { get; set; }
}
