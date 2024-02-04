// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Components.Builders;

/// <summary>
/// Constructs a NBT chat component with data sourced from a storage file. This class cannot be inherited.
/// </summary>
public sealed class StorageNbtComponentBuilder : NbtComponentBuilder<StorageNbtChatComponent>
{
    internal StorageNbtComponentBuilder(ResourceLocation storage)
    {
        _storage = storage;
    }
    
    private readonly ResourceLocation _storage;

    /// <inheritdoc />
    protected override StorageNbtChatComponent CreateComponent(string path)
    {
        return new StorageNbtChatComponent(_storage, path);
    }
}