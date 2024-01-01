﻿namespace MineJason.Serialization.TextJson;
using System.Text.Json;
using System.Text.Json.Serialization;

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
                    return dom.RootElement.Deserialize<TextChatComponent>(ChatComponent.SerializerOptions);

                case "translatable":
                    return dom.RootElement.Deserialize<TranslatableChatComponent>(ChatComponent
                        .SerializerOptions);

                case "selector":
                case "score":
                    return dom.RootElement.Deserialize<ScoreboardChatComponent>(ChatComponent
                        .SerializerOptions);

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
        JsonSerializer.Serialize(writer, (object)value, ChatComponent.SerializerOptions);
    }
}