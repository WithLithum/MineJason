namespace MineJason.Serialization.TextJson;
using System.Text.Json;
using System.Text.Json.Nodes;
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
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                return ReadArray(ref reader);
            }

            throw new JsonException("not a chat component");
        }

        var dom = JsonDocument.ParseValue(ref reader);

        if (dom.RootElement.TryGetProperty("type", out var value)
            && value.ValueKind == JsonValueKind.String)
        {
            switch (value.GetString()!)
            {
                case "text":
                    return DeserializeComponent<TextChatComponent>(dom);

                case "translatable":
                    return DeserializeComponent<TranslatableChatComponent>(dom);

                case "selector":
                    return DeserializeComponent<EntityChatComponent>(dom);

                case "score":
                    return DeserializeComponent<ScoreboardChatComponent>(dom);
                
                case "keybind":
                    return DeserializeComponent<KeybindChatComponent>(dom);

                case "nbt":
                    return DeserializeComponent<NbtChatComponent>(dom);

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
            return DeserializeComponent<TranslatableChatComponent>(dom);
        }

        if (dom.RootElement.TryGetProperty("score", out _))
        {
            return DeserializeComponent<ScoreboardChatComponent>(dom);
        }

        if (dom.RootElement.TryGetProperty("selector", out _))
        {
            return DeserializeComponent<EntityChatComponent>(dom);
        }

        if (dom.RootElement.TryGetProperty("keybind", out _))
        {
            return DeserializeComponent<KeybindChatComponent>(dom);
        }

        if (dom.RootElement.TryGetProperty("path", out _))
        {
            return DeserializeComponent<NbtChatComponent>(dom);
        }

        throw new NotSupportedException();
    }

    private static ChatComponent ReadArray(ref Utf8JsonReader reader)
    {
        var array = JsonNode.Parse(ref reader)!.AsArray();

        if (array.Count == 0)
        {
            return ChatComponent.CreateText(string.Empty);
        }

        var first = array[0]!.AsObject();
        var component = first.Deserialize<ChatComponent>(ChatComponent.SerializerOptions)
                        ?? ChatComponent.CreateText(string.Empty);

        if (array.Count < 2)
        {
            return component;
        }

        for (var i = 1; i < array.Count; i++)
        {
            var extraComp = array[i].Deserialize<ChatComponent>();

            if (extraComp != null)
            {
                component.Append(extraComp);
            }
        }

        return component;
    }

    private static T? DeserializeComponent<T>(JsonDocument document)
    {
        return document.RootElement.Deserialize<T>(ChatComponent.SerializerOptions);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, ChatComponent value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, ChatComponent.SerializerOptions);
    }
}