// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Components;

using MineJason.Data;
using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a chat component that resolves into the value of the NBT entry from an entity
/// upon being displayed.
/// </summary>
public sealed class EntityNbtChatComponent : BaseNbtChatComponent, IEquatable<EntityNbtChatComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNbtChatComponent"/> class.
    /// </summary>
    /// <param name="path">The NBT path.</param>
    /// <param name="entity">The entity to source NBT from.</param>
    public EntityNbtChatComponent(string path, EntitySelector entity) : base(NbtDataSource.Entity, path)
    {
        Entity = entity;
    }

    /// <summary>
    /// Gets or sets the entity to source the NBT data from.
    /// </summary>
    [JsonPropertyName("entity")]
    public EntitySelector Entity { get; set; }
    
    /// <inheritdoc/>
    public bool Equals(EntityNbtChatComponent? other)
    {
        return other != null && base.Equals(other) &&
            Entity.Equals(other.Entity) &&
            Path.Equals(other.Path, StringComparison.Ordinal);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as EntityNbtChatComponent);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Path.GetHashCode(), Entity.GetHashCode());
    }
}
