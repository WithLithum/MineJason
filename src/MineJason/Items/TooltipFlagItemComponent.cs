// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using MineJason.SNbt.Values;

/// <summary>
/// Represents a flag that is used by detecting its presence, along with an optional
/// flag to determine its tooltip behaviour.
/// </summary>
public abstract class TooltipFlagItemComponent : IItemComponent
{
    /// <summary>
    /// Gets or sets a value indicating whether to show the information associated with
    /// this component in the item tooltip.
    /// </summary>
    public bool ShowInTooltip { get; set; }

    /// <inheritdoc />
    public string GetString()
    {
        return new SNbtCompound()
        {
            { "show_in_tooltip", new SNbtByteValue(ShowInTooltip) }
        }.ToSNbtString();
    }

    /// <inheritdoc />
    public bool IsValid()
    {
        return true;
    }
}