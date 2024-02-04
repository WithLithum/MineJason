// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Components.Builders;

using System.Diagnostics.CodeAnalysis;
using MineJason.Data;
using MineJason.Data.Coordinates;

/// <summary>
/// Initiates the construction of a NBT chat component. This class cannot be inherited.
/// </summary>
[SuppressMessage("", "CA1822")]
public sealed class NbtComponentBuilderFactory
{
    internal NbtComponentBuilderFactory() {}
    
    /// <summary>
    /// Specifies an entity as the source of data.
    /// </summary>
    /// <param name="selector">The selector that selects an entity.</param>
    /// <returns>An instance of the NBT component builder for an entity NBT component.</returns>
    public EntityNbtComponentBuilder Entity(EntitySelector selector)
    {
        return new EntityNbtComponentBuilder(selector);
    }

    /// <summary>
    /// Specifies a block as the source of data.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <returns>An instance of the NBT component builder for an entity NBT component.</returns>
    public BlockNbtComponentBuilder Block(AnyBlockPosition block)
    {
        return new BlockNbtComponentBuilder(block);
    }

    /// <summary>
    /// Specifies a storage file as the source of data.
    /// </summary>
    /// <param name="storage">The resource location of the storage.</param>
    /// <returns>An instance of the NBT component builder for an entity NBT component.</returns>
    public StorageNbtComponentBuilder Storage(ResourceLocation storage)
    {
        return new StorageNbtComponentBuilder(storage);
    }
}