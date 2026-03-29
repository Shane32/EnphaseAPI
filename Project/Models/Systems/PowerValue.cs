using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A numeric value with associated unit and precision, used for energy or power readings.
/// </summary>
public class PowerValue
{
    /// <summary>Gets or sets the numeric value of the measurement.</summary>
    [JsonPropertyName("value")] public double? Value { get; set; }

    /// <summary>Gets or sets the unit of the measurement (e.g. "Wh", "W").</summary>
    [JsonPropertyName("units")] public string? Units { get; set; }

    /// <summary>Gets or sets the number of decimal places to display for this value.</summary>
    [JsonPropertyName("precision")] public int? Precision { get; set; }
}
