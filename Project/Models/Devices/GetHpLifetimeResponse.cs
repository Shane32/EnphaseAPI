using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Daily lifetime energy consumption for heat pump devices on a system.
/// </summary>
public class GetHpLifetimeResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the first date (YYYY-MM-DD) in the consumption series.</summary>
    [JsonPropertyName("start_date")] public string? StartDate { get; set; }

    /// <summary>Gets or sets the last date (YYYY-MM-DD) in the consumption series.</summary>
    [JsonPropertyName("end_date")] public string? EndDate { get; set; }

    /// <summary>Gets or sets the list of daily energy consumption values in Wh, one entry per day starting from <see cref="StartDate"/>.</summary>
    [JsonPropertyName("consumption")] public List<long>? Consumption { get; set; }
}
