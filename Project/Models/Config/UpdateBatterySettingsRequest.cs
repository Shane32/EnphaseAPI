using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class UpdateBatterySettingsRequest
{
    [JsonPropertyName("battery_mode")] public string? BatteryMode { get; set; }
    [JsonPropertyName("reserve_soc")] public int? ReserveSoc { get; set; }
    [JsonPropertyName("energy_independence")] public string? EnergyIndependence { get; set; }
}
