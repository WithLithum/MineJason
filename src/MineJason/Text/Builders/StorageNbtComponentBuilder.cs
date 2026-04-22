// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Text.Builders;

/// <summary>
/// Constructs a new instance of <see cref="StorageNbtComponentBuilder"/>. This class cannot be
/// inherited.
/// </summary>
public sealed class StorageNbtComponentBuilder : NbtComponentBuilder<StorageNbtTextComponent>
{
    internal StorageNbtComponentBuilder(ResourceLocation storage)
    {
        _storage = storage;
    }

    private readonly ResourceLocation _storage;

    /// <inheritdoc />
    public override StorageNbtTextComponent Build()
    {
        var baseData = CreateData();
        var nbtData = CreateNBTData();

        return new StorageNbtTextComponent(baseData, nbtData, _storage);
    }
}