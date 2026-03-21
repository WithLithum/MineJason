// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.SNbt.Values;

/// <inheritdoc />
public class ByteValueConverter : JsonConverter<SNbtByteValue>
{
    /// <inheritdoc />
    public override SNbtByteValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.True => new SNbtByteValue(1),
            JsonTokenType.False => new SNbtByteValue(0),
            _ => new SNbtByteValue(reader.GetSByte())
        };
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, SNbtByteValue value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Value);
    }
}