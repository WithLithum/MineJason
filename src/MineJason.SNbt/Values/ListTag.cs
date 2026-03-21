// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.NBT.Values;

using JetBrains.Annotations;
using MineJason.SNbt;
using MineJason.SNbt.Parsing;
using MineJason.SNbt.Values;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// Represents a homogeneous list of tags.
/// </summary>
[PublicAPI]
public sealed class ListTag : Collection<ISNbtWritable>, ISNbtWritable, ISNbtList
{
    /// <summary>
    /// Initializes a new instance of <see cref="ListTag"/> with no values contained within and
    /// with the items constrained to the specified type.
    /// </summary>
    public ListTag(SNbtTagType itemType)
    {
        ItemType = itemType;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ListTag"/> as a wrapper for the specified list.
    /// </summary>
    /// <param name="list">The list to wrap.</param>
    /// <param name="itemType">The type of the tag.</param>
    /// <remarks>
    /// <para>
    /// This constructor does not guarantee that the items contained in the specified list are of
    /// the specified tag type.
    /// </para>
    /// </remarks>
    public ListTag(SNbtTagType itemType, IList<ISNbtWritable> list) : base(list)
    {
        ItemType = itemType;
    }

    /// <summary>
    /// Gets the type of the tag supported by this instance.
    /// </summary>
    public SNbtTagType ItemType { get; }

    /// <inheritdoc />
    public SNbtTagType Type => SNbtTagType.List;

    /// <inheritdoc/>
    void ISNbtList.Add(ISNbtWritable value)
    {
        if (!DoesAccept(value))
        {
            throw new ArgumentException("The specified value is not of an acceptable type.",
                nameof(value));
        }

        Add(value);
    }

    /// <inheritdoc/>
    public bool DoesAccept(ISNbtWritable value)
    {
        return value.Type == ItemType;
    }

    /// <inheritdoc/>
    public void WriteTo(SNbtWriter writer)
    {
        writer.WriteBeginList();
        
        foreach (var value in this)
        {
            writer.WriteComma();
            writer.WriteValue(value);
        }
        
        writer.WriteEndList();
    }
}