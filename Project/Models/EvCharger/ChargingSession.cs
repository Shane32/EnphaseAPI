using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class ChargingSession
{
    [JsonPropertyName("start_time")] public long? StartTime { get; set; }
    [JsonPropertyName("end_time")] public long? EndTime { get; set; }
    [JsonPropertyName("duration")] public long? Duration { get; set; }
    [JsonPropertyName("energy_added")] public double? EnergyAdded { get; set; }
    [JsonPropertyName("charge_time")] public long? ChargeTime { get; set; }
    [JsonPropertyName("miles_added")] public double? MilesAdded { get; set; }
    [JsonPropertyName("cost")] public double? Cost { get; set; }
}
