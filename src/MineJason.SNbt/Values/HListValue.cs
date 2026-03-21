// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.NBT.Values;

using MineJason.SNbt;
using MineJason.SNbt.Parsing;
using MineJason.SNbt.Values;

/// <summary>
/// Encapsulates a value in a heterogenous list.
/// </summary>
public readonly struct HListValue : ISNbtWritable
{
    /// <summary>
    /// Initializes a new instance of <see cref="HListValue"/> with the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    public HListValue(ISNbtWritable value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the value contained in this instance.
    /// </summary>
    public ISNbtWritable Value { get; }

    /// <inheritdoc/>
    public SNbtTagType Type => SNbtTagType.Compound;

    /// <inheritdoc/>
    public void WriteTo(SNbtWriter writer)
    {
        writer.WriteBeginCompound();
        writer.WriteKeyNameQuoted("");
        Value.WriteTo(writer);
        writer.WriteEndCompound();
    }
}
