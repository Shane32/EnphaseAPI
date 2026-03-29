using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Consumption meter readings for a system.
/// </summary>
public class GetConsumptionMeterReadingsResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the list of consumption meter readings.</summary>
    [JsonPropertyName("meter_readings")] public List<ProductionMeterReading>? MeterReadings { get; set; }

    /// <summary>Gets or sets system metadata associated with this response.</summary>
    [JsonPropertyName("meta")] public SystemMeta? Meta { get; set; }
}
