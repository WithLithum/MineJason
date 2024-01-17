// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Components.Builders;

using JetBrains.Annotations;
using MineJason.Data;

/// <summary>
/// Provides fluent syntax building for <see cref="EntityChatComponent"/>.
/// </summary>
[PublicAPI]
public sealed class EntityComponentBuilder : ChatComponentBuilder<EntityChatComponent>
{
    private EntitySelector? _selector;

    /// <summary>
    /// Sets the selector.
    /// </summary>
    /// <param name="selector">The selector.</param>
    /// <returns>This instance.</returns>
    public EntityComponentBuilder Selector(EntitySelector selector)
    {
        _selector = selector;
        return this;
    }
    
    /// <inheritdoc />
    public override EntityChatComponent Build()
    {
        if (_selector is null)
        {
            throw new InvalidOperationException("Selector cannot be null");
        }

        var component = new EntityChatComponent(_selector);
        Apply(component);
        return component;
    }
}