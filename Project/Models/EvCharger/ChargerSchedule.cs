using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// A charging schedule for an EV charger.
/// </summary>
public class ChargerSchedule
{
    /// <summary>Gets or sets the list of time blocks that make up this schedule.</summary>
    [JsonPropertyName("schedules")] public List<ChargerScheduleTimeBlock>? Schedules { get; set; }

    /// <summary>Gets or sets the schedule type (e.g. "weekly").</summary>
    [JsonPropertyName("type")] public string? Type { get; set; }

    /// <summary>Gets or sets whether this schedule is currently active.</summary>
    [JsonPropertyName("is_active")] public bool? IsActive { get; set; }

    /// <summary>Gets or sets whether the reminder notification flag is enabled.</summary>
    [JsonPropertyName("reminder_flag")] public bool? ReminderFlag { get; set; }

    /// <summary>Gets or sets the reminder notification timer in minutes before the scheduled start.</summary>
    [JsonPropertyName("reminder_timer")] public int? ReminderTimer { get; set; }
}
