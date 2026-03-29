using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetConsumptionLifetimeResponse
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("start_date")] public string? StartDate { get; set; }
    [JsonPropertyName("consumption")] public List<long>? Consumption { get; set; }
    [JsonPropertyName("meta")] public SystemMeta? Meta { get; set; }
}
