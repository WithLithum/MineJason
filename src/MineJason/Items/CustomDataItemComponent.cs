// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;
using MineJason.Data;

/// <summary>
/// Represents an item component that is used to store custom data. This class cannot be inherited.
/// </summary>
[PublicAPI]
public sealed class CustomDataItemComponent : IItemComponent
{
    /// <summary>
    /// The identifier of this data component.
    /// </summary>
    public static readonly ResourceLocation Id = new("minecraft", "custom_data");
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomDataItemComponent"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public CustomDataItemComponent(NbtProvider value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets or sets the custom data value.
    /// </summary>
    public NbtProvider Value { get; set; }

    /// <inheritdoc />
    public string GetString()
    {
        return Value.ToString();
    }

    /// <inheritdoc />
    public bool IsValid()
    {
        // We don't validate NBT providers here.
        return true;
    }
}