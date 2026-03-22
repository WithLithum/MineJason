// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;

namespace MineJason.SNbt.Codecs.Primitives;

/// <inheritdoc />
[Obsolete("Use Serialization instead.")]
public class DoubleCodec : ISNbtCodec<double>
{
    /// <inheritdoc />
    public double Read(SNbtBasicTokenReader reader)
    {
        return SNbtToken.ParseDoubleTag(reader.ReadUnquoteWord()).Value;
    }

    /// <inheritdoc />
    public void Write(double value, SNbtWriter writer)
    {
        writer.WriteValue(value);
    }
}