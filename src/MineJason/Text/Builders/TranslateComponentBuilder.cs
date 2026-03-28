// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using JetBrains.Annotations;

namespace MineJason.Text.Builders;

/// <summary>
/// Constructs a new instance of <see cref="TranslateTextComponent"/>. This class cannot be
/// inherited.
/// </summary>
[PublicAPI]
public sealed class TranslateComponentBuilder : TextComponentBuilder<TranslateTextComponent>
{
    private string? _translate;
    private string? _fallback;
    private readonly List<TextComponent> _with = [];

    /// <summary>
    /// Sets the translation key value of the component.
    /// </summary>
    /// <param name="value">The value to set to.</param>
    /// <returns>This instance.</returns>
    public TranslateComponentBuilder Value(string value)
    {
        _translate = value;
        return this;
    }

    /// <summary>
    /// Sets the text displayed when the translation entry was not found.
    /// </summary>
    /// <param name="value">THe fallback text.</param>
    /// <returns>This instance.</returns>
    public TranslateComponentBuilder Fallback(string value)
    {
        _fallback = value;
        return this;
    }

    /// <summary>
    /// Adds the specified value to the arguments.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>This instance.</returns>
    public TranslateComponentBuilder With(TextComponent value)
    {
        _with.Add(value);
        return this;
    }

    /// <summary>
    /// Adds the specified values to the arguments.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>This instance.</returns>
    public TranslateComponentBuilder With(params TextComponent[] values)
    {
        _with.AddRange(values);
        return this;
    }

    /// <inheritdoc />
    /// <exception cref="InvalidOperationException">The translation key was not set.</exception>
    public override TranslateTextComponent Build()
    {
        if (_translate == null)
        {
            throw new InvalidOperationException("The text was not set.");
        }

        var creationInfo = CreateData();
        return new TranslateTextComponent(creationInfo)
        {
            Translate = _translate,
            Fallback = _fallback,
            With = _with.Count > 0 ? _with : null
        };
    }
}