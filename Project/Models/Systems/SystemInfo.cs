using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Detailed information about a solar energy system.
/// </summary>
public class SystemInfo
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the name of the system.</summary>
    [JsonPropertyName("name")] public string? Name { get; set; }

    /// <summary>Gets or sets the publicly visible name of the system.</summary>
    [JsonPropertyName("public_name")] public string? PublicName { get; set; }

    /// <summary>Gets or sets the IANA timezone name for the system location.</summary>
    [JsonPropertyName("timezone")] public string? Timezone { get; set; }

    /// <summary>Gets or sets the physical address of the system installation.</summary>
    [JsonPropertyName("address")] public SystemAddress? Address { get; set; }

    /// <summary>Gets or sets the connection type used by the system (e.g. "ethernet", "wifi").</summary>
    [JsonPropertyName("connection_type")] public string? ConnectionType { get; set; }

    /// <summary>Gets or sets the total lifetime energy produced by the system in Wh.</summary>
    [JsonPropertyName("energy_lifetime")] public long? EnergyLifetime { get; set; }

    /// <summary>Gets or sets the energy produced today in Wh.</summary>
    [JsonPropertyName("energy_today")] public long? EnergyToday { get; set; }

    /// <summary>Gets or sets the total installed system size in W.</summary>
    [JsonPropertyName("system_size")] public long? SystemSize { get; set; }

    /// <summary>Gets or sets the operational status of the system.</summary>
    [JsonPropertyName("status")] public string? Status { get; set; }

    /// <summary>Gets or sets the Unix timestamp of the most recent data report.</summary>
    [JsonPropertyName("last_report_at")] public long? LastReportAt { get; set; }

    /// <summary>Gets or sets the Unix timestamp of the most recent energy production measurement.</summary>
    [JsonPropertyName("last_energy_at")] public long? LastEnergyAt { get; set; }

    /// <summary>Gets or sets the Unix timestamp when the system first became operational.</summary>
    [JsonPropertyName("operational_at")] public long? OperationalAt { get; set; }

    /// <summary>Gets or sets the attachment type of the system (e.g. "rooftop", "ground").</summary>
    [JsonPropertyName("attachment_type")] public string? AttachmentType { get; set; }

    /// <summary>Gets or sets the grid interconnect date (YYYY-MM-DD).</summary>
    [JsonPropertyName("interconnect_date")] public string? InterconnectDate { get; set; }

    /// <summary>Gets or sets the primary reference string for the system.</summary>
    [JsonPropertyName("reference")] public string? Reference { get; set; }

    /// <summary>Gets or sets additional reference strings associated with the system.</summary>
    [JsonPropertyName("other_references")] public List<string>? OtherReferences { get; set; }

    /// <summary>Gets or sets the live stream status for the system.</summary>
    [JsonPropertyName("live_stream")] public string? LiveStream { get; set; }
}
