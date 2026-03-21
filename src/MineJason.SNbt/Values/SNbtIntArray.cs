// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Values;

using System.Collections.ObjectModel;
using MineJason.SNbt.Parsing;

/// <summary>
/// Represents a <c>TAG_Int_Array</c>.
/// </summary>
public class SNbtIntArray : Collection<int>, ISNbtWritable
{
    /// <inheritdoc/>
    public SNbtTagType Type => SNbtTagType.IntArray;

    /// <inheritdoc/>
    public void WriteTo(SNbtWriter writer)
    {
        SNbtArrayHelper.WriteArray('I', this, writer);
    }
}