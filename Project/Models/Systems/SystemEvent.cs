using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A system event recording a status change or alarm condition.
/// </summary>
public class SystemEvent
{
    /// <summary>Gets or sets the current status of the event (e.g. "active", "cleared").</summary>
    [JsonPropertyName("status")] public string? Status { get; set; }

    /// <summary>Gets or sets the event type identifier for this event.</summary>
    [JsonPropertyName("event_type_id")] public int? EventTypeId { get; set; }

    /// <summary>Gets or sets the date and time when the event started.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("event_start_time")] public DateTimeOffset? EventStartTime { get; set; }

    /// <summary>Gets or sets the date and time when the event ended.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("event_end_time")] public DateTimeOffset? EventEndTime { get; set; }

    /// <summary>Gets or sets the serial number of the device that generated the event.</summary>
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }
}
