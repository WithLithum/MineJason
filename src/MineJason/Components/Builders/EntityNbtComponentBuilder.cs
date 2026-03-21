// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Components.Builders;

using JetBrains.Annotations;
using MineJason.Data;
using MineJason.Data.Selectors;

/// <summary>
/// Builds a NBT chat component with data sourced from a single entity. This class cannot be inherited.
/// </summary>
[PublicAPI]
public sealed class EntityNbtComponentBuilder : NbtComponentBuilder<EntityNbtChatComponent>
{
    internal EntityNbtComponentBuilder(IEntitySelector selector)
    {
        _selector = selector;
    }
    
    private readonly IEntitySelector _selector;

    /// <inheritdoc />
    public override EntityNbtChatComponent Build()
    {
        var data = CreateData();
        var nbtData = CreateNBTData();

        return new EntityNbtChatComponent(data, nbtData, _selector);
    }
}