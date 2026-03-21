// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Collections.ObjectModel;
using MineJason.SNbt.Values;

namespace MineJason.SNbt.Collections;

using JetBrains.Annotations;
using MineJason.SNbt.Parsing;

/// <summary>
/// Provides a base for collections of NBT types.
/// </summary>
/// <typeparam name="T">The type.</typeparam>
[PublicAPI]
public abstract class SNbtCollection<T> : Collection<T>, ISNbtWritable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtCollection{T}"/> class.
    /// </summary>
    protected SNbtCollection()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtCollection{T}"/> class as a wrapper
    /// of the specified list.
    /// </summary>
    /// <param name="list">The list to wrap.</param>
    protected SNbtCollection(IList<T> list) : base(list)
    {
    }

    /// <inheritdoc/>
    public SNbtTagType Type => SNbtTagType.List;

    /// <inheritdoc/>
    public void WriteTo(SNbtWriter writer)
    {
        writer.WriteBeginList();
        
        foreach (var value in this)
        {
            writer.WriteComma();
            
            // Behaviour for writable objects.
            if (value is ISNbtWritable writable)
            {
                writer.WriteValue(writable);
                continue;
            }

            // Behaviour for default objects.
            if (value == null)
            {
                throw new InvalidOperationException("Entries cannot be null.");
            }
            
            writer.WriteValue(value);
        }
        
        writer.WriteEndList();
    }
}