using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Energy import telemetry data for a system.
/// </summary>
public class GetEnergyImportTelemetryResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the granularity of the telemetry intervals (e.g. "week", "day", "15mins").</summary>
    [JsonPropertyName("granularity")] public string? Granularity { get; set; }

    /// <summary>Gets or sets the total number of import meter devices.</summary>
    [JsonPropertyName("total_devices")] public int? TotalDevices { get; set; }

    /// <summary>Gets or sets the Unix timestamp of the first interval in the response.</summary>
    [JsonPropertyName("start_at")] public long? StartAt { get; set; }

    /// <summary>Gets or sets the Unix timestamp of the last interval in the response.</summary>
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }

    /// <summary>Gets or sets the type of items contained in the intervals list.</summary>
    [JsonPropertyName("items")] public string? Items { get; set; }

    /// <summary>Gets or sets the list of per-meter import telemetry intervals.</summary>
    [JsonPropertyName("intervals")] public List<List<ImportTelemetryInterval>>? Intervals { get; set; }

    /// <summary>Gets or sets system metadata associated with this response.</summary>
    [JsonPropertyName("meta")] public SystemMeta? Meta { get; set; }
}
