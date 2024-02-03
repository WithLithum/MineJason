// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Serialization.TextJson;

using MineJason.Data.Coordinates;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Provides JSON conversion for <see cref="AnyBlockPosition"/>.
/// </summary>
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
