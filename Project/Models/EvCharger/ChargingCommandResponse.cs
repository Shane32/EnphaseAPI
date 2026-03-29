using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Response returned after issuing a start or stop charging command to an EV charger.
/// </summary>
public class ChargingCommandResponse
{
    /// <summary>Gets or sets the confirmation message returned by the API.</summary>
    [JsonPropertyName("message")] public string? Message { get; set; }
}
