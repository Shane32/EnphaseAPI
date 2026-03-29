using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class SystemEvent
{
    [JsonPropertyName("status")] public string? Status { get; set; }
    [JsonPropertyName("event_type_id")] public int? EventTypeId { get; set; }
    [JsonPropertyName("event_start_time")] public long? EventStartTime { get; set; }
    [JsonPropertyName("event_end_time")] public long? EventEndTime { get; set; }
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }
}
