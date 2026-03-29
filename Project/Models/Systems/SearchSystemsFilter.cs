using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Filter criteria for searching systems.
/// </summary>
public class SearchSystemsFilter
{
    /// <summary>Gets or sets a list of system IDs to filter by.</summary>
    [JsonPropertyName("ids")] public List<int>? Ids { get; set; }

    /// <summary>Gets or sets a system name substring to filter by.</summary>
    [JsonPropertyName("name")] public string? Name { get; set; }

    /// <summary>Gets or sets a reference string to filter by.</summary>
    [JsonPropertyName("reference")] public string? Reference { get; set; }

    /// <summary>Gets or sets an other-reference string to filter by.</summary>
    [JsonPropertyName("other_reference")] public string? OtherReference { get; set; }

    /// <summary>Gets or sets a list of system statuses to filter by.</summary>
    [JsonPropertyName("statuses")] public List<string>? Statuses { get; set; }
}
