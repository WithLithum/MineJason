// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Components.Builders;

using JetBrains.Annotations;
using MineJason.Data;

/// <summary>
/// Builds a NBT chat component with data sourced from a single entity. This class cannot be inherited.
/// </summary>
[PublicAPI]
public sealed class EntityNbtComponentBuilder : NbtComponentBuilder<EntityNbtChatComponent>
{
    internal EntityNbtComponentBuilder(EntitySelector selector)
    {
        _selector = selector;
    }
    
    private readonly EntitySelector _selector;

    /// <inheritdoc />
    protected override EntityNbtChatComponent CreateComponent(string path)
    {
        return new EntityNbtChatComponent(_selector, path);
    }
}