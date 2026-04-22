// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using MineJason.Data.Profile;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Defines the schema that encodes or decodes a player profile to or from a string represented in
/// the given element type.
/// </summary>
public class StringPlayerProfileSchema : ValueSchema<PlayerProfile>
{
    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(PlayerProfile value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return Errors.Error("Operation is not supported.");
    }

    /// <inheritdoc />
    public override Result<PlayerProfile> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var strResult = decoder.GetString(value);
        if (strResult.Error != null) return strResult.AsError();

        return new PlayerProfile
        {
            Name = strResult.Value!
        };
    }
}