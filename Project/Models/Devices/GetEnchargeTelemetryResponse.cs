using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Telemetry data for a specific Encharge battery device.
/// </summary>
public class GetEnchargeTelemetryResponse
{
    /// <summary>Gets or sets the system identifier.</summary>
    [JsonPropertyName("system_id")] public int SystemId { get; set; }

    /// <summary>Gets or sets the serial number of the Encharge device.</summary>
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }

    /// <summary>Gets or sets the granularity of the telemetry intervals (e.g. "week", "day", "15mins").</summary>
    [JsonPropertyName("granularity")] public string? Granularity { get; set; }

    /// <summary>Gets or sets the total number of Encharge devices.</summary>
    [JsonPropertyName("total_devices")] public int? TotalDevices { get; set; }

    /// <summary>Gets or sets the date and time of the first interval in the response.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("start_at")] public DateTimeOffset? StartAt { get; set; }

    /// <summary>Gets or sets the date and time of the last interval in the response.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("end_at")] public DateTimeOffset? EndAt { get; set; }

    /// <summary>Gets or sets the type of items contained in the intervals list.</summary>
    [JsonPropertyName("items")] public string? Items { get; set; }

    /// <summary>Gets or sets the list of Encharge telemetry intervals.</summary>
    [JsonPropertyName("intervals")] public List<EnchargeInterval>? Intervals { get; set; }

    /// <summary>Gets or sets the date and time of the most recent report from this device.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("last_reported_time")] public DateTimeOffset? LastReportedTime { get; set; }

    /// <summary>Gets or sets the most recently reported state-of-charge for this device.</summary>
    [JsonPropertyName("last_reported_soc")] public string? LastReportedSoc { get; set; }
}
