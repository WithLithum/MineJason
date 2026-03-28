// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;
using System.Text.Json;
using System.Text.Json.Serialization;
using MineJason.Text.Colors;

/// <summary>
/// Provides JSON conversion for <see cref="ITextColor"/>.
/// </summary>
public class ChatColorConverter : JsonConverter<ITextColor>
{
    /// <inheritdoc />
    public override ITextColor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected string");
        }

        var str = reader.GetString();

        if (string.IsNullOrWhiteSpace(str))
        {
            throw new JsonException("Expected chat colour");
        }

        if (str.StartsWith('#'))
        {
            // If it is RGB chat color then we are parsing it here.
            if (!RgbTextColor.TryParse(str, out var rgbColor))
            {
                throw new JsonException("Expected RGB color notation");
            }

            return rgbColor;
        }
        else
        {
            try
            {
                return new NamedTextColor(str);
            }
            catch (ArgumentException ex)
            {
                throw new JsonException($"Invalid color name '{str}'.", ex);
            }
        }
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, ITextColor value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GenerateColorText());
    }
}