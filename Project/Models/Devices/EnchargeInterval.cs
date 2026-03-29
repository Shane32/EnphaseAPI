using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class EnchargeInterval
{
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }
    [JsonPropertyName("charge")] public EnchargeEnergyData? Charge { get; set; }
    [JsonPropertyName("discharge")] public EnchargeEnergyData? Discharge { get; set; }
    [JsonPropertyName("soc")] public EnchargeSocData? Soc { get; set; }
}
