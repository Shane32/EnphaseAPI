using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class LatestTelemetryDevices
{
    [JsonPropertyName("meters")] public List<LatestTelemetryMeter>? Meters { get; set; }
    [JsonPropertyName("encharges")] public List<LatestTelemetryEncharge>? Encharges { get; set; }
    [JsonPropertyName("heat-pump")] public List<LatestTelemetryHeatPump>? HeatPump { get; set; }
    [JsonPropertyName("evse")] public List<LatestTelemetryEvse>? Evse { get; set; }
}
