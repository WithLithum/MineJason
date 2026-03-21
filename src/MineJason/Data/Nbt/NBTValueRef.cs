// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Nbt;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Encapsulates an NBT value.
/// </summary>
[Obsolete("Use the schema routine instead.")]
public readonly struct NBTValueRef
{
    /// <summary>
    /// Initializes a new instance of <see cref="NBTValueRef"/> with the specified value.
    /// </summary>
    /// <param name="value">The value to encapsulate.</param>
    [SetsRequiredMembers]
    public NBTValueRef(object? value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the encapsulated value.
    /// </summary>
    /// <value>
    /// The encapsulated value. If <see langword="null" />, this instance does not have a value.
    /// </value>
    public required object? Value { get; init; }
}
