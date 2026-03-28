// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Diagnostics.CodeAnalysis;
using MineJason.Components.Builders;
using MineJason.Data.Selectors;

namespace MineJason.Text;

/// <summary>
/// Represents a text component that resolves to the text representation of an NBT value retrieved
/// from an entity. This class cannot be inherited.
/// </summary>
/// <remarks>
/// This type of component is resolved on the server side and will display nothing if resolved on
/// the client as-is.
/// </remarks>
public sealed record EntityNbtTextComponent : NbtTextComponent
{
    /// <summary>
    /// The value of the <c>source</c> field identifying this type.
    /// </summary>
    public const string SourceName = "entity";

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNbtTextComponent"/> class.
    /// </summary>
    public EntityNbtTextComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNbtTextComponent"/> class.
    /// </summary>
    /// <param name="entity">The entity to source NBT from.</param>
    /// <param name="path">The NBT path.</param>
    [SetsRequiredMembers]
    public EntityNbtTextComponent(IEntitySelector entity, string path)
    {
        Entity = entity;
        Path = path;
    }

    [SetsRequiredMembers]
    internal EntityNbtTextComponent(in TextComponentCreationInfo creationInfo,
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
