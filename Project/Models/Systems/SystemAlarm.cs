using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A system alarm grouping one or more related alarm events.
/// </summary>
public class SystemAlarm
{
    /// <summary>Gets or sets the alarm identifier.</summary>
    [JsonPropertyName("id")] public string? Id { get; set; }

    /// <summary>Gets or sets whether the alarm has been cleared.</summary>
    [JsonPropertyName("cleared")] public bool? Cleared { get; set; }

    /// <summary>Gets or sets the severity level of the alarm.</summary>
    [JsonPropertyName("severity")] public int? Severity { get; set; }

    /// <summary>Gets or sets the list of events associated with this alarm.</summary>
    [JsonPropertyName("events")] public List<AlarmEvent>? Events { get; set; }

    /// <summary>Gets or sets the event type identifier for this alarm.</summary>
    [JsonPropertyName("event_type_id")] public int? EventTypeId { get; set; }

    /// <summary>Gets or sets the date and time when the alarm started.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("alarm_start_time")] public DateTimeOffset? AlarmStartTime { get; set; }

    /// <summary>Gets or sets the date and time when the alarm ended.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("alarm_end_time")] public DateTimeOffset? AlarmEndTime { get; set; }
}
