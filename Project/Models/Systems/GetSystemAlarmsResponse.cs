using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Alarms for a system within a requested time range.
/// </summary>
public class GetSystemAlarmsResponse
{
    /// <summary>Gets or sets the list of system alarms.</summary>
    [JsonPropertyName("alarms")] public List<SystemAlarm>? Alarms { get; set; }

    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
}
