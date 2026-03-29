using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Summary of EV charger devices associated with a system.
/// </summary>
public class EvChargerDevices
{
    /// <summary>Gets or sets the type of items contained in the devices collection.</summary>
    [JsonPropertyName("items")] public string? Items { get; set; }

    /// <summary>Gets or sets the total number of EV charger devices.</summary>
    [JsonPropertyName("total_devices")] public int? TotalDevices { get; set; }

    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int? SystemId { get; set; }

    /// <summary>Gets or sets the container holding the list of EV charger devices.</summary>
    [JsonPropertyName("devices")] public EvChargerDevicesContainer? Devices { get; set; }
}
