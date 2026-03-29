using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class ImportTelemetryInterval
{
    [JsonPropertyName("end_at")] public long? EndAt { get; set; }
    [JsonPropertyName("wh_imported")] public long? WhImported { get; set; }
}
