// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Values;

using JetBrains.Annotations;
using MineJason.SNbt.Parsing;
using System.Collections.ObjectModel;

/// <summary>
/// Represents a <c>TAG_List</c>.
/// </summary>
/// <typeparam name="T">The string NBT value type.</typeparam>
[PublicAPI]
public sealed class SNbtListValue<T> : Collection<T>, ISNbtWritable, ISNbtList
    where T : ISNbtWritable
{

    /// <inheritdoc/>
    public SNbtTagType Type => SNbtTagType.List;

    /// <inheritdoc/>
    void ISNbtList.Add(ISNbtWritable value)
    {
        if (!DoesAccept(value))
        {
            throw new ArgumentException("The specified value is not of an acceptable type.",
                nameof(value));
        }

        var item = (T)value;
        Add(item);
    }

    /// <inheritdoc/>
    public bool DoesAccept(ISNbtWritable value)
    {
        return typeof(T).IsAssignableFrom(value.GetType());
    }


    /// <inheritdoc/>
    public void WriteTo(SNbtWriter writer)
    {
        writer.WriteBeginList();
        
        foreach (var value in this)
        {
            writer.WriteComma();
            
            if (value is ISNbtWritable writable)
            {
                writer.WriteValue(writable);
                continue;
            }
            
            writer.WriteValue(value);
        }
        
        writer.WriteEndList();
    }
}