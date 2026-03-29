using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetConsumptionMeterReadingsResponse
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("meter_readings")] public List<ProductionMeterReading>? MeterReadings { get; set; }
    [JsonPropertyName("meta")] public SystemMeta? Meta { get; set; }
}
