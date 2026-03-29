using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Details of a single EV charging session.
/// </summary>
public class ChargingSession
{
    /// <summary>Gets or sets the Unix timestamp when the charging session started.</summary>
    [JsonPropertyName("start_time")] public long StartTime { get; set; }

    /// <summary>Gets or sets the Unix timestamp when the charging session ended.</summary>
    [JsonPropertyName("end_time")] public long EndTime { get; set; }

    /// <summary>Gets or sets the total duration of the session in seconds.</summary>
    [JsonPropertyName("duration")] public long Duration { get; set; }

    /// <summary>Gets or sets the total energy added to the vehicle in kWh during the session.</summary>
    [JsonPropertyName("energy_added")] public double EnergyAdded { get; set; }

    /// <summary>Gets or sets the active charging time in seconds during the session.</summary>
    [JsonPropertyName("charge_time")] public long ChargeTime { get; set; }

    /// <summary>Gets or sets the estimated miles of range added during the session.</summary>
    [JsonPropertyName("miles_added")] public double MilesAdded { get; set; }

    /// <summary>Gets or sets the estimated cost of the charging session in the local currency.</summary>
    [JsonPropertyName("cost")] public double Cost { get; set; }
}
