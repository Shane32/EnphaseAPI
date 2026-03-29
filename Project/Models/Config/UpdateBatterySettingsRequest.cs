using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Request body for updating battery settings on a system.
/// </summary>
public class UpdateBatterySettingsRequest
{
    /// <summary>Gets or sets the desired battery operating mode (e.g. "backup", "self-consumption").</summary>
    [JsonPropertyName("battery_mode")] public string? BatteryMode { get; set; }

    /// <summary>Gets or sets the minimum state-of-charge percentage to reserve for backup power.</summary>
    [JsonPropertyName("reserve_soc")] public int? ReserveSoc { get; set; }

    /// <summary>Gets or sets the energy independence setting.</summary>
    [JsonPropertyName("energy_independence")] public string? EnergyIndependence { get; set; }
}
