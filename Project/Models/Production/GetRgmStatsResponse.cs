using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetRgmStatsResponse
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("total_devices")] public int? TotalDevices { get; set; }
    [JsonPropertyName("meta")] public SystemMeta? Meta { get; set; }
    [JsonPropertyName("intervals")] public List<RgmInterval>? Intervals { get; set; }
    [JsonPropertyName("meter_intervals")] public List<RgmMeterInterval>? MeterIntervals { get; set; }
}
