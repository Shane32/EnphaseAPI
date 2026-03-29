using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class AlarmEvent
{
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }
    [JsonPropertyName("start_date")] public long? StartDate { get; set; }
    [JsonPropertyName("end_date")] public long? EndDate { get; set; }
}
