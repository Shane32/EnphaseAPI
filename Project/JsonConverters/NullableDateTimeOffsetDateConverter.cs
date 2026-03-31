using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.JsonConverters;

/// <summary>
/// Converts a nullable ISO date string (YYYY-MM-DD) to/from <see cref="Nullable{DateTimeOffset}"/> (midnight UTC).
/// </summary>
internal class NullableDateTimeOffsetDateConverter : JsonConverter<DateTimeOffset?>
{
    /// <inheritdoc/>
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;
        var str = reader.GetString()!;
        return DateTimeOffset.ParseExact(str, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        if (value == null)
            writer.WriteNullValue();
        else
            writer.WriteStringValue(value.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
    }
}
