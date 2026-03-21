// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Parsing;

using System.Runtime.CompilerServices;
using MineJason.SNbt.Values;

/// <summary>
/// Provides methods for parsing basic tokens to NBT values.
/// </summary>
public static class SNbtToken
{
    /// <summary>
    /// Converts the specified token to a <see cref="SNbtIntValue"/>.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <returns>The parsed value.</returns>
    /// <exception cref="FormatException">The format of the token is invalid.</exception>
    public static SNbtIntValue ParseIntTag(SNbtBasicToken token)
    {
        VerifyToken(token);

        return new SNbtIntValue(int.Parse(token.Value));
    }

    /// <summary>
    /// Converts the specified token to a <see cref="SNbtDoubleValue"/>.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <returns>The parsed value.</returns>
    /// <exception cref="FormatException">The format of the token is invalid.</exception>
    public static SNbtDoubleValue ParseDoubleTag(SNbtBasicToken token)
    {
        VerifyToken(token);

        // Fixes MNT-15
        // The suffix denotes a double even without being a point
        if (token.Value.EndsWith('d')
            || token.Value.EndsWith('D'))
        {
            return new SNbtDoubleValue(double.Parse(token.Value[..^1]));
        }
        
        if (!token.Value.Contains('.'))
        {
            throw ParseExceptionNotType("TAG_Double");
        }

        return new SNbtDoubleValue(double.Parse(token.Value));
    }

    /// <summary>
    /// Converts the specified token to a <see cref="SNbtFloatValue"/>.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <returns>The parsed value.</returns>
    /// <exception cref="FormatException">The format of the token is invalid.</exception>
    public static SNbtFloatValue ParseFloatTag(SNbtBasicToken token)
    {
        VerifyToken(token);

        var suffix = token.Value[^1];
        if (suffix != 'f' && suffix != 'F')
        {
            throw ParseExceptionNotType("TAG_Float");
        }

        return new SNbtFloatValue(float.Parse(token.Value[..^1]));
    }

    /// <summary>
    /// Converts the specified token to a <see cref="SNbtByteValue"/>.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <returns>The parsed value.</returns>
    /// <exception cref="FormatException">The format of the token is invalid.</exception>
    public static SNbtByteValue ParseByteTag(SNbtBasicToken token)
    {
        VerifyToken(token);

        // Check boolean primitives.
        if (token.Value == "true") return new SNbtByteValue(1);
        if (token.Value == "false") return new SNbtByteValue(0);

        var suffix = token.Value[^1];
        if (suffix != 'b' && suffix != 'B')
        {
            throw ParseExceptionNotType("TAG_Byte");
        }

        return new SNbtByteValue(sbyte.Parse(token.Value[..^1]));
    }

    /// <summary>
    /// Converts the specified token to a <see cref="SNbtLongValue"/>.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <returns>The parsed value.</returns>
    /// <exception cref="FormatException">The format of the token is invalid.</exception>
    public static SNbtLongValue ParseLongTag(SNbtBasicToken token)
    {
        VerifyToken(token);

        var suffix = token.Value[^1];
        if (suffix != 'l' && suffix != 'L')
        {
            throw ParseExceptionNotType("TAG_Long");
        }

        return new SNbtLongValue(long.Parse(token.Value[..^1]));
    }

    /// <summary>
    /// Converts the specified token to an array.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <returns>The parsed value.</returns>
    /// <exception cref="FormatException">The format of the token is invalid.</exception>
    public static ISNbtWritable ParseArray(SNbtBasicToken token, SNbtTagType type)
    {
        VerifyToken(token, BasicTokenType.List);

        using var reader = new StringReader(token.Value);
        var nbtReader = new SNbtTextReader(reader);
        return new SNbtArrayParser(nbtReader, type).ParseArray();
    }

    private static FormatException ParseExceptionNotType(string type)
    {
        return new FormatException($"The specified token is not of {type} type");
    }

    private static void VerifyToken(SNbtBasicToken token, BasicTokenType type = BasicTokenType.UnquotedWord, [CallerArgumentExpression(nameof(token))] string? argName = null)
    {
        if (token.Type != type)
        {
            throw new ArgumentException("The specified token must be a basic token", argName);
        }
    }
}
