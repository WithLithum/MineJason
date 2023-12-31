using System.Text.Json;
using System.Text.Json.Serialization;

namespace MineJason.Serialization.TextJson;

public class ChatComponentConverter : JsonConverter<ChatComponent>
{
    public override ChatComponent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("not a chat component");
        }

        var dom = JsonDocument.ParseValue(ref reader);

        if (dom.RootElement.TryGetProperty("type", out var value)
            && value.ValueKind == JsonValueKind.String)
        {
            switch (value.GetString()!)
            {
                case "text":
                    return dom.RootElement.Deserialize<TextChatComponent>(TextJsonComponentHelper.SerializerOptions);

                case "translatable":
                    return dom.RootElement.Deserialize<TranslatableChatComponent>(TextJsonComponentHelper
                        .SerializerOptions);

                case "selector":
                case "score":
                case "keybind":
                case "nbt":
                    throw new NotImplementedException();

                default:
                    throw new NotSupportedException();
            }
        }
        
        // Use old Minecraft behaviour...
        if (dom.RootElement.TryGetProperty("text", out _))
        {
            return dom.RootElement.Deserialize<TextChatComponent>(TextJsonComponentHelper.SerializerOptions);
        }

        if (dom.RootElement.TryGetProperty("translate", out _))
        {
            return dom.RootElement.Deserialize<TranslatableChatComponent>(TextJsonComponentHelper.SerializerOptions);
        }

        if (dom.RootElement.TryGetProperty("score", out _))
        {
            throw new NotImplementedException();
        }

        if (dom.RootElement.TryGetProperty("selector", out _))
        {
            throw new NotImplementedException();
        }

        if (dom.RootElement.TryGetProperty("keybind", out _))
        {
            throw new NotImplementedException();
        }

        throw new NotSupportedException();
    }

    public override void Write(Utf8JsonWriter writer, ChatComponent value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, TextJsonComponentHelper.SerializerOptions);
    }
}