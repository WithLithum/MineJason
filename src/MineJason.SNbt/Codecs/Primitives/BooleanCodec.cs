// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;

namespace MineJason.SNbt.Codecs.Primitives;

/// <inheritdoc />
[Obsolete("Use Serialization instead.")]
public class BooleanCodec : ISNbtCodec<bool>
{
    /// <inheritdoc />
    public bool Read(SNbtBasicTokenReader reader)
    {
        var word = reader.ReadUnquoteWord();
        var value = word.Value;

        return value switch
        {
            "true" or "1b" => true,
            "false" or "0b" => false,
            _ => throw new FormatException("Invalid boolean value")
        };
    }

    /// <inheritdoc />
    public void Write(bool value, SNbtWriter writer)
    {
        writer.WriteValue(value ? (sbyte)1 : (sbyte)0);
    }
}