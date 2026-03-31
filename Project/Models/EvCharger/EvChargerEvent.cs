using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// An event reported by an EV charger device.
/// </summary>
public class EvChargerEvent
{
    /// <summary>Gets or sets the event status (e.g. "active", "cleared").</summary>
    [JsonPropertyName("status")] public string? Status { get; set; }

    /// <summary>Gets or sets the date and time when the event was triggered.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("triggered_date")] public DateTimeOffset? TriggeredDate { get; set; }

    /// <summary>Gets or sets the date and time when the event was cleared.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("cleared_date")] public DateTimeOffset? ClearedDate { get; set; }

    /// <summary>Gets or sets additional detail about the event.</summary>
    [JsonPropertyName("details")] public string? Details { get; set; }
}
