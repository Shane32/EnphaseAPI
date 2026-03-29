using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single battery telemetry interval containing charge, discharge, and state-of-charge data.
/// </summary>
public class BatteryTelemetryInterval
{
    /// <summary>Gets or sets the Unix timestamp at which the interval ends.</summary>
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }

    /// <summary>Gets or sets the battery charge energy data for the interval.</summary>
    [JsonPropertyName("charge")] public BatteryEnergyData? Charge { get; set; }

    /// <summary>Gets or sets the battery discharge energy data for the interval.</summary>
    [JsonPropertyName("discharge")] public BatteryEnergyData? Discharge { get; set; }

    /// <summary>Gets or sets the battery state-of-charge data for the interval.</summary>
    [JsonPropertyName("soc")] public BatterySocData? Soc { get; set; }
}
