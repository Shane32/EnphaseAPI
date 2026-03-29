using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class EvChargerTelemetryInterval
{
    [JsonPropertyName("consumption")] public long? Consumption { get; set; }
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }
}
