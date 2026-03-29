using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class EvChargerEvent
{
    [JsonPropertyName("status")] public string? Status { get; set; }
    [JsonPropertyName("triggered_date")] public long? TriggeredDate { get; set; }
    [JsonPropertyName("cleared_date")] public long? ClearedDate { get; set; }
    [JsonPropertyName("details")] public string? Details { get; set; }
}
