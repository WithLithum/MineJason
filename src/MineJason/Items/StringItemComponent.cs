// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;
using MineJason.SNbt.Values;

/// <summary>
/// Represents an item component where its value is a string.
/// </summary>
[PublicAPI]
public abstract class StringItemComponent : IItemComponent
{
    /// <summary>
    /// Initialises a new instance of the <see cref="StringItemComponent"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    protected StringItemComponent(string value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets or sets the value of this instance.
    /// </summary>
    public string Value { get; set; }

    /// <inheritdoc />
    public string GetString()
    {
        return SNbtStringValue.EscapeString(Value, false);
    }

    /// <inheritdoc />
    public bool IsValid()
    {
        return true;
    }
}