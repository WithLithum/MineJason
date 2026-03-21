// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Data.Selectors;

/// <summary>
/// Converts <see cref="EntityGuidSelector"/> between its instance and JSON representation.
/// </summary>
public class EntityGuidSelectorConverter : JsonConverter<EntityGuidSelector>
{
    /// <inheritdoc />
    public override EntityGuidSelector? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected GUID value");
        }
        
        var str = reader.GetString();

        if (string.IsNullOrWhiteSpace(str))
        {
            throw new JsonException("Expected GUID value, but is empty string");
        }

        if (!EntityGuidSelector.TryParse(str, out var result))
        {
            throw new JsonException("Invalid GUID value");
        }

        return result;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, EntityGuidSelector value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}