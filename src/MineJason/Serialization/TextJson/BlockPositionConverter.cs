// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;

using MineJason.Data.Coordinates;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Provides JSON conversion for <see cref="BlockPosition"/>.
/// </summary>
public sealed class BlockPositionConverter : JsonConverter<BlockPosition>
{
    /// <inheritdoc/>
    public override BlockPosition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString() ?? throw new JsonException();

        return BlockPosition.Parse(value);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, BlockPosition value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
