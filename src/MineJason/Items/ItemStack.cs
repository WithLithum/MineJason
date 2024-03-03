// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

/// <summary>
/// Represents an item stack.
/// </summary>
[SuppressMessage("Naming", "CA1711",
    Justification = "ItemStack is item stack")]
[PublicAPI]
public class ItemStack
{
    /// <summary>
    /// Initialises a new instance of the <see cref="ItemStack"/> structure.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="count">The count.</param>
    public ItemStack(ResourceLocation id, int count)
    {
        Id = id;
        Count = count;
    }
    
    /// <summary>
    /// Reserved for serialization use.
    /// </summary>
    /// <param name="id">The ID.</param>
    /// <param name="count">The count.</param>
    /// <param name="components">The components.</param>
    [JsonConstructor]
    public ItemStack(ResourceLocation id, int count, ItemComponentDictionary components)
    {
        Id = id;
        Count = count;
        Components = components;
    }
    
    /// <summary>
    /// Gets the identifier of the item of this stack.
    /// </summary>
    [JsonPropertyName("id")]
    public ResourceLocation Id { get; }
    
    /// <summary>
    /// Gets the count of the items of this stack.
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; }

    /// <summary>
    /// Gets the component dictionary of this stack.
    /// </summary>
    [JsonPropertyName("components")]
    public ItemComponentDictionary Components { get; } = new();
}