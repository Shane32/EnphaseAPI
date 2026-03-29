using System.Text.Json.Serialization;

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

    /// <summary>Gets or sets the Unix timestamp of the most recent grid status report.</summary>
    [JsonPropertyName("last_report_date")] public long? LastReportDate { get; set; }
}
