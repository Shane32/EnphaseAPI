using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Paginated list of systems returned by the Enphase API.
/// </summary>
public class GetSystemsResponse
{
    /// <summary>Gets or sets the total number of systems matching the request.</summary>
    [JsonPropertyName("total")] public int Total { get; set; }

    /// <summary>Gets or sets the one-based current page number.</summary>
    [JsonPropertyName("current_page")] public int CurrentPage { get; set; }

    /// <summary>Gets or sets the number of systems per page.</summary>
    [JsonPropertyName("size")] public int Size { get; set; }

    /// <summary>Gets or sets the number of systems returned in this page.</summary>
    [JsonPropertyName("count")] public int Count { get; set; }

    /// <summary>Gets or sets the type of items contained in the systems list.</summary>
    [JsonPropertyName("items")] public string? Items { get; set; }

    /// <summary>Gets or sets the list of systems on this page.</summary>
    [JsonPropertyName("systems")] public List<SystemInfo>? Systems { get; set; }
}
