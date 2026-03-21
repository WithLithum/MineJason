// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Values;

namespace MineJason.SNbt.Parsing;

/// <summary>
/// Acquires the type of a tag.
/// </summary>
public static class SNbtTagResolver
{
    private const string SNbtTrue = "true";
    private const string SNbtFalse = "false";
    
    /// <summary>
    /// Determines which type a string NBT tag belongs to. Note that this does not necessarily mean that
    /// the tag will parse successfully later on.
    /// </summary>
    /// <param name="basicToken">The basic token to resolve. Must be an unquoted word, or any other type that can correspond to a tag type.</param>
    /// <returns>The resolved token type.</returns>
    /// <exception cref="ArgumentException">The specified basic token is not an unquoted word, nor a simple type.</exception>
    public static SNbtTagType ResolvePrimitive(SNbtBasicToken basicToken)
    {
        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (basicToken.Type)
        {
            case BasicTokenType.DoubleQuotedString:
            case BasicTokenType.SingleQuotedString:
                return SNbtTagType.String;
            case BasicTokenType.Compound:
                return SNbtTagType.Compound;
            case BasicTokenType.List:
                return ResolveList(basicToken);
        }
        
        if (basicToken.Type != BasicTokenType.UnquotedWord)
        {
            throw new ArgumentException("The specified token must be a unquoted word basic token.",
                nameof(basicToken));
        }

        var value = basicToken.Value;

        // TAG_Byte - boolean
        if (value is SNbtTrue or SNbtFalse) return SNbtTagType.Byte;
        
        // TAG_Byte - value
        if (value.EndsWith("b", StringComparison.OrdinalIgnoreCase))
        {
            return SNbtTagType.Byte;
        }

        // TAG_Float
        if (value.EndsWith("f", StringComparison.OrdinalIgnoreCase))
        {
            return SNbtTagType.Float;
        }
        
        // TAG_Long
        if (value.EndsWith("l", StringComparison.OrdinalIgnoreCase))
        {
            return SNbtTagType.Long;
        }
        
        // TAG_Double
        if (value.Contains('.') &&
            double.TryParse(value, out var doubleValue))
            return SNbtTagType.Double;

        // TAG_Int
        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (int.TryParse(value, out var intValue))
            return SNbtTagType.Int;

        // TAG_String
        return SNbtTagType.String;
    }

    private static SNbtTagType ResolveList(SNbtBasicToken basicToken)
    {
        // If not greater than 3 then it would be "[]" to be valid, which
        // is certainly NOT any array
        //
        // Semicolon is also required for declaring an array
        if (basicToken.Value.Length < 3
            || basicToken.Value[2] != ';')
        {
            return SNbtTagType.List;
        }

        return basicToken.Value[1] switch
        {
            'I' => SNbtTagType.IntArray,
            'L' => SNbtTagType.LongArray,
            'B' => SNbtTagType.ByteArray,
            _ => throw new FormatException($"Unknown array type (identified by '{basicToken.Value[1]}')")
        };
    }

}