using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Metadata about a system's reporting and operational state.
/// </summary>
public class SystemMeta
{
    /// <summary>Gets or sets the system operational status.</summary>
    [JsonPropertyName("status")] public string? Status { get; set; }

    /// <summary>Gets or sets the Unix timestamp of the most recent data report.</summary>
    [JsonPropertyName("last_report_at")] public long? LastReportAt { get; set; }

    /// <summary>Gets or sets the Unix timestamp of the most recent energy production measurement.</summary>
    [JsonPropertyName("last_energy_at")] public long? LastEnergyAt { get; set; }

    /// <summary>Gets or sets the Unix timestamp when the system first became operational.</summary>
    [JsonPropertyName("operational_at")] public long? OperationalAt { get; set; }
}
