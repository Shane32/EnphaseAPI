using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Device information for an EV charger.
/// </summary>
public class EvChargerDeviceInfo
{
    /// <summary>Gets or sets the device identifier.</summary>
    [JsonPropertyName("id")] public string? Id { get; set; }

    /// <summary>Gets or sets the SKU (stock-keeping unit) of the device.</summary>
    [JsonPropertyName("sku")] public string? Sku { get; set; }

    /// <summary>Gets or sets the current operational status of the device.</summary>
    [JsonPropertyName("status")] public string? Status { get; set; }

    /// <summary>Gets or sets the serial number of the device.</summary>
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }

    /// <summary>Gets or sets the human-readable name of the device.</summary>
    [JsonPropertyName("name")] public string? Name { get; set; }

    /// <summary>Gets or sets the device model name.</summary>
    [JsonPropertyName("model")] public string? Model { get; set; }

    /// <summary>Gets or sets the manufacturer part number.</summary>
    [JsonPropertyName("part_number")] public string? PartNumber { get; set; }

    /// <summary>Gets or sets the Unix timestamp of the last report from the device.</summary>
    [JsonPropertyName("last_report_at")] public long? LastReportAt { get; set; }

    /// <summary>Gets or sets the firmware version installed on the device.</summary>
    [JsonPropertyName("firmware")] public string? Firmware { get; set; }

    /// <summary>Gets or sets whether the device is currently active.</summary>
    [JsonPropertyName("active")] public bool? Active { get; set; }
}
