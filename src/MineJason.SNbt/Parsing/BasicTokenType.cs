// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Parsing;

/// <summary>
/// An enumeration of possible "basic" types of NBT tokens.
/// </summary>
public enum BasicTokenType
{
    /// <summary>
    /// The token type is unknown.
    /// </summary>
    Unknown,
    /// <summary>
    /// A <c>TAG_Compound</c>.
    /// </summary>
    Compound,
    /// <summary>
    /// A double quoted <c>TAG_String</c>.
    /// </summary>
    DoubleQuotedString,
    /// <summary>
    /// A single quoted <c>TAG_String</c>.
    /// </summary>
    SingleQuotedString,
    /// <summary>
    /// An unquoted word.
    /// </summary>
    UnquotedWord,
    /// <summary>
    /// A list or array.
    /// </summary>
    List
}