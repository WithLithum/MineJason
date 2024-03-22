// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data;

using System.Collections.ObjectModel;
using MineJason.SNbt;
using MineJason.SNbt.Values;

/// <summary>
/// A collection of <see cref="TypeTagEntry"/>.
/// </summary>
public class TypeTagCollection : Collection<TypeTagEntry>, ISNbtWritable, ISNbtValue
{
    /// <summary>
    /// Converts this instance to its string representation.
    /// </summary>
    /// <returns>The string representation.</returns>
    public void WriteTo(SNbtWriter writer)
    {
        writer.WriteBeginList();

        foreach (var entry in this)
        {
            writer.WriteComma();
            entry.WriteTo(writer);
        }
        
        writer.WriteEndList();
    }

    /// <summary>
    /// Converts this instance to its String NBT representation.
    /// </summary>
    /// <returns>The String NBT representation.</returns>
    public string ToSNbtString()
    {
        var stream = new StringWriter();

        using (var writer = new SNbtWriter(stream))
        {
            WriteTo(writer);
        }

        return stream.ToString();
    }
}