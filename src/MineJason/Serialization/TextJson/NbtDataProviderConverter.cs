// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Data.Nbt;

/// <inheritdoc />
public class NbtDataProviderConverter : JsonConverter<INbtDataProvider>
{
    /// <inheritdoc />
    public override INbtDataProvider? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("To be converted from JSON, a NBT field must be a string token.");
        }

        var str = reader.GetString();
        if (str == null)
        {
            throw new JsonException("To be converted from JSON, a NBT field must not be null.");
        }

        return new RawNbtDataProvider(str);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, INbtDataProvider value, JsonSerializerOptions options)
    {
        var realType = value.GetType();
        var realTypeInfo = options.GetTypeInfo(realType);
        JsonSerializer.Serialize(writer, value, realTypeInfo);
    }
}