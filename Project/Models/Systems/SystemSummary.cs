using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class SystemSummary
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
    [JsonPropertyName("current_power")] public long? CurrentPower { get; set; }
    [JsonPropertyName("energy_lifetime")] public long? EnergyLifetime { get; set; }
    [JsonPropertyName("energy_today")] public long? EnergyToday { get; set; }
    [JsonPropertyName("last_interval_end_at")] public long? LastIntervalEndAt { get; set; }
    [JsonPropertyName("last_report_at")] public long? LastReportAt { get; set; }
    [JsonPropertyName("modules")] public int? Modules { get; set; }
    [JsonPropertyName("operational_at")] public long? OperationalAt { get; set; }
    [JsonPropertyName("size_w")] public int? SizeW { get; set; }
    [JsonPropertyName("nmi")] public string? Nmi { get; set; }
    [JsonPropertyName("source")] public string? Source { get; set; }
    [JsonPropertyName("status")] public string? Status { get; set; }
    [JsonPropertyName("summary_date")] public string? SummaryDate { get; set; }
    [JsonPropertyName("battery_charge_w")] public long? BatteryChargeW { get; set; }
    [JsonPropertyName("battery_discharge_w")] public long? BatteryDischargeW { get; set; }
    [JsonPropertyName("battery_capacity_wh")] public long? BatteryCapacityWh { get; set; }
    [JsonPropertyName("evse_power")] public JsonElement? EvsePower { get; set; }
}
