using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Battery configuration settings for a system.
/// </summary>
public class BatterySettings
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the battery operating mode (e.g. "backup", "self-consumption").</summary>
    [JsonPropertyName("battery_mode")] public string? BatteryMode { get; set; }

    /// <summary>Gets or sets the minimum state-of-charge percentage reserved for backup power.</summary>
    [JsonPropertyName("reserve_soc")] public int? ReserveSoc { get; set; }

    /// <summary>Gets or sets the energy independence setting.</summary>
    [JsonConverter(typeof(EnabledDisabledConverter))]
    [JsonPropertyName("energy_independence")] public bool? EnergyIndependence { get; set; }

    /// <summary>Gets or sets whether the battery is allowed to charge from the grid.</summary>
    [JsonConverter(typeof(EnabledDisabledConverter))]
    [JsonPropertyName("charge_from_grid")] public bool? ChargeFromGrid { get; set; }

    /// <summary>Gets or sets the battery state-of-charge percentage at which the system shuts down.</summary>
    [JsonPropertyName("battery_shutdown_level")] public int? BatteryShutdownLevel { get; set; }
}
