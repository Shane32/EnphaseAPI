using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetLoadControlResponse
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("load_control_data")] public List<LoadControlChannel>? LoadControlData { get; set; }
}
