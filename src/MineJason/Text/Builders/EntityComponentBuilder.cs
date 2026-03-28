// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Components.Builders;

using JetBrains.Annotations;
using MineJason.Data;
using MineJason.Data.Selectors;
using MineJason.Text;
using MineJason.Text.Builders;

/// <summary>
/// Provides fluent syntax building for <see cref="EntityTextComponent"/>.
/// </summary>
[PublicAPI]
public sealed class EntityComponentBuilder : TextComponentBuilder<EntityTextComponent>
{
    private IEntitySelector? _selector;
    private TextComponent? _separator;

    /// <summary>
    /// Sets the selector.
    /// </summary>
    /// <param name="selector">The selector.</param>
    /// <returns>This instance.</returns>
    public EntityComponentBuilder Selector(IEntitySelector selector)
    {
        _selector = selector;
        return this;
    }
    
    /// <summary>
    /// Sets the text component used to separate entity names found.
    /// </summary>
    /// <param name="separator">The separator.</param>
    /// <returns>This instance.</returns>
    public EntityComponentBuilder Separator(TextComponent? separator)
    {
        _separator = separator;
        return this;
    }

    /// <inheritdoc />
    public override EntityTextComponent Build()
    {
        if (_selector is null)
        {
            throw new InvalidOperationException("Selector cannot be null");
        }

        var creationInfo = CreateData();

        return new EntityTextComponent(creationInfo)
        {
            Selector = _selector,
            Separator = _separator
        };
    }
}