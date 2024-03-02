// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;

/// <summary>
/// Represents a registry of component types. This class cannot be inherited.
/// </summary>
public sealed class ItemComponentTypeRegistry
{
    private readonly Dictionary<ResourceLocation, ItemComponentType> Types = new();

    /// <summary>
    /// Registers the specified component type and associates it with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="type">The type to register.</param>
    public void Add(ResourceLocation id, ItemComponentType type)
    {
        Types.Add(id, type);
    }
    
    /// <summary>
    /// Gets or sets the item component type associated with the specified ID.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>The type.</returns>
    public ItemComponentType this[ResourceLocation id]
    {
        get => Types[id];
        set => Types[id] = value;
    }

    /// <summary>
    /// Gets the value associated with the specified identifier.
    /// </summary>
    /// <param name="id">The ID.</param>
    /// <param name="value">The returned value.</param>
    /// <returns><see langword="true"/> if the specified type exists in this registry; otherwise, <see langword="false"/>.</returns>
    public bool TryGetValue(ResourceLocation id, out ItemComponentType value)
    {
        return Types.TryGetValue(id, out value);
    }

    /// <summary>
    /// Adds the default entries to a registry.
    /// </summary>
    [PublicAPI]
    public static void AddDefaults(ItemComponentTypeRegistry registry)
    {
        registry.Add(CustomDataItemComponent.Id, new(typeof(CustomDataItemComponent)));
        registry.Add(DamageItemComponent.Id, new(typeof(DamageItemComponent)));
        registry.Add(DyedColorItemComponent.Id, new(typeof(DyedColorItemComponent)));
        registry.Add(CustomModelDataItemComponent.Id, new(typeof(CustomModelDataItemComponent)));
        registry.Add(CustomNameItemComponent.Id, new(typeof(CustomNameItemComponent)));
        registry.Add(EnchantmentGlintOverrideItemComponent.Id, new(typeof(EnchantmentGlintOverrideItemComponent)));
    }
}