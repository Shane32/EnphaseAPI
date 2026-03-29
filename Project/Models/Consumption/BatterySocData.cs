using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class BatterySocData
{
    [JsonPropertyName("percent")] public int? Percent { get; set; }
    [JsonPropertyName("devices_reporting")] public int? DevicesReporting { get; set; }
}
