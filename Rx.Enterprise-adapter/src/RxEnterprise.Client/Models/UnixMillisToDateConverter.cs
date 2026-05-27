using System.Text.Json;
using System.Text.Json.Serialization;

namespace RxEnterprise.Client;

internal sealed class UnixMillisToDateConverter : JsonConverter<string?>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null) return null;
        if (reader.TokenType == JsonTokenType.String) return reader.GetString();
        if (reader.TokenType == JsonTokenType.Number)
            return DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64()).ToString("yyyy-MM-dd");
        throw new JsonException($"Unexpected token type {reader.TokenType} for date field");
    }

    public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
        => writer.WriteStringValue(value);
}
