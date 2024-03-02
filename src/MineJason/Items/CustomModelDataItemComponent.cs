// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Items;

/// <summary>
/// Represents a component that sets a value which can be used in item models to specify custom item
/// models.
/// </summary>
public sealed class CustomModelDataItemComponent : InvariantItemComponent<int>
{
    /// <summary>
    /// The type ID of this component.
    /// </summary>
    public static readonly ResourceLocation Id = new("minecraft", "custom_model_data");

    /// <summary>
    /// Initialises a new instance of the <see cref="CustomModelDataItemComponent"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public CustomModelDataItemComponent(int value) : base(value)
    {
    }

    /// <inheritdoc/>
    public override bool IsValid()
    {
        return true;
    }
}
