using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class EvChargerDevicesContainer
{
    [JsonPropertyName("ev_chargers")] public List<EvChargerDeviceInfo>? EvChargers { get; set; }
}
