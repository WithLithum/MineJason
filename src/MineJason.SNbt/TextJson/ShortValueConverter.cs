// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.SNbt.Values;

/// <inheritdoc />
public class ShortValueConverter : JsonConverter<SNbtShortValue>
{
    /// <inheritdoc />
    public override SNbtShortValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new SNbtShortValue(reader.GetInt16());
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, SNbtShortValue value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Value);
    }
}