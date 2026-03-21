// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Components;

using MineJason.Components.Builders;
using MineJason.Data.Selectors;
using MineJason.Text;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents a chat component that resolves into the value of the NBT entry from an entity
/// upon being displayed.
/// </summary>
public sealed record EntityNbtChatComponent : BaseNbtChatComponent
{
    /// <summary>
    /// The value of the <c>source</c> field identifying this type.
    /// </summary>
    public const string SourceName = "entity";

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNbtChatComponent"/> class.
    /// </summary>
    public EntityNbtChatComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNbtChatComponent"/> class.
    /// </summary>
    /// <param name="entity">The entity to source NBT from.</param>
    /// <param name="path">The NBT path.</param>
    [SetsRequiredMembers]
    public EntityNbtChatComponent(IEntitySelector entity, string path)
    {
        Entity = entity;
        Path = path;
    }

    [SetsRequiredMembers]
    internal EntityNbtChatComponent(in TextComponentCreationInfo creationInfo,
        in NBTTextComponentCreationInfo nbtInfo,
        IEntitySelector selector) : base(creationInfo, nbtInfo)
    {
        Entity = selector;
    }

    /// <summary>
    /// Gets or sets the entity to source the NBT data from.
    /// </summary>
    public required IEntitySelector Entity { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[entity={Entity},nbt={Path}]";
    }
}
