// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items.Blocks;

using JetBrains.Annotations;
using MineJason.Data.Nbt;
using MineJason.SNbt;
using MineJason.SNbt.Values;

/// <summary>
/// Represents a component that accepts a list of predicates.
/// </summary>
public abstract class InteractivePredicateItemComponent : IItemComponent
{
    /// <summary>
    /// Gets the list of predicates.
    /// </summary>
    [PublicAPI]
    public virtual SNbtListValue<InteractBlockPredicate> Predicates { get; } = [];

    /// <summary>
    /// Gets or sets a value indicating whether a list of matching blocks is shown in the tooltip.
    /// </summary>
    [PublicAPI]
    public bool ShowInTooltip { get; set; }
    
    /// <inheritdoc />
    public string GetString()
    {
        return new SNbtObjectBuilder()
            .Property("predicates", Predicates)
            .Property("show_in_tooltip", ShowInTooltip)
            .ToSNbtString();
    }

    /// <inheritdoc />
    public bool IsValid()
    {
        return true;
    }
}