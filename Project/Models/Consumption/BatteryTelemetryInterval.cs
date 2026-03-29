using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class BatteryTelemetryInterval
{
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }
    [JsonPropertyName("charge")] public BatteryEnergyData? Charge { get; set; }
    [JsonPropertyName("discharge")] public BatteryEnergyData? Discharge { get; set; }
    [JsonPropertyName("soc")] public BatterySocData? Soc { get; set; }
}
