// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Parsing;

using MineJason.SNbt.Values;

/// <summary>
/// Parse a string NBT tag.
/// </summary>
public static class SNbtTagParser
{
    /// <summary>
    /// Parses the specified basic token with the specified type.
    /// </summary>
    /// <param name="type">The type to parse with.</param>
    /// <param name="token">The token to parse.</param>
    /// <returns>The parse result.</returns>
    /// <exception cref="FormatException">The token is in an invalid format.</exception>
    /// <exception cref="ArgumentException">The type is not primitive or parseable.</exception>
    public static ISNbtWritable Parse(SNbtTagType type, SNbtBasicToken token)
    {
        return type switch
        {
            SNbtTagType.Byte => SNbtToken.ParseByteTag(token),
            SNbtTagType.Int => SNbtToken.ParseIntTag(token),
            SNbtTagType.Double => SNbtToken.ParseDoubleTag(token),
            SNbtTagType.Float => SNbtToken.ParseFloatTag(token),
            SNbtTagType.Long => SNbtToken.ParseLongTag(token),
            SNbtTagType.String => new SNbtStringValue(token.Value),
            SNbtTagType.IntArray or SNbtTagType.LongArray or SNbtTagType.ByteArray => SNbtToken.ParseArray(token, type),
            SNbtTagType.List => new SNbtListParser(new StringReader(token.Value)).ReadPrimitive()
                ?? throw new FormatException("Invalid list tag type"),
            SNbtTagType.Compound => ReadCompoundTagInternal(token),
            _ => throw new ArgumentException("Not a terminally parse-able type", nameof(type)),
        };
    }

    private static ISNbtWritable ReadCompoundTagInternal(SNbtBasicToken token)
    {
        var tuple = new SNbtCompoundParser(new SNbtCompoundReader(new StringReader(token.Value)))
                .ReadCompoundOrHListValue();

        return tuple.Item1 == null ? tuple.Item2 : tuple.Item1;
    }
}