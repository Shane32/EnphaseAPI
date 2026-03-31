using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single battery telemetry interval containing charge, discharge, and state-of-charge data.
/// </summary>
public class BatteryTelemetryInterval
{
    /// <summary>Gets or sets the date and time at which the interval ends.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("end_at")] public DateTimeOffset? EndAt { get; set; }

    /// <summary>Gets or sets the battery charge energy data for the interval.</summary>
    [JsonPropertyName("charge")] public BatteryEnergyData? Charge { get; set; }

    /// <summary>Gets or sets the battery discharge energy data for the interval.</summary>
    [JsonPropertyName("discharge")] public BatteryEnergyData? Discharge { get; set; }

    /// <summary>Gets or sets the battery state-of-charge data for the interval.</summary>
    [JsonPropertyName("soc")] public BatterySocData? Soc { get; set; }
}
