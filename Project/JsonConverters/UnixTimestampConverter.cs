using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shane32.EnphaseAPI.JsonConverters;

/// <summary>
/// Converts a Unix timestamp (seconds since epoch) to/from <see cref="DateTimeOffset"/>.
/// </summary>
internal class UnixTimestampConverter : JsonConverter<DateTimeOffset>
{
    /// <inheritdoc/>
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64());

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        => writer.WriteNumberValue(value.ToUnixTimeSeconds());
}
