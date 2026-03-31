using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.JsonConverters;

/// <summary>
/// Converts an ISO date string (YYYY-MM-DD) to/from <see cref="DateTimeOffset"/> (midnight UTC).
/// </summary>
internal class DateTimeOffsetDateConverter : JsonConverter<DateTimeOffset>
{
    /// <inheritdoc/>
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString()!;
        return DateTimeOffset.ParseExact(str, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
}
