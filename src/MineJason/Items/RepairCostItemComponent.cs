// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;

/// <summary>
/// Represents a component that sets repair costs. This class cannot be inherited.
/// </summary>
[PublicAPI]
public sealed class RepairCostItemComponent : PositiveNumberItemComponent
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ResourceLocation Type =
        new ResourceLocation("minecraft", "repair_cost");
    
    /// <summary>
    /// Initializes a new instance of the <see cref="RepairCostItemComponent"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public RepairCostItemComponent(int value) : base(value)
    {
    }
}