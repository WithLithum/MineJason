// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Data.Selectors;

/// <summary>
/// Converts <see cref="IEntitySelector"/> between its instance and JSON representation.
/// </summary>
public class AnySelectorConverter : JsonConverter<IEntitySelector>
{
    /// <inheritdoc />
    public override IEntitySelector? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected entity selector");
        }
        
        var str = reader.GetString();

        if (string.IsNullOrWhiteSpace(str))
        {
            throw new JsonException("Expected entity selector, but is empty string");
        }

        if (EntityGuidSelector.TryParse(str, out var guid))
        {
            return guid;
        }

        return EntitySelectorStringFormatter.ParseSelector(str);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, IEntitySelector value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}