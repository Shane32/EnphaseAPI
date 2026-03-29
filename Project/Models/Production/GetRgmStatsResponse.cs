using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Revenue-grade meter (RGM) statistics for a system.
/// </summary>
public class GetRgmStatsResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the total number of revenue-grade meter devices.</summary>
    [JsonPropertyName("total_devices")] public int? TotalDevices { get; set; }

    /// <summary>Gets or sets system metadata associated with this response.</summary>
    [JsonPropertyName("meta")] public SystemMeta? Meta { get; set; }

    /// <summary>Gets or sets the list of aggregated RGM intervals.</summary>
    [JsonPropertyName("intervals")] public List<RgmInterval>? Intervals { get; set; }

    /// <summary>Gets or sets the per-meter RGM interval data.</summary>
    [JsonPropertyName("meter_intervals")] public List<RgmMeterInterval>? MeterIntervals { get; set; }
}
