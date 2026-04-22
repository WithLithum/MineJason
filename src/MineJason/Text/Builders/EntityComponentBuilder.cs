// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using JetBrains.Annotations;

namespace MineJason.Text.Builders;

/// <summary>
/// Constructs a new instance of <see cref="EntityTextComponent"/>.
/// </summary>
[PublicAPI]
public sealed class EntityComponentBuilder : TextComponentBuilder<EntityTextComponent>
{
    private string? _selector;
    private TextComponent? _separator;

    /// <summary>
    /// Sets the selector.
    /// </summary>
    /// <param name="selector">The selector pattern.</param>
    /// <remarks>
    /// <para>
    /// This method makes no attempt to validate the selector string, other than ensuring that it
    /// is not null, empty or consisted only of whitespace characters. It is the caller's
    /// responsibility to ensure that the selector is valid according to Minecraft's selector
    /// syntax.
    /// </para>
    /// <para>
    /// Invalid selectors may lead to runtime errors when the component is used in-game.
    /// </para>
    /// </remarks>
    /// <returns>The current instance for chaining.</returns>
    /// <exception cref="ArgumentException">
    /// The <paramref name="selector"/> is empty or consisted only of white space characters.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="selector"/> is <see langword="null"/>.
    /// </exception>
    public EntityComponentBuilder Selector(string selector)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(selector);

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
        if (string.IsNullOrWhiteSpace(_selector))
        {
            throw new InvalidOperationException(
                "Selector cannot be null, empty or consist entirely of whitespace.");
        }

        var creationInfo = CreateData();

        return new EntityTextComponent(creationInfo)
        {
            Selector = _selector,
            Separator = _separator
        };
    }
}