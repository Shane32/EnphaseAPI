using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// An event associated with a system alarm, identifying the affected device and time range.
/// </summary>
public class AlarmEvent
{
    /// <summary>Gets or sets the serial number of the device that triggered the alarm event.</summary>
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }

    /// <summary>Gets or sets the Unix timestamp when the alarm event started.</summary>
    [JsonPropertyName("start_date")] public long? StartDate { get; set; }

    /// <summary>Gets or sets the Unix timestamp when the alarm event ended.</summary>
    [JsonPropertyName("end_date")] public long? EndDate { get; set; }
}
