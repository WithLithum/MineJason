// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Items;

using MineJason.Utilities;
using System;

/// <summary>
/// Provides a base class for item components with values that needs to be converted
/// with an invariant culture.
/// </summary>
/// <typeparam name="T">The type.</typeparam>
public abstract class InvariantItemComponent<T> : TypedItemComponent<T>
    where T : IFormattable
{
    /// <summary>
    /// Initialises a new instance of the <see cref="InvariantItemComponent{T}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    protected InvariantItemComponent(T value) : base(value)
    {
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Value.ToStringNeutral();
    }
}
