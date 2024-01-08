// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Components.Builders;

using JetBrains.Annotations;

/// <summary>
/// Provides a fluent syntax builder for <see cref="TranslatableChatComponent"/>.
/// </summary>
[PublicAPI]
public sealed class TranslatableComponentBuilder : ChatComponentBuilder<TranslatableChatComponent>
{
    private string? _translate;
    private string? _fallback;
    private readonly List<ChatComponent> _with = [];
    
    /// <summary>
    /// Sets the translation key value of the component.
    /// </summary>
    /// <param name="value">The value to set to.</param>
    /// <returns>This instance.</returns>
    public TranslatableComponentBuilder Value(string value)
    {
        _translate = value;
        return this;
    }

    /// <summary>
    /// Sets the text displayed when the translation entry was not found.
    /// </summary>
    /// <param name="value">THe fallback text.</param>
    /// <returns>This instance.</returns>
    public TranslatableComponentBuilder Fallback(string value)
    {
        _fallback = value;
        return this;
    }

    /// <summary>
    /// Adds the specified value to the arguments.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>This instance.</returns>
    public TranslatableComponentBuilder With(ChatComponent value)
    {
        _with.Add(value);
        return this;
    }
    
    /// <summary>
    /// Adds the specified values to the arguments.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <returns>This instance.</returns>
    public TranslatableComponentBuilder With(params ChatComponent[] values)
    {
        _with.AddRange(values);
        return this;
    }

    /// <inheritdoc />
    /// <exception cref="InvalidOperationException">The translation key was not set.</exception>
    public override TranslatableChatComponent Build()
    {
        if (_translate == null)
        {
            throw new InvalidOperationException("The text was not set.");
        }
        
        var component = new TranslatableChatComponent(_translate, _fallback);
        Apply(component);
        return component;
    }
}