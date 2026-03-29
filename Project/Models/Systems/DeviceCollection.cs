using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A collection of devices on a system grouped by device type.
/// </summary>
public class DeviceGroup
{
    /// <summary>Gets or sets the list of micro-inverter devices.</summary>
    [JsonPropertyName("micros")] public List<DeviceInfo>? Micros { get; set; }

    /// <summary>Gets or sets the list of meter devices.</summary>
    [JsonPropertyName("meters")] public List<MeterDevice>? Meters { get; set; }

    /// <summary>Gets or sets the list of Envoy gateway devices.</summary>
    [JsonPropertyName("gateways")] public List<GatewayDevice>? Gateways { get; set; }

    /// <summary>Gets or sets the list of Q-Relay devices.</summary>
    [JsonPropertyName("q_relays")] public List<DeviceInfo>? QRelays { get; set; }

    /// <summary>Gets or sets the list of AC battery (ACB) devices.</summary>
    [JsonPropertyName("acbs")] public List<DeviceInfo>? Acbs { get; set; }

    /// <summary>Gets or sets the list of Encharge battery devices.</summary>
    [JsonPropertyName("encharges")] public List<DeviceInfo>? Encharges { get; set; }

    /// <summary>Gets or sets the list of Enpower smart switch devices.</summary>
    [JsonPropertyName("enpowers")] public List<DeviceInfo>? Enpowers { get; set; }

    /// <summary>Gets or sets the list of EV charger devices.</summary>
    [JsonPropertyName("ev_chargers")] public List<EvChargerDevice>? EvChargers { get; set; }

    /// <summary>Gets or sets the list of IQ Collar devices.</summary>
    [JsonPropertyName("iq_collars")] public List<DeviceInfo>? IqCollars { get; set; }
}
