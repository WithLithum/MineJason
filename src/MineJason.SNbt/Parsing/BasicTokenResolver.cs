// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Parsing;

/// <summary>
/// Determines which "basic" type a token is.
/// </summary>
public static class BasicTokenResolver
{
    /// <summary>
    /// Determines which basic type that a token belongs judging on the first character of that
    /// token.
    /// </summary>
    /// <param name="first">The first character.</param>
    /// <returns>The token.</returns>
    public static BasicTokenType Resolve(char first)
    {
        return first switch
        {
            '"' => BasicTokenType.DoubleQuotedString,
            '\'' => BasicTokenType.SingleQuotedString,
            '{' => BasicTokenType.Compound,
            '[' => BasicTokenType.List,
            _ => BasicTokenType.UnquotedWord
        };
    }
}