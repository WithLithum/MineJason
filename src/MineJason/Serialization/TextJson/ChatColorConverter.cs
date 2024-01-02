namespace MineJason.Serialization.TextJson;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ChatColorConverter : JsonConverter<IChatColor>
{
    public override IChatColor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected string");
        }

        var str = reader.GetString();

        if (string.IsNullOrWhiteSpace(str))
        {
            throw new JsonException("Expected chat colour");
        }

        if (str.StartsWith('#'))
        {
            throw new NotImplementedException();
        }
        else
        {
            try
            {
                return new KnownColor(str);
            }
            catch (ArgumentException ex)
            {
                throw new JsonException($"Invalid color name '{str}'.", ex);
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, IChatColor value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GenerateColorText());
    }
}