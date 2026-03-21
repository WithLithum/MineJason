// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;

using MineJason.Text.Colors;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Converts <see cref="TextColor"/> to or from JSON.
/// </summary>
public class TextColorConverter : JsonConverter<TextColor>
{
    /// <inheritdoc />
    public override TextColor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();

#if DEBUG
        Console.WriteLine("RgbColorConverter: received {0}", str);
#endif

        if (string.IsNullOrWhiteSpace(str))
        {
            throw new JsonException("Expected RGB chat string");
        }

        if (!TextColor.TryParse(str, out var result))
        {
            throw new JsonException("Expected valid RGB color notation");
        }

        return result;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, TextColor value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GenerateColorText());
    }
}
