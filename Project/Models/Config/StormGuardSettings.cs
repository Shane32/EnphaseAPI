using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Storm Guard configuration and alert status for a system.
/// </summary>
public class StormGuardSettings
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the Storm Guard feature status (e.g. "enabled", "disabled").</summary>
    [JsonPropertyName("storm_guard_status")] public string? StormGuardStatus { get; set; }

    /// <summary>Gets or sets the current storm alert status indicating whether a storm is forecasted.</summary>
    [JsonPropertyName("storm_alert")] public string? StormAlert { get; set; }
}
