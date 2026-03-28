// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Components.Builders;

using JetBrains.Annotations;
using MineJason.Text;

/// <summary>
/// Provides fluent syntax builder for <see cref="KeybindTextComponent"/>.
/// </summary>
[PublicAPI]
public class KeybindComponentBuilder : ChatComponentBuilder<KeybindTextComponent>
{
    private string? _keybind;

    /// <summary>
    /// Sets the keybind of the component.
    /// </summary>
    /// <param name="keybind">The keybind ID.</param>
    /// <returns>This instance.</returns>
    public KeybindComponentBuilder Keybind(string keybind)
    {
        _keybind = keybind;
        return this;
    }
    
    /// <inheritdoc />
    public override KeybindTextComponent Build()
    {
        if (string.IsNullOrWhiteSpace(_keybind))
        {
            throw new InvalidOperationException("Keybind was not set.");
        }

        var creationInfo = CreateData();

        return new KeybindTextComponent(creationInfo)
        {
            Keybind = _keybind
        };
    }
}