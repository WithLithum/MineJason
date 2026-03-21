// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;

namespace MineJason.SNbt.Codecs.Primitives;

/// <inheritdoc />
public class SByteCodec : ISNbtCodec<sbyte>
{
    /// <inheritdoc />
    public sbyte Read(SNbtBasicTokenReader reader)
    {
        return SNbtToken.ParseByteTag(reader.ReadUnquoteWord()).Value;
    }

    /// <inheritdoc />
    public void Write(sbyte value, SNbtWriter writer)
    {
        writer.WriteValue(value);
    }
}