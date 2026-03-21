// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Text.Colors;

/// <summary>
/// Provides JSON conversion for <see cref="NamedTextColor"/>.
/// </summary>
public class KnownColorConverter : JsonConverter<NamedTextColor>
{
    /// <inheritdoc />
    public override NamedTextColor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected known color string");
        }

        var str = reader.GetString();

        if (string.IsNullOrWhiteSpace(str))
        {
            throw new JsonException("Expected known colour");
        }

        try
        {
            return new NamedTextColor(str);
        }
        catch (ArgumentException e)
        {
            throw new JsonException("Invalid known colour name", e);
        }
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, NamedTextColor value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GenerateColorText());
    }
}