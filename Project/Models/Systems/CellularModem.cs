using System;
using System.Text.Json.Serialization;
using Shane32.EnphaseAPI.JsonConverters;

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

    /// <summary>Gets or sets the date and time when the cellular data plan started.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("plan_start_date")] public DateTimeOffset? PlanStartDate { get; set; }

    /// <summary>Gets or sets the date and time when the cellular data plan expires.</summary>
    [JsonConverter(typeof(NullableUnixTimestampConverter))]
    [JsonPropertyName("plan_end_date")] public DateTimeOffset? PlanEndDate { get; set; }
}
