using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class RgmMeterChannel
{
    [JsonPropertyName("channel")] public int? Channel { get; set; }
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }
    [JsonPropertyName("wh_del")] public long? WhDel { get; set; }
    [JsonPropertyName("curr_w")] public long? CurrW { get; set; }
}
