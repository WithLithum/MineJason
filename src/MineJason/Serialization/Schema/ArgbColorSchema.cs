// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using System.Drawing;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Encodes or decodes <see cref="Color"/> in ARGB format.
/// </summary>
public class ArgbColorSchema : ValueSchema<Color>
{
    /// <summary>
    /// The singleton instance of this class.
    /// </summary>
    public static readonly ArgbColorSchema Instance = new();

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(Color value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateInt32(value.ToArgb(), elementName);
    }

    /// <inheritdoc />
    public override Result<Color> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        // Check if int32 format works.
        var intResult = decoder.GetInt32(value);
        if (intResult.IsSuccess(out var argb))
        {
            return Color.FromArgb(argb);
        }

        // If not, use float list format.
        return DecodeFloatListInternal(value, decoder);
    }

    private static Result<Color> DecodeFloatListInternal<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var listResult = decoder.GetCollection(value);
        if (!listResult.IsSuccess(out var col)) return listResult.AsError();

        Span<int> list = stackalloc int[4];
        var index = 0;
        foreach (var x in col)
        {
            if (index > 3)
            {
                return Errors.Error("List of floats has more than four values.");
            }

            var fltResult = decoder.GetSingle(x);
            if (!fltResult.IsSuccess(out var flt)) return fltResult.AsError();

            list[index++] = (int)MathF.Round(flt * 255);
        }

        return Color.FromArgb(list[3],
            list[0],
            list[1],
            list[2]);
    }
}