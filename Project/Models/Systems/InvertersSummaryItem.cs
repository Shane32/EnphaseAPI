using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class InvertersSummaryItem
{
    [JsonPropertyName("signal_strength")] public int? SignalStrength { get; set; }
    [JsonPropertyName("micro_inverters")] public List<MicroInverterInfo>? MicroInverters { get; set; }
}
