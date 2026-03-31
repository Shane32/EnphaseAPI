using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Current grid connection status for a system.
/// </summary>
public class GridStatus
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the current grid connection state (e.g. "grid-tied", "islanded").</summary>
    [JsonPropertyName("grid_state")] public string? GridState { get; set; }

    /// <summary>Gets or sets the date and time of the most recent grid status report.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("last_report_date")] public DateTimeOffset? LastReportDate { get; set; }
}
