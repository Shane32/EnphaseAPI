using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// An event associated with a system alarm, identifying the affected device and time range.
/// </summary>
public class AlarmEvent
{
    /// <summary>Gets or sets the serial number of the device that triggered the alarm event.</summary>
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }

    /// <summary>Gets or sets the date and time when the alarm event started.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("start_date")] public DateTimeOffset? StartDate { get; set; }

    /// <summary>Gets or sets the date and time when the alarm event ended.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("end_date")] public DateTimeOffset? EndDate { get; set; }
}
