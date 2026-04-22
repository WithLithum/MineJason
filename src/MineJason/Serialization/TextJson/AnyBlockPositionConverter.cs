// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.TextJson;

using MineJason.Data.Coordinates;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Provides JSON conversion for <see cref="AnyBlockPosition"/>.
/// </summary>
[Obsolete("Use BlockPositionConverter instead.")]
public class AnyBlockPositionConverter : JsonConverter<AnyBlockPosition>
{
    /// <inheritdoc/>
    public override AnyBlockPosition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString() ?? throw new JsonException();

        if (value.Contains('^'))
        {
            return new(LocalBlockPosition.Parse(value));
        }
        else
        {
            return new(BlockPosition.Parse(value));
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, AnyBlockPosition value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
