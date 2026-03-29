using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single energy export telemetry interval.
/// </summary>
public class ExportTelemetryInterval
{
    /// <summary>Gets or sets the Unix timestamp at which the interval ends.</summary>
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }

    /// <summary>Gets or sets the energy exported to the grid in Wh during the interval.</summary>
    [JsonPropertyName("wh_exported")] public long? WhExported { get; set; }
}
