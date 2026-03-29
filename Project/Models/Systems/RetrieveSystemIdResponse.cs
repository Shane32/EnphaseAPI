using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.Models;

public class RetrieveSystemIdResponse
{
    [JsonPropertyName("system_id")] public int SystemId { get; set; }
}
