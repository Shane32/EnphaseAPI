using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Daily lifetime battery charge and discharge data for a system.
/// </summary>
public class GetBatteryLifetimeResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the first date (YYYY-MM-DD) in the series.</summary>
    [JsonPropertyName("start_date")] public string? StartDate { get; set; }

    /// <summary>Gets or sets the list of daily battery charge values in Wh, one entry per day starting from <see cref="StartDate"/>.</summary>
    [JsonPropertyName("charge")] public List<long>? Charge { get; set; }

    /// <summary>Gets or sets the list of daily battery discharge values in Wh, one entry per day starting from <see cref="StartDate"/>.</summary>
    [JsonPropertyName("discharge")] public List<long>? Discharge { get; set; }

    /// <summary>Gets or sets system metadata associated with this response.</summary>
    [JsonPropertyName("meta")] public SystemMeta? Meta { get; set; }
}
