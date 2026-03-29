using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Summary information for a group of micro-inverters grouped by Envoy.
/// </summary>
public class InvertersSummaryItem
{
    /// <summary>Gets or sets the signal strength of the Envoy communication link.</summary>
    [JsonPropertyName("signal_strength")] public int? SignalStrength { get; set; }

    /// <summary>Gets or sets the list of micro-inverters associated with this Envoy.</summary>
    [JsonPropertyName("micro_inverters")] public List<MicroInverterInfo>? MicroInverters { get; set; }
}
