using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class MicroInterval
{
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }
    [JsonPropertyName("powr")] public long? Powr { get; set; }
    [JsonPropertyName("enwh")] public long? Enwh { get; set; }
}
