// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.SNbt.Values;

/// <inheritdoc />
public class StringValueConverter : JsonConverter<SNbtStringValue>
{
    /// <inheritdoc />
    public override SNbtStringValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() ??
               throw new JsonException("TAG_String value cannot be null.");
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, SNbtStringValue value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}