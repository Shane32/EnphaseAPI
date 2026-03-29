using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class SearchSystemsFilter
{
    [JsonPropertyName("ids")] public List<int>? Ids { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("reference")] public string? Reference { get; set; }
    [JsonPropertyName("other_reference")] public string? OtherReference { get; set; }
    [JsonPropertyName("statuses")] public List<string>? Statuses { get; set; }
}
