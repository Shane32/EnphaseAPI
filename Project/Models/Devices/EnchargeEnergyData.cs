using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class EnchargeEnergyData
{
    [JsonPropertyName("enwh")] public long? Enwh { get; set; }
}
