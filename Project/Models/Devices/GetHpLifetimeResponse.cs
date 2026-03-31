using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Daily lifetime energy consumption for heat pump devices on a system.
/// </summary>
public class GetHpLifetimeResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the first date in the consumption series.</summary>
    [JsonConverter(typeof(NullableDateTimeOffsetDateConverter))]
    [JsonPropertyName("start_date")] public DateTimeOffset? StartDate { get; set; }

    /// <summary>Gets or sets the last date in the consumption series.</summary>
    [JsonConverter(typeof(NullableDateTimeOffsetDateConverter))]
    [JsonPropertyName("end_date")] public DateTimeOffset? EndDate { get; set; }

    /// <summary>Gets or sets the list of daily energy consumption values in Wh, one entry per day starting from <see cref="StartDate"/>.</summary>
    [JsonPropertyName("consumption")] public List<long>? Consumption { get; set; }
}
