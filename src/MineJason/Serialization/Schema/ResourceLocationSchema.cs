// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.Schema;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Converts <see cref="ResourceLocation"/> to or from the given element type.
/// </summary>
public class ResourceLocationSchema : ValueSchema<ResourceLocation>
{
    /// <summary>
    /// The singleton instance of this schema.
    /// </summary>
    public static readonly ResourceLocationSchema Instance = new();

    private ResourceLocationSchema()
    {
    }

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(ResourceLocation value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateString(value.ToString(), elementName);
    }

    /// <inheritdoc />
    public override Result<ResourceLocation> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var strResult = decoder.GetString(value);
        if (!strResult.IsSuccess(out var str))
        {
            return strResult.AsError();
        }

        if (!ResourceLocation.TryParse(str, out var result))
        {
            return Errors.Error("Malformed resource location");
        }

        return result;
    }
}