using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// The most recent telemetry data for all devices on a system.
/// </summary>
public class GetLatestTelemetryResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the type of items contained in the devices collection.</summary>
    [JsonPropertyName("items")] public string? Items { get; set; }

    /// <summary>Gets or sets the latest telemetry data grouped by device type.</summary>
    [JsonPropertyName("devices")] public LatestTelemetryDevices? Devices { get; set; }
}
