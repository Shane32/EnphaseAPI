using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Battery charge or discharge energy data for a telemetry interval.
/// </summary>
public class BatteryEnergyData
{
    /// <summary>Gets or sets the energy in Wh for this interval.</summary>
    [JsonPropertyName("enwh")] public long? Enwh { get; set; }

    /// <summary>Gets or sets the number of battery devices reporting during the interval.</summary>
    [JsonPropertyName("devices_reporting")] public int? DevicesReporting { get; set; }
}
