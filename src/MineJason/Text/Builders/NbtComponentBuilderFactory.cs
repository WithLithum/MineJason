// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using MineJason.Data.Coordinates;
using MineJason.Data.Selectors;

namespace MineJason.Text.Builders;

/// <summary>
/// Initiates the construction of a NBT chat component. This class cannot be inherited.
/// </summary>
[PublicAPI]
[SuppressMessage("Performance", "CA1822:Mark members as static",
    Justification = "Fluent building model")]
[SuppressMessage("", "S2325",
    Justification = "Fluent building model")]
[SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression",
    Justification = "The above one was false positive-d")]
public sealed class NbtComponentBuilderFactory
{
    internal NbtComponentBuilderFactory() { }

    /// <summary>
    /// Specifies an entity as the source of data.
    /// </summary>
    /// <param name="selector">The selector that selects an entity.</param>
    /// <returns>An instance of the NBT component builder for an entity NBT component.</returns>
    public EntityNbtComponentBuilder Entity(IEntitySelector selector)
    {
        return new EntityNbtComponentBuilder(selector);
    }

    /// <summary>
    /// Specifies a block as the source of data.
    /// </summary>
    /// <param name="block">The block.</param>
    /// <returns>An instance of the NBT component builder for an entity NBT component.</returns>
    public BlockNbtComponentBuilder Block(BlockPosition block)
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