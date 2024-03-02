// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Items;

using MineJason.Utilities;

/// <summary>
/// Provides a base class for boolean item components.
/// </summary>
public abstract class BooleanItemComponent : TypedItemComponent<bool>
{
    /// <summary>
    /// Initialises a new instance of the <see cref="BooleanItemComponent"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    protected BooleanItemComponent(bool value) : base(value)
    {
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return SpecificValueUtil.ToLowerBooleanString(Value);
    }

    /// <inheritdoc/>
    public override bool IsValid()
    {
        return true;
    }
}
