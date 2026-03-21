// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Serialization.Schema;

/// <summary>
/// Converts text components from and to JSON.
/// </summary>
public class TextComponentConverter : JsonConverter<ChatComponent>
{
    /// <inheritdoc />
    public override ChatComponent? Read(ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var element = JsonElement.ParseValue(ref reader);
        return ReadElement(ref element);
    }

    private static ChatComponent? ReadElement(ref JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.Null)
        {
            return null;
        }

        return ReadOne(ref element);
    }

    private static ChatComponent ReadOne(ref JsonElement element)
    {
        var decodeResult = TextComponentSchema.Instance.Decode(element,
            SchemaCommons.JsonDecoder);
        if (decodeResult.Error != null)
        {
            throw new JsonException($"Decode failed: {decodeResult.Error}");
        }

        return decodeResult.Value!;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, ChatComponent value, JsonSerializerOptions options)
    {
        var nodeResult = TextComponentSchema.Instance.Encode(value,
                SchemaCommons.JsonEncoder);
        if (nodeResult.Error != null)
        {
            throw new JsonException($"Encode failed: {nodeResult.Error}");
        }

        nodeResult.Value!.WriteTo(writer);
    }

    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableTo(typeof(ChatComponent));
    }
}
