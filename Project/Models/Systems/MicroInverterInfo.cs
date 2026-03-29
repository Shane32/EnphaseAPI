using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Detailed information about a micro-inverter returned by the inverters summary endpoint.
/// </summary>
public class MicroInverterInfo
{
    /// <summary>Gets or sets the micro-inverter device identifier.</summary>
    [JsonPropertyName("id")] public long? Id { get; set; }

    /// <summary>Gets or sets the serial number of the micro-inverter.</summary>
    [JsonPropertyName("serial_number")] public string? SerialNumber { get; set; }

    /// <summary>Gets or sets the device model name.</summary>
    [JsonPropertyName("model")] public string? Model { get; set; }

    /// <summary>Gets or sets the manufacturer part number.</summary>
    [JsonPropertyName("part_number")] public string? PartNumber { get; set; }

    /// <summary>Gets or sets the SKU of the micro-inverter.</summary>
    [JsonPropertyName("sku")] public string? Sku { get; set; }

    /// <summary>Gets or sets the operational status of the micro-inverter.</summary>
    [JsonPropertyName("status")] public string? Status { get; set; }

    /// <summary>Gets or sets the raw JSON value representing the power produced by the micro-inverter.</summary>
    [JsonPropertyName("power_produced")] public JsonElement? PowerProduced { get; set; }

    /// <summary>Gets or sets the processor load of the micro-inverter.</summary>
    [JsonPropertyName("proc_load")] public string? ProcLoad { get; set; }

    /// <summary>Gets or sets the parameter table identifier for the micro-inverter.</summary>
    [JsonPropertyName("param_table")] public string? ParamTable { get; set; }

    /// <summary>Gets or sets the serial number of the Envoy gateway this micro-inverter reports to.</summary>
    [JsonPropertyName("envoy_serial_number")] public string? EnvoySerialNumber { get; set; }

    /// <summary>Gets or sets the lifetime energy value for the micro-inverter.</summary>
    [JsonPropertyName("energy")] public PowerValue? Energy { get; set; }

    /// <summary>Gets or sets the grid profile applied to the micro-inverter.</summary>
    [JsonPropertyName("grid_profile")] public string? GridProfile { get; set; }

    /// <summary>Gets or sets the Unix timestamp of the most recent report from this micro-inverter.</summary>
    [JsonPropertyName("last_report_date")] public long? LastReportDate { get; set; }
}
