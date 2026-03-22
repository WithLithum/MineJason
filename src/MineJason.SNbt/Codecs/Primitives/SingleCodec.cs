// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;

namespace MineJason.SNbt.Codecs.Primitives;

/// <inheritdoc />
[Obsolete("Use Serialization instead.")]
public class SingleCodec : ISNbtCodec<float>
{
    /// <inheritdoc />
    public float Read(SNbtBasicTokenReader reader)
    {
        return SNbtToken.ParseFloatTag(reader.ReadUnquoteWord()).Value;
    }

    /// <inheritdoc />
    public void Write(float value, SNbtWriter writer)
    {
        writer.WriteValue(value);
    }
}