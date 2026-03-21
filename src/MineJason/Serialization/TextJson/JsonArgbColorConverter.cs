// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.TextJson;

using System;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Converts a color value from and to ARGB format. This converter supports reading both array and
/// integer format, but will only write integer format.
/// </summary>
public class JsonArgbColorConverter : JsonConverter<Color>
{
    /// <inheritdoc />
    public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            return ReadNormalized(ref reader);
        }
        else
        {
            return ReadArgb(ref reader);
        }
    }

    private static Color ReadArgb(ref Utf8JsonReader reader)
    {
        var argb = reader.GetInt32();
        return Color.FromArgb(argb);
    }

    private static Color ReadNormalized(ref Utf8JsonReader reader)
    {
        var numRead = 0;
        var argb = new float[4];

        while (true)
        {
            if (numRead > 4)
            {
                throw new JsonException("Expected ARGB array but found array size bigger than 4");
            }

            reader.Read();
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                break;
            }

            argb[numRead] = reader.GetInt32();
            numRead++;
        }

        if (numRead != 4)
        {
            throw new JsonException("Expected ARGB array but found array size lesser than 4");
        }

        return Color.FromArgb((int)argb[0],
            (int)argb[1],
            (int)argb[2],
            (int)argb[3]);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.ToArgb());
    }
}
