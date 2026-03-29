using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class MicroInverterInfo
{
    [JsonPropertyName("id")] public long? Id { get; set; }
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }
    [JsonPropertyName("model")] public string? Model { get; set; }
    [JsonPropertyName("part_number")] public string? PartNumber { get; set; }
    [JsonPropertyName("sku")] public string? Sku { get; set; }
    [JsonPropertyName("status")] public string? Status { get; set; }
    [JsonPropertyName("power_produced")] public JsonElement? PowerProduced { get; set; }
    [JsonPropertyName("proc_load")] public string? ProcLoad { get; set; }
    [JsonPropertyName("param_table")] public string? ParamTable { get; set; }
    [JsonPropertyName("envoy_serial_number")] public string? EnvoySerialNumber { get; set; }
    [JsonPropertyName("energy")] public PowerValue? Energy { get; set; }
    [JsonPropertyName("grid_profile")] public string? GridProfile { get; set; }
    [JsonPropertyName("last_report_date")] public long? LastReportDate { get; set; }
}
