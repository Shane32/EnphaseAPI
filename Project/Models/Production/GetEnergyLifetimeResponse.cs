using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Daily lifetime energy production data for a system.
/// </summary>
public class GetEnergyLifetimeResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the first date in the production series.</summary>
    [JsonConverter(typeof(NullableDateTimeOffsetDateConverter))]
    [JsonPropertyName("start_date")] public DateTimeOffset? StartDate { get; set; }

    /// <summary>Gets or sets the date from which meter-based production data begins.</summary>
    [JsonConverter(typeof(NullableDateTimeOffsetDateConverter))]
    [JsonPropertyName("meter_start_date")] public DateTimeOffset? MeterStartDate { get; set; }

    /// <summary>Gets or sets the list of daily production values in Wh, one entry per day starting from <see cref="StartDate"/>.</summary>
    [JsonPropertyName("production")] public List<long>? Production { get; set; }

    /// <summary>Gets or sets the list of daily micro-inverter production values in Wh.</summary>
    [JsonPropertyName("micro_production")] public List<long>? MicroProduction { get; set; }

    /// <summary>Gets or sets the list of daily meter-based production values in Wh.</summary>
    [JsonPropertyName("meter_production")] public List<long>? MeterProduction { get; set; }

    /// <summary>Gets or sets system metadata associated with this response.</summary>
    [JsonPropertyName("meta")] public SystemMeta? Meta { get; set; }
}
