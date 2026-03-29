using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Telemetry data for a specific EV charger.
/// </summary>
public class GetEvChargerTelemetryResponse
{
    /// <summary>Gets or sets the granularity of the telemetry intervals (e.g. "week", "day", "15mins").</summary>
    [JsonPropertyName("granularity")] public string? Granularity { get; set; }

    /// <summary>Gets or sets the list of EV charger telemetry intervals.</summary>
    [JsonPropertyName("consumption")] public List<EvChargerTelemetryInterval>? Consumption { get; set; }

    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int? SystemId { get; set; }

    /// <summary>Gets or sets the start date (YYYY-MM-DD) of the telemetry window.</summary>
    [JsonPropertyName("start_date")] public string? StartDate { get; set; }

    /// <summary>Gets or sets the end date (YYYY-MM-DD) of the telemetry window.</summary>
    [JsonPropertyName("end_date")] public string? EndDate { get; set; }

    /// <summary>Gets or sets the Unix timestamp of the first interval in the response.</summary>
    [JsonPropertyName("start_at")] public long? StartAt { get; set; }

    /// <summary>Gets or sets the Unix timestamp of the last interval in the response.</summary>
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }
}
