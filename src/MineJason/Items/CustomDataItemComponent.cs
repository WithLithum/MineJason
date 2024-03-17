// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;
using MineJason.Data;
using MineJason.SNbt.Values;

/// <summary>
/// Represents an item component that is used to store custom data. This class cannot be inherited.
/// </summary>
[PublicAPI]
public sealed class CustomDataItemComponent : IItemComponent
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ResourceLocation Type =
        new ResourceLocation("minecraft", "custom_data");
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomDataItemComponent"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public CustomDataItemComponent(SNbtCompound value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets or sets the custom data value.
    /// </summary>
    public SNbtCompound Value { get; set; }

    /// <inheritdoc />
    public string GetString()
    {
        return Value.ToSNbtString();
    }

    /// <inheritdoc />
    public bool IsValid()
    {
        // We don't validate NBT providers here.
        return true;
    }
}