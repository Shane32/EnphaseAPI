using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Charging schedules for a specific EV charger.
/// </summary>
public class GetEvChargerSchedulesResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int? SystemId { get; set; }

    /// <summary>Gets or sets the list of charging schedules configured on the EV charger.</summary>
    [JsonPropertyName("charger_schedules")] public List<ChargerSchedule>? ChargerSchedules { get; set; }
}
