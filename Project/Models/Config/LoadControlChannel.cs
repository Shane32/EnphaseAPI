using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class LoadControlChannel
{
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("load_name")] public string? LoadName { get; set; }
    [JsonPropertyName("owner_can_override")] public bool? OwnerCanOverride { get; set; }
    [JsonPropertyName("mode")] public string? Mode { get; set; }
    [JsonPropertyName("soc_low")] public int? SocLow { get; set; }
    [JsonPropertyName("soc_high")] public int? SocHigh { get; set; }
    [JsonPropertyName("status")] public string? Status { get; set; }
    [JsonPropertyName("essential_start_time")] public int? EssentialStartTime { get; set; }
    [JsonPropertyName("essential_end_time")] public int? EssentialEndTime { get; set; }
}
