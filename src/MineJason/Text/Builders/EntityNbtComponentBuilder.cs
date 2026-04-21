// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using JetBrains.Annotations;

namespace MineJason.Text.Builders;

/// <summary>
/// Constructs a new instance of <see cref="EntityNbtTextComponent"/>. This class cannot be
/// inherited.
/// </summary>
[PublicAPI]
public sealed class EntityNbtComponentBuilder : NbtComponentBuilder<EntityNbtTextComponent>
{
    internal EntityNbtComponentBuilder(string selector)
    {
        _selector = selector;
    }

    private readonly string _selector;

    /// <inheritdoc />
    public override EntityNbtTextComponent Build()
    {
        var data = CreateData();
        var nbtData = CreateNBTData();

        return new EntityNbtTextComponent(data, nbtData, _selector);
    }
}