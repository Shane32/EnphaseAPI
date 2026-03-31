using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Daily lifetime energy consumption data for a system.
/// </summary>
public class GetConsumptionLifetimeResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the first date in the consumption series.</summary>
    [JsonConverter(typeof(NullableDateTimeOffsetDateConverter))]
    [JsonPropertyName("start_date")] public DateTimeOffset? StartDate { get; set; }

    /// <summary>Gets or sets the list of daily energy consumption values in Wh, one entry per day starting from <see cref="StartDate"/>.</summary>
    [JsonPropertyName("consumption")] public List<long>? Consumption { get; set; }

    /// <summary>Gets or sets system metadata associated with this response.</summary>
    [JsonPropertyName("meta")] public SystemMeta? Meta { get; set; }
}
