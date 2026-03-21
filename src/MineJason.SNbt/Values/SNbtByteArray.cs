// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Values;

using System.Collections.ObjectModel;
using JetBrains.Annotations;
using MineJason.SNbt.Parsing;

/// <summary>
/// Represents a <c>TAG_Byte_Array</c>.
/// </summary>
public class SNbtByteArray : Collection<sbyte>, ISNbtWritable
{
    /// <inheritdoc/>
    public SNbtTagType Type => SNbtTagType.List;

    /// <summary>
    /// Adds the specified value to this instance.
    /// </summary>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public void Add(SNbtByteValue value)
    {
        base.Add(value.Value);
    }

    /// <inheritdoc/>
    public void WriteTo(SNbtWriter writer)
    {
        SNbtArrayHelper.WriteArray('B', this, writer, 'b');
    }
}