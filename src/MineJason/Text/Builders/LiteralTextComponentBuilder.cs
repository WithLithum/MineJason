// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using JetBrains.Annotations;

namespace MineJason.Text.Builders;

/// <summary>
/// Provides a fluent syntax builder for <see cref="LiteralTextComponent"/>.
/// </summary>
[PublicAPI]
public sealed class LiteralTextComponentBuilder : TextComponentBuilder<LiteralTextComponent>
{
    private string? _text;

    /// <summary>
    /// Sets the text value of the component.
    /// </summary>
    /// <param name="text">The text value to set to.</param>
    /// <returns>This instance.</returns>
    public LiteralTextComponentBuilder Value(string text)
    {
        _text = text;
        return this;
    }
    
    /// <inheritdoc />
    /// <exception cref="InvalidOperationException">The text value was not set.</exception>
    public override LiteralTextComponent Build()
    {
        if (_text == null)
        {
            throw new InvalidOperationException("The text was not set.");
        }

        var creationInfo = CreateData();
        return new LiteralTextComponent(creationInfo)
        {
            Text = _text
        };
    }
}