// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using MineJason.Events;
using MineJason.Serialization.IO.Json;
using MineJason.Serialization.Schema;

/// <summary>
/// Converts <see cref="ClickEvent"/> and its inheritors to or from JSON.
/// </summary>
public class ClickEventConverter : JsonConverter<ClickEvent>
{
    private static readonly JsonElementDecoder Decoder = new();
    private static readonly JsonNodeEncoder Encoder = new();

    /// <inheritdoc />
    public override ClickEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var doc = JsonDocument.ParseValue(ref reader);

        var result = ClickEventSchema.TextInstance.Decode(doc.RootElement, Decoder);
        return result.Error != null
            ? throw new JsonException($"Failed to decode: {result.Error}")
            : result.Value;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, ClickEvent value, JsonSerializerOptions options)
    {
        var result = ClickEventSchema.TextInstance.Encode(value, Encoder);
        if (result.Error != null)
        {
            throw new JsonException($"Failed to encode: {result.Error}");
        }

        result.Value!.WriteTo(writer, options);
    }

    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableTo(typeof(ClickEvent));
    }
}