using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GridStatus
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("grid_state")] public string? GridState { get; set; }
    [JsonPropertyName("last_report_date")] public long? LastReportDate { get; set; }
}
