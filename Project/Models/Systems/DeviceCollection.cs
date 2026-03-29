using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class DeviceCollection
{
    [JsonPropertyName("micros")] public List<DeviceInfo>? Micros { get; set; }
    [JsonPropertyName("meters")] public List<MeterDevice>? Meters { get; set; }
    [JsonPropertyName("gateways")] public List<GatewayDevice>? Gateways { get; set; }
    [JsonPropertyName("q_relays")] public List<DeviceInfo>? QRelays { get; set; }
    [JsonPropertyName("acbs")] public List<DeviceInfo>? Acbs { get; set; }
    [JsonPropertyName("encharges")] public List<DeviceInfo>? Encharges { get; set; }
    [JsonPropertyName("enpowers")] public List<DeviceInfo>? Enpowers { get; set; }
    [JsonPropertyName("ev_chargers")] public List<EvChargerDevice>? EvChargers { get; set; }
    [JsonPropertyName("iq_collars")] public List<DeviceInfo>? IqCollars { get; set; }
}
