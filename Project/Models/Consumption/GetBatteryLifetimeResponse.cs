using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetBatteryLifetimeResponse
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("start_date")] public string? StartDate { get; set; }
    [JsonPropertyName("charge")] public List<long>? Charge { get; set; }
    [JsonPropertyName("discharge")] public List<long>? Discharge { get; set; }
    [JsonPropertyName("meta")] public SystemMeta? Meta { get; set; }
}
