using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A single energy import telemetry interval.
/// </summary>
public class ImportTelemetryInterval
{
    /// <summary>Gets or sets the Unix timestamp at which the interval ends.</summary>
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }

    /// <summary>Gets or sets the energy imported from the grid in Wh during the interval.</summary>
    [JsonPropertyName("wh_imported")] public long? WhImported { get; set; }
}
