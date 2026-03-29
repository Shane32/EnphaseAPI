using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class EnchargeSocData
{
    [JsonPropertyName("percent")] public int? Percent { get; set; }
}
