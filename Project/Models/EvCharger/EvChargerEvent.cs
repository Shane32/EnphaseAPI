using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// An event reported by an EV charger device.
/// </summary>
public class EvChargerEvent
{
    /// <summary>Gets or sets the event status (e.g. "active", "cleared").</summary>
    [JsonPropertyName("status")] public string? Status { get; set; }

    /// <summary>Gets or sets the Unix timestamp when the event was triggered.</summary>
    [JsonPropertyName("triggered_date")] public long? TriggeredDate { get; set; }

    /// <summary>Gets or sets the Unix timestamp when the event was cleared.</summary>
    [JsonPropertyName("cleared_date")] public long? ClearedDate { get; set; }

    /// <summary>Gets or sets additional detail about the event.</summary>
    [JsonPropertyName("details")] public string? Details { get; set; }
}
