using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class SystemMeta
{
    [JsonPropertyName("status")] public string? Status { get; set; }
    [JsonPropertyName("last_report_at")] public long? LastReportAt { get; set; }
    [JsonPropertyName("last_energy_at")] public long? LastEnergyAt { get; set; }
    [JsonPropertyName("operational_at")] public long? OperationalAt { get; set; }
}
