namespace MineJason.Serialization.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Colors;

/// <summary>
/// Provides conversion for <see cref="RgbChatColor"/>.
/// </summary>
public class RgbColorConverter : JsonConverter<RgbChatColor>
{
    /// <inheritdoc />
    public override RgbChatColor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();

        #if DEBUG
        Console.WriteLine("RgbColorConverter: received {0}", str);        
        #endif
        
        if (string.IsNullOrWhiteSpace(str))
        {
            throw new JsonException("Expected RGB chat string");
        }

        if (!RgbChatColor.TryParse(str, out var result))
        {
            throw new JsonException("Expected valid RGB color notation");
        }

        return result;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, RgbChatColor value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GenerateColorText());
    }
}