using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class ProductionMeterReading
{
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }
    [JsonPropertyName("value")] public long? Value { get; set; }
    [JsonPropertyName("read_at")] public long? ReadAt { get; set; }
}
