// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Components;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Provides a basic concrete type for data component values. 
/// </summary>
/// <typeparam name="TValue">The value to encapsulate.</typeparam>
public readonly record struct DataComponentValue<TValue> : IDataComponentValue
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataComponentValue{TValue}"/> structure.
    /// </summary>
    public DataComponentValue()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="DataComponentValue{TValue}"/> with the specified
    /// value.
    /// </summary>
    /// <param name="value">The value.</param>
    [SetsRequiredMembers]
    public DataComponentValue(TValue value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the value of this instance.
    /// </summary>
    public required TValue Value { get; init; }

    /// <inheritdoc />
    public bool IsRemoval => false;
}