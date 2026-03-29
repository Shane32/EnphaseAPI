using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Latest telemetry data grouped by device type for a system.
/// </summary>
public class LatestTelemetryDevices
{
    /// <summary>Gets or sets the latest telemetry for meter devices.</summary>
    [JsonPropertyName("meters")] public List<LatestTelemetryMeter>? Meters { get; set; }

    /// <summary>Gets or sets the latest telemetry for Encharge battery devices.</summary>
    [JsonPropertyName("encharges")] public List<LatestTelemetryEncharge>? Encharges { get; set; }

    /// <summary>Gets or sets the latest telemetry for heat pump devices.</summary>
    [JsonPropertyName("heat-pump")] public List<LatestTelemetryHeatPump>? HeatPump { get; set; }

    /// <summary>Gets or sets the latest telemetry for EVSE devices.</summary>
    [JsonPropertyName("evse")] public List<LatestTelemetryEvse>? Evse { get; set; }
}
