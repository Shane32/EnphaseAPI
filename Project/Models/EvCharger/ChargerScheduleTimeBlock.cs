using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A time block within a charger schedule defining days, times, and charging level.
/// </summary>
public class ChargerScheduleTimeBlock
{
    /// <summary>Gets or sets the list of days-of-week (0=Sunday through 6=Saturday) for this block.</summary>
    [JsonPropertyName("days")] public List<int>? Days { get; set; }

    /// <summary>Gets or sets the start time of the charging window (HH:mm format).</summary>
    [JsonPropertyName("start_time")] public string? StartTime { get; set; }

    /// <summary>Gets or sets the end time of the charging window (HH:mm format).</summary>
    [JsonPropertyName("end_time")] public string? EndTime { get; set; }

    /// <summary>Gets or sets the charging level for this time block.</summary>
    [JsonPropertyName("charging_level")] public int? ChargingLevel { get; set; }
}
