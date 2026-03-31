using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Base device information shared across all Enphase device types.
/// </summary>
public class DeviceInfo
{
    /// <summary>Gets or sets the device identifier.</summary>
    [JsonPropertyName("id")] public long Id { get; set; }

    /// <summary>Gets or sets the date and time of the most recent data report from this device.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("last_report_at")] public DateTimeOffset? LastReportAt { get; set; }

    /// <summary>Gets or sets the human-readable name of the device.</summary>
    [JsonPropertyName("name")] public string? Name { get; set; }

    /// <summary>Gets or sets the serial number of the device.</summary>
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }

    /// <summary>Gets or sets the manufacturer part number.</summary>
    [JsonPropertyName("part_number")] public string? PartNumber { get; set; }

    /// <summary>Gets or sets the SKU (stock-keeping unit) of the device.</summary>
    [JsonPropertyName("sku")] public string? Sku { get; set; }

    /// <summary>Gets or sets the device model name.</summary>
    [JsonPropertyName("model")] public string? Model { get; set; }

    /// <summary>Gets or sets the operational status of the device.</summary>
    [JsonPropertyName("status")] public string? Status { get; set; }

    /// <summary>Gets or sets the raw JSON value indicating whether the device is active.</summary>
    [JsonPropertyName("active")] public JsonElement? Active { get; set; }

    /// <summary>Gets or sets the product name of the device.</summary>
    [JsonPropertyName("product_name")] public string? ProductName { get; set; }
}
