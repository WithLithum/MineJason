// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using MineJason.Data.Coordinates;

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
    /// <param name="selector">
    /// The selector that selects an entity. Minecraft restricts that this selector must be limited
    /// to a single entity.
    /// </param>
    /// <remarks>
    /// <para>
    /// This method makes no attempt to validate the selector string, other than ensuring that it
    /// is not null, empty or consisted only of whitespace characters. It is the caller's
    /// responsibility to ensure that the selector is valid according to Minecraft's selector
    /// syntax.
    /// </para>
    /// <para>
    /// Invalid selectors may lead to runtime errors when the component is used in-game.
    /// </para>
    /// </remarks>
    /// <returns>An instance of the NBT component builder for an entity NBT component.</returns>
    /// <exception cref="ArgumentException">
    /// The <paramref name="selector"/> is empty or consisted only of white space characters.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="selector"/> is <see langword="null"/>.
    /// </exception>
    public EntityNbtComponentBuilder Entity(string selector)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(selector);

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