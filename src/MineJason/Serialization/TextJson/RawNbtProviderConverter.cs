// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Data.Nbt;

/// <inheritdoc />
public class RawNbtProviderConverter : JsonConverter<RawNbtDataProvider>
{
    /// <inheritdoc />
    public override RawNbtDataProvider? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        return str == null
            ? null
            : new RawNbtDataProvider(str);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, RawNbtDataProvider value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Data);
    }
}