// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

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
    public override StorageNbtChatComponent Build()
    {
        var baseData = CreateData();
        var nbtData = CreateNBTData();

        return new StorageNbtChatComponent(baseData, nbtData, _storage);
    }
}