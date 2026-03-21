// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Parsing;

/// <summary>
/// Encapsulates a basic token in string NBT.
/// </summary>
public readonly record struct SNbtBasicToken
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtBasicToken"/> structure.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    public SNbtBasicToken(BasicTokenType type, string value)
    {
        Type = type;
        Value = value;
    }
    
    public BasicTokenType Type { get; }
    public string Value { get; }
}