using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetEnergyLifetimeResponse
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("start_date")] public string? StartDate { get; set; }
    [JsonPropertyName("meter_start_date")] public string? MeterStartDate { get; set; }
    [JsonPropertyName("production")] public List<long>? Production { get; set; }
    [JsonPropertyName("micro_production")] public List<long>? MicroProduction { get; set; }
    [JsonPropertyName("meter_production")] public List<long>? MeterProduction { get; set; }
    [JsonPropertyName("meta")] public SystemMeta? Meta { get; set; }
}
