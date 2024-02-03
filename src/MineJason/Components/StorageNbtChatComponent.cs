// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Components;

using MineJason.Data;
using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an NBT chat component that sources the value from a storage. This class cannot be inherited.
/// </summary>
public sealed class StorageNbtChatComponent : BaseNbtChatComponent, IEquatable<StorageNbtChatComponent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StorageNbtChatComponent"/> class.
    /// </summary>
    /// <param name="storage">The ID of the storage.</param>
    /// <param name="path">The NBT path.</param>
    public StorageNbtChatComponent(ResourceLocation storage, string path) : base(path)
    {
        Storage = storage;
    }

    /// <summary>
    /// Gets or sets the ID of the storage to source NBT from.
    /// </summary>
    [JsonPropertyName("storage")]
    public ResourceLocation Storage { get; set; }
    
    /// <inheritdoc/>
    public bool Equals(StorageNbtChatComponent? other)
    {
        return other is not null && base.Equals(other)
            && other.Storage.Equals(Storage);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as StorageNbtChatComponent);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Storage.GetHashCode());
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[storage={Storage},nbt={Path}]";
    }
}
