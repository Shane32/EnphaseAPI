using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetEnergyImportLifetimeResponse
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("start_date")] public string? StartDate { get; set; }
    [JsonPropertyName("import")] public List<long>? Import { get; set; }
    [JsonPropertyName("meta")] public SystemMeta? Meta { get; set; }
}
