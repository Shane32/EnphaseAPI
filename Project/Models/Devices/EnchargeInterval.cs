using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single Encharge battery telemetry interval containing charge, discharge, and state-of-charge data.
/// </summary>
public class EnchargeInterval
{
    /// <summary>Gets or sets the date and time at which the interval ends.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("end_at")] public DateTimeOffset? EndAt { get; set; }

    /// <summary>Gets or sets the charge energy data for the interval.</summary>
    [JsonPropertyName("charge")] public EnchargeEnergyData? Charge { get; set; }

    /// <summary>Gets or sets the discharge energy data for the interval.</summary>
    [JsonPropertyName("discharge")] public EnchargeEnergyData? Discharge { get; set; }

    /// <summary>Gets or sets the state-of-charge data for the interval.</summary>
    [JsonPropertyName("soc")] public EnchargeSocData? Soc { get; set; }
}
