using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class RgmMeterInterval
{
    [JsonPropertyName("meter_serial_number")] public string? MeterSerialNumber { get; set; }
    [JsonPropertyName("envoy_serial_number")] public string? EnvoySerialNumber { get; set; }
    [JsonPropertyName("intervals")] public List<RgmMeterChannel>? Intervals { get; set; }
}
