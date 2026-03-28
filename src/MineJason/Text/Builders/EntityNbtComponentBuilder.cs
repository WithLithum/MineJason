// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using JetBrains.Annotations;
using MineJason.Data.Selectors;

namespace MineJason.Text.Builders;

/// <summary>
/// Builds a NBT chat component with data sourced from a single entity. This class cannot be inherited.
/// </summary>
[PublicAPI]
public sealed class EntityNbtComponentBuilder : NbtComponentBuilder<EntityNbtTextComponent>
{
    internal EntityNbtComponentBuilder(IEntitySelector selector)
    {
        _selector = selector;
    }
    
    private readonly IEntitySelector _selector;

    /// <inheritdoc />
    public override EntityNbtTextComponent Build()
    {
        var data = CreateData();
        var nbtData = CreateNBTData();

        return new EntityNbtTextComponent(data, nbtData, _selector);
    }
}