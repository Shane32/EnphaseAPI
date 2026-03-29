using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class GetSystemsResponse
{
    [JsonPropertyName("total")] public int Total { get; set; }
    [JsonPropertyName("current_page")] public int CurrentPage { get; set; }
    [JsonPropertyName("size")] public int Size { get; set; }
    [JsonPropertyName("count")] public int Count { get; set; }
    [JsonPropertyName("items")] public string? Items { get; set; }
    [JsonPropertyName("systems")] public List<SystemInfo>? Systems { get; set; }
}
