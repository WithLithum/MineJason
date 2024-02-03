// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Components;

using MineJason.Data;
using System;

/// <summary>
/// Represents an NBT chat component that sources the value from a storage. This class cannot be inherited.
/// </summary>
public sealed class StorageNbtChatComponent : BaseNbtChatComponent, IEquatable<StorageNbtChatComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StorageNbtChatComponent"/> class.
    /// </summary>
    /// <param name="storageId">The ID of the storage.</param>
    /// <param name="path">The NBT path.</param>
    public StorageNbtChatComponent(ResourceLocation storageId, string path) : base(NbtDataSource.Storage, path)
    {
        StorageId = storageId;
    }

    /// <summary>
    /// Gets or sets the ID of the storage to source NBT from.
    /// </summary>
    public ResourceLocation StorageId { get; set; }
    
    /// <inheritdoc/>
    public bool Equals(StorageNbtChatComponent? other)
    {
        return other is not null && base.Equals(other)
            && other.StorageId.Equals(StorageId);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as StorageNbtChatComponent);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), StorageId.GetHashCode());
    }
}
