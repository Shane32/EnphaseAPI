using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.JsonConverters;

/// <summary>
/// Converts a "true"/"false" string to/from <see cref="Nullable{T}"/> of <see cref="bool"/>.
/// Throws <see cref="JsonException"/> for unrecognized values.
/// </summary>
internal class StringBoolConverter : JsonConverter<bool?>
{
    /// <inheritdoc/>
    public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;
        var str = reader.GetString();
        return str switch {
            "true" => true,
            "false" => false,
            _ => throw new JsonException($"Unrecognized value '{str}' for boolean field. Expected 'true' or 'false'.")
        };
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options)
    {
        if (value == null)
            writer.WriteNullValue();
        else
            writer.WriteStringValue(value.Value ? "true" : "false");
    }
}
