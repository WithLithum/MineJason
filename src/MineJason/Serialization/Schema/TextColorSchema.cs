// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.Schema;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;
using MineJason.Text.Colors;

/// <summary>
/// Defines the conversion between the instance representation of <see cref="IChatColor"/> and
/// element types. Supports encoding any <see cref="IChatColor"/> implementation.
/// </summary>
public class TextColorSchema : ValueSchema<IChatColor>
{
    /// <summary>
    /// The singleton instance of the schema.
    /// </summary>
    public static readonly TextColorSchema Instance = new();

    private TextColorSchema()
    {
    }

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(IChatColor value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateString(value.GenerateColorText(), elementName);
    }

    /// <inheritdoc />
    public override Result<IChatColor> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var strResult = decoder.GetString(value);
        if (!strResult.IsSuccess(out var color))
        {
            return strResult.AsError();
        }

        if (string.IsNullOrWhiteSpace(color))
        {
            return Errors.Error("Text color value cannot be empty");
        }

        if (!color.StartsWith('#'))
        {
            var known = NamedTextColor.CreateOrDefault(color);
            if (known == null)
            {
                return Errors.Error("Unrecognised known color name");
            }

            return known;
        }

        if (!TextColor.TryParse(color, out var rgbValue))
        {
            return Errors.Error("Malformed RGB color value");
        }

        return rgbValue;
    }
}