// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;

/// <summary>
/// Represents a component that sets a value which can be used in item models to specify custom item
/// models. This class cannot be inherited.
/// </summary>
[PublicAPI]
public sealed class CustomModelDataItemComponent : InvariantItemComponent<int>
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ResourceLocation Type = 
        new ResourceLocation("minecraft", "custom_model_data");

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
