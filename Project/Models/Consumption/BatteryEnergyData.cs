using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class BatteryEnergyData
{
    [JsonPropertyName("enwh")] public long? Enwh { get; set; }
    [JsonPropertyName("devices_reporting")] public int? DevicesReporting { get; set; }
}
