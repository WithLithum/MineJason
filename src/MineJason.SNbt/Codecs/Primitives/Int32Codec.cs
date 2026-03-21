// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;

namespace MineJason.SNbt.Codecs.Primitives;

/// <inheritdoc />
public class Int32Codec : ISNbtCodec<int>
{
    /// <inheritdoc />
    public int Read(SNbtBasicTokenReader reader)
    {
        var word = reader.ReadUnquoteWord();
        return int.Parse(word.Value);
    }

    /// <inheritdoc />
    public void Write(int value, SNbtWriter writer)
    {
        writer.WriteValue(value);
    }
}