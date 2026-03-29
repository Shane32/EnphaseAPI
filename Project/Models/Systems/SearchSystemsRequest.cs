using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class SearchSystemsRequest
{
    [JsonPropertyName("sort_by")] public string? SortBy { get; set; }
    [JsonPropertyName("system")] public SearchSystemsFilter? System { get; set; }
}
