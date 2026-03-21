// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.Schema;

using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Defines a schema that converts a <see cref="Guid"/> from or to its string representation,
/// represented by the specified element type.
/// </summary>
public class StringGuidSchema : ValueSchema<Guid>
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static readonly StringGuidSchema Instance = new();

    private StringGuidSchema()
    {
    }

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(Guid value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return encoder.CreateString(value.ToString("D"), elementName);
    }

    /// <inheritdoc />
    public override Result<Guid> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var strResult = decoder.GetString(value);
        if (strResult.Error != null)
        {
            return strResult.AsError();
        }

        if (!Guid.TryParseExact(strResult.Value!, "D", out var guid))
        {
            return Errors.Error("Malformed UUID");
        }

        return guid;
    }
}