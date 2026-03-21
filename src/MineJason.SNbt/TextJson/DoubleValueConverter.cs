// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.SNbt.Values;

/// <inheritdoc />
public class DoubleValueConverter : JsonConverter<SNbtDoubleValue>
{
    /// <inheritdoc />
    public override SNbtDoubleValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new SNbtDoubleValue(reader.GetDouble());
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, SNbtDoubleValue value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Value);
    }
}