using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Container holding the list of EV charger devices.
/// </summary>
public class EvChargerDevicesContainer
{
    /// <summary>Gets or sets the list of EV charger device information.</summary>
    [JsonPropertyName("ev_chargers")] public List<EvChargerDeviceInfo>? EvChargers { get; set; }
}
