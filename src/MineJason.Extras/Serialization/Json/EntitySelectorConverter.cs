// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Extras.Serialization.Json;

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Data.Selectors;
using MineJason.Extras.Selectors;

/// <summary>
/// Provides conversion services to <see cref="EntitySelector"/>.
/// </summary>
public class EntitySelectorConverter : JsonConverter<EntitySelector>
{
    /// <inheritdoc />
    public override EntitySelector? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();

        if (string.IsNullOrWhiteSpace(str))
        {
            throw new JsonException("Expected string");
        }

        return EntitySelectorStringFormatter.ParseSelector(str);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, EntitySelector value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}