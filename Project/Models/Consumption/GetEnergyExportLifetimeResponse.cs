using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetEnergyExportLifetimeResponse
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("start_date")] public string? StartDate { get; set; }
    [JsonPropertyName("export")] public List<long>? Export { get; set; }
    [JsonPropertyName("meta")] public SystemMeta? Meta { get; set; }
}
