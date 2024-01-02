namespace MineJason.Serialization.TextJson;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Provides JSON conversion for <see cref="ChatComponent"/>.
/// </summary>
public class ChatComponentConverter : JsonConverter<ChatComponent>
{
    /// <inheritdoc />
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
                    return dom.RootElement.Deserialize<TextChatComponent>(ChatComponent.SerializerOptions);

                case "translatable":
                    return dom.RootElement.Deserialize<TranslatableChatComponent>(ChatComponent
                        .SerializerOptions);

                case "selector":
                    return dom.RootElement.Deserialize<EntityChatComponent>(ChatComponent
                        .SerializerOptions);

                case "score":
                    return dom.RootElement.Deserialize<ScoreboardChatComponent>(ChatComponent
                        .SerializerOptions);

                // TODO add keybind and nbt
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
            return dom.RootElement.Deserialize<TextChatComponent>(ChatComponent.SerializerOptions);
        }

        if (dom.RootElement.TryGetProperty("translate", out _))
        {
            return dom.RootElement.Deserialize<TranslatableChatComponent>(ChatComponent.SerializerOptions);
        }

        if (dom.RootElement.TryGetProperty("score", out _))
        {
            return dom.RootElement.Deserialize<ScoreboardChatComponent>(ChatComponent.SerializerOptions);
        }

        if (dom.RootElement.TryGetProperty("selector", out _))
        {
            return dom.RootElement.Deserialize<EntityChatComponent>(ChatComponent.SerializerOptions);
        }

        // TODO add keybind and nbt.
        if (dom.RootElement.TryGetProperty("keybind", out _))
        {
            throw new NotImplementedException();
        }

        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, ChatComponent value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, ChatComponent.SerializerOptions);
    }
}