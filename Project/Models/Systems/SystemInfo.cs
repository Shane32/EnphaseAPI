using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class SystemInfo
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("public_name")] public string? PublicName { get; set; }
    [JsonPropertyName("timezone")] public string? Timezone { get; set; }
    [JsonPropertyName("address")] public SystemAddress? Address { get; set; }
    [JsonPropertyName("connection_type")] public string? ConnectionType { get; set; }
    [JsonPropertyName("energy_lifetime")] public long? EnergyLifetime { get; set; }
    [JsonPropertyName("energy_today")] public long? EnergyToday { get; set; }
    [JsonPropertyName("system_size")] public long? SystemSize { get; set; }
    [JsonPropertyName("status")] public string? Status { get; set; }
    [JsonPropertyName("last_report_at")] public long? LastReportAt { get; set; }
    [JsonPropertyName("last_energy_at")] public long? LastEnergyAt { get; set; }
    [JsonPropertyName("operational_at")] public long? OperationalAt { get; set; }
    [JsonPropertyName("attachment_type")] public string? AttachmentType { get; set; }
    [JsonPropertyName("interconnect_date")] public string? InterconnectDate { get; set; }
    [JsonPropertyName("reference")] public string? Reference { get; set; }
    [JsonPropertyName("other_references")] public List<string>? OtherReferences { get; set; }
    [JsonPropertyName("live_stream")] public string? LiveStream { get; set; }
}
