using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class ConsumptionMeterInterval
{
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }
    [JsonPropertyName("devices_reporting")] public int? DevicesReporting { get; set; }
    [JsonPropertyName("enwh")] public long? Enwh { get; set; }
}
