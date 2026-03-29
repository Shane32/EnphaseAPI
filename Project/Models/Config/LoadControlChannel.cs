using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Configuration and status for a single load control channel.
/// </summary>
public class LoadControlChannel
{
    /// <summary>Gets or sets the channel identifier name.</summary>
    [JsonPropertyName("name")] public string? Name { get; set; }

    /// <summary>Gets or sets the human-readable name of the controlled load.</summary>
    [JsonPropertyName("load_name")] public string? LoadName { get; set; }

    /// <summary>Gets or sets whether the system owner is permitted to override this channel's schedule.</summary>
    [JsonPropertyName("owner_can_override")] public bool? OwnerCanOverride { get; set; }

    /// <summary>Gets or sets the operating mode of the channel (e.g. "schedule", "soc").</summary>
    [JsonPropertyName("mode")] public string? Mode { get; set; }

    /// <summary>Gets or sets the lower state-of-charge threshold (%) at which the load is shed.</summary>
    [JsonPropertyName("soc_low")] public int? SocLow { get; set; }

    /// <summary>Gets or sets the upper state-of-charge threshold (%) at which the load is restored.</summary>
    [JsonPropertyName("soc_high")] public int? SocHigh { get; set; }

    /// <summary>Gets or sets the current status of the channel (e.g. "on", "off").</summary>
    [JsonPropertyName("status")] public string? Status { get; set; }

    /// <summary>Gets or sets the time-of-day (in minutes from midnight) when essential mode starts.</summary>
    [JsonPropertyName("essential_start_time")] public int? EssentialStartTime { get; set; }

    /// <summary>Gets or sets the time-of-day (in minutes from midnight) when essential mode ends.</summary>
    [JsonPropertyName("essential_end_time")] public int? EssentialEndTime { get; set; }
}
