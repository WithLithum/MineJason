// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;

using MineJason.Events.Hover;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Serialization.Schema;

/// <summary>
/// Converts <see cref="HoverEvent"/> and its children from or to JSON.
/// </summary>
public class HoverEventConverter : JsonConverter<HoverEvent>
{
    /// <inheritdoc />
    public override HoverEvent Read(ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var document = JsonDocument.ParseValue(ref reader);

        var decodeResult = HoverEventSchema.Instance.Decode(document.RootElement,
            SchemaCommons.JsonDecoder);

        return decodeResult.Error != null
            ? throw new JsonException($"Decode failed: {decodeResult.Error}")
            : decodeResult.Value!;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer,
        HoverEvent value,
        JsonSerializerOptions options)
    {
        var encodeResult = HoverEventSchema.Instance.Encode(value,
            SchemaCommons.JsonEncoder);

        if (encodeResult.Error != null)
        {
            throw new JsonException($"Encode failed: {encodeResult.Error}");
        }

        encodeResult.Value!.WriteTo(writer, options);
    }

    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableTo(typeof(HoverEvent));
    }
}
