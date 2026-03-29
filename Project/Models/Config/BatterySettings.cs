using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class BatterySettings
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("battery_mode")] public string? BatteryMode { get; set; }
    [JsonPropertyName("reserve_soc")] public int? ReserveSoc { get; set; }
    [JsonPropertyName("energy_independence")] public string? EnergyIndependence { get; set; }
    [JsonPropertyName("charge_from_grid")] public string? ChargeFromGrid { get; set; }
    [JsonPropertyName("battery_shutdown_level")] public int? BatteryShutdownLevel { get; set; }
}
