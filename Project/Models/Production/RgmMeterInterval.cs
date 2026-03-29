using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// RGM interval data grouped by individual meter device.
/// </summary>
public class RgmMeterInterval
{
    /// <summary>Gets or sets the serial number of the revenue-grade meter.</summary>
    [JsonPropertyName("meter_serial_number")] public string? MeterSerialNumber { get; set; }

    /// <summary>Gets or sets the serial number of the Envoy gateway associated with the meter.</summary>
    [JsonPropertyName("envoy_serial_number")] public string? EnvoySerialNumber { get; set; }

    /// <summary>Gets or sets the list of per-channel RGM readings for this meter.</summary>
    [JsonPropertyName("intervals")] public List<RgmMeterChannel>? Intervals { get; set; }
}
