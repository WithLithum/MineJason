// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;

namespace MineJason.SNbt.Codecs.Primitives;

/// <inheritdoc />
[Obsolete("Use Serialization instead.")]
public class Int64Codec : ISNbtCodec<long>
{
    /// <inheritdoc />
    public long Read(SNbtBasicTokenReader reader)
    {
        var word = reader.ReadUnquoteWord();
        var suffix = word.Value[^1];
        if (suffix != 'l' && suffix != 'L')
        {
            throw new FormatException("Invalid TAG_Long.");
        }

        return long.Parse(word.Value[..^1]);
    }

    /// <inheritdoc />
    public void Write(long value, SNbtWriter writer)
    {
        writer.WriteValue(value);
    }
}