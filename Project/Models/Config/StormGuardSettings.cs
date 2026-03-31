using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Storm Guard configuration and alert status for a system.
/// </summary>
public class StormGuardSettings
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets whether the Storm Guard feature is enabled.</summary>
    [JsonConverter(typeof(EnabledDisabledConverter))]
    [JsonPropertyName("storm_guard_status")] public bool? StormGuardStatus { get; set; }

    /// <summary>Gets or sets the current storm alert status indicating whether a storm is forecasted.</summary>
    [JsonConverter(typeof(StringBoolConverter))]
    [JsonPropertyName("storm_alert")] public bool? StormAlert { get; set; }
}
