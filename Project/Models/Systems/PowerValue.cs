using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class PowerValue
{
    [JsonPropertyName("value")] public double? Value { get; set; }
    [JsonPropertyName("units")] public string? Units { get; set; }
    [JsonPropertyName("precision")] public int? Precision { get; set; }
}
