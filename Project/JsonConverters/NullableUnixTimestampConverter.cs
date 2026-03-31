using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.JsonConverters;

/// <summary>
/// Converts a nullable Unix timestamp (seconds since epoch) to/from <see cref="Nullable{DateTimeOffset}"/>.
/// </summary>
internal class NullableUnixTimestampConverter : JsonConverter<DateTimeOffset?>
{
    /// <inheritdoc/>
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;
        return DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64());
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        if (value == null)
            writer.WriteNullValue();
        else
            writer.WriteNumberValue(value.Value.ToUnixTimeSeconds());
    }
}
