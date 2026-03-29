using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Physical address of a system installation.
/// </summary>
public class SystemAddress
{
    /// <summary>Gets or sets the city of the system installation.</summary>
    [JsonPropertyName("city")] public string? City { get; set; }

    /// <summary>Gets or sets the state or province of the system installation.</summary>
    [JsonPropertyName("state")] public string? State { get; set; }

    /// <summary>Gets or sets the country of the system installation.</summary>
    [JsonPropertyName("country")] public string? Country { get; set; }

    /// <summary>Gets or sets the postal code of the system installation.</summary>
    [JsonPropertyName("postal_code")] public string? PostalCode { get; set; }
}
