// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using MineJason.Text.Colors;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MineJason.Serialization.TextJson;

/// <summary>
/// Converts <see cref="RgbTextColor"/> to or from JSON.
/// </summary>
public class RgbTextColorConverter : JsonConverter<RgbTextColor>
{
    /// <inheritdoc />
    public override RgbTextColor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();

#if DEBUG
        Console.WriteLine("RgbColorConverter: received {0}", str);
#endif

        if (string.IsNullOrWhiteSpace(str))
        {
            throw new JsonException("Expected RGB chat string");
        }

        if (!RgbTextColor.TryParse(str, out var result))
        {
            throw new JsonException("Expected valid RGB color notation");
        }

        return result;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, RgbTextColor value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GenerateColorText());
    }
}
