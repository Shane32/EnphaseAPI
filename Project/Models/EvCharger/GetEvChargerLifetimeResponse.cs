using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Daily lifetime energy consumption for a specific EV charger.
/// </summary>
public class GetEvChargerLifetimeResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public long? SystemId { get; set; }

    /// <summary>Gets or sets the first date (YYYY-MM-DD) in the consumption series.</summary>
    [JsonPropertyName("start_date")] public string? StartDate { get; set; }

    /// <summary>Gets or sets the list of daily energy consumption values in Wh, one entry per day starting from <see cref="StartDate"/>.</summary>
    [JsonPropertyName("consumption")] public List<long>? Consumption { get; set; }
}
