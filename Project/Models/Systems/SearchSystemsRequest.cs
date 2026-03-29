using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Request body for the search systems endpoint.
/// </summary>
public class SearchSystemsRequest
{
    /// <summary>Gets or sets the field to sort results by.</summary>
    [JsonPropertyName("sort_by")] public string? SortBy { get; set; }

    /// <summary>Gets or sets the filter criteria for the search.</summary>
    [JsonPropertyName("system")] public SearchSystemsFilter? System { get; set; }
}
