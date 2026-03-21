// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Values;

using System.Collections.ObjectModel;
using MineJason.SNbt.Parsing;

/// <summary>
/// Represents <c>TAG_Long_Array</c>.
/// </summary>
public sealed class SNbtLongArray : Collection<long>, ISNbtWritable
{
    /// <inheritdoc/>
    public SNbtTagType Type => SNbtTagType.LongArray;

    /// <inheritdoc/>
    public void WriteTo(SNbtWriter writer)
    {
        SNbtArrayHelper.WriteArray('L', this, writer, 'L');
    }
}