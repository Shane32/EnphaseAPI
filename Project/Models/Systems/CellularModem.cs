using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class CellularModem
{
    [JsonPropertyName("imei")] public string? Imei { get; set; }
    [JsonPropertyName("part_num")] public string? PartNum { get; set; }
    [JsonPropertyName("sku")] public string? Sku { get; set; }
    [JsonPropertyName("plan_start_date")] public long? PlanStartDate { get; set; }
    [JsonPropertyName("plan_end_date")] public long? PlanEndDate { get; set; }
}
