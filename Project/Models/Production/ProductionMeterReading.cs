using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single production meter reading from a specific meter device.
/// </summary>
public class ProductionMeterReading
{
    /// <summary>Gets or sets the serial number of the meter device.</summary>
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }

    /// <summary>Gets or sets the cumulative energy reading in Wh.</summary>
    [JsonPropertyName("value")] public long? Value { get; set; }

    /// <summary>Gets or sets the date and time when this reading was taken.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("read_at")] public DateTimeOffset? ReadAt { get; set; }
}
