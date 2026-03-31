using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Latest telemetry data for an EVSE (EV charger) device.
/// </summary>
public class LatestTelemetryEvse
{
    /// <summary>Gets or sets the serial number of the EVSE device.</summary>
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }

    /// <summary>Gets or sets the human-readable device name.</summary>
    [JsonPropertyName("name")] public string? Name { get; set; }

    /// <summary>Gets or sets the date and time of the most recent report from this device.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("last_report_at")] public DateTimeOffset? LastReportAt { get; set; }

    /// <summary>Gets or sets the current operational mode of the device.</summary>
    [JsonPropertyName("operational_mode")] public string? OperationalMode { get; set; }
}
