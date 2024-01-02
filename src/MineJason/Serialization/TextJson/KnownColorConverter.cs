namespace MineJason.Serialization.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Provides JSON conversion for <see cref="KnownColor"/>.
/// </summary>
public class KnownColorConverter : JsonConverter<KnownColor>
{
    /// <inheritdoc />
    public override KnownColor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected known color string");
        }

        var str = reader.GetString();

        if (string.IsNullOrWhiteSpace(str))
        {
            throw new JsonException("Expected known colour");
        }

        try
        {
            return new KnownColor(str);
        }
        catch (ArgumentException e)
        {
            throw new JsonException("Invalid known colour name", e);
        }
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, KnownColor value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GenerateColorText());
    }
}