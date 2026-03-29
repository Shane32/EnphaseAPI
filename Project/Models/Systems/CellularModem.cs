using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Information about a cellular modem attached to a gateway device.
/// </summary>
public class CellularModem
{
    /// <summary>Gets or sets the International Mobile Equipment Identity (IMEI) of the modem.</summary>
    [JsonPropertyName("imei")] public string? Imei { get; set; }

    /// <summary>Gets or sets the manufacturer part number of the modem.</summary>
    [JsonPropertyName("part_num")] public string? PartNum { get; set; }

    /// <summary>Gets or sets the SKU of the modem.</summary>
    [JsonPropertyName("sku")] public string? Sku { get; set; }

    /// <summary>Gets or sets the Unix timestamp when the cellular data plan started.</summary>
    [JsonPropertyName("plan_start_date")] public long? PlanStartDate { get; set; }

    /// <summary>Gets or sets the Unix timestamp when the cellular data plan expires.</summary>
    [JsonPropertyName("plan_end_date")] public long? PlanEndDate { get; set; }
}
