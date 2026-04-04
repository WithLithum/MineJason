// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using JetBrains.Annotations;
using MineJason.Events;
using MineJason.Events.Hover;
using MineJason.Text.Colors;
using System.Drawing;
using MineJason.Text.Behaviour.Click;

namespace MineJason.Text.Builders;

/// <summary>
/// Constructs new instances of <see cref="TextComponent"/> derivatives.
/// </summary>
[PublicAPI]
public abstract class TextComponentBuilder<TTarget> where TTarget : TextComponent
{
    private ITextColor? _color;
    private ResourceLocation? _font;

    private bool _bold;
    private bool _italic;
    private bool _underlined;
    private bool _strikethrough;
    private bool _obfuscated;

    private string? _insertion;
    private HoverEvent? _hoverEvent;
    private ClickEvent? _clickEvent;
    private IList<TextComponent>? _extras;

    private Color? _shadowColor;

    /// <summary>
    /// Sets the color of the component.
    /// </summary>
    /// <param name="color">The color to set to.</param>
    /// <returns>This instance.</returns>
    public TextComponentBuilder<TTarget> Color(ITextColor? color)
    {
        _color = color;
        return this;
    }

    /// <summary>
    /// Sets the font of the component.
    /// </summary>
    /// <param name="location">The location to set to.</param>
    /// <returns>This instance.</returns>
    public TextComponentBuilder<TTarget> Font(ResourceLocation? location)
    {
        _font = location;
        return this;
    }

    /// <summary>
    /// Enables the bold style of the component.
    /// </summary>
    /// <returns>This instance.</returns>
    public TextComponentBuilder<TTarget> Bold()
    {
        _bold = true;
        return this;
    }

    /// <summary>
    /// Enables the italic style of the component.
    /// </summary>
    /// <returns>This instance.</returns>
    public TextComponentBuilder<TTarget> Italic()
    {
        _italic = true;
        return this;
    }

    /// <summary>
    /// Enables the underlined style of the component.
    /// </summary>
    /// <returns>This instance.</returns>
    public TextComponentBuilder<TTarget> Underline()
    {
        _underlined = true;
        return this;
    }

    /// <summary>
    /// Enables the strikethrough style of the component.
    /// </summary>
    /// <returns>This instance.</returns>
    public TextComponentBuilder<TTarget> Strikethrough()
    {
        _strikethrough = true;
        return this;
    }

    /// <summary>
    /// Enables the obfuscated style of the component.
    /// </summary>
    /// <returns>This instance.</returns>
    public TextComponentBuilder<TTarget> Obfuscate()
    {
        _obfuscated = true;
        return this;
    }

    /// <summary>
    /// Sets the text to insert into the chat box whenever this component was clicked while
    /// <c>SHIFT</c> is held.
    /// </summary>
    /// <param name="insertion">The text to insert.</param>
    /// <returns>This instance.</returns>
    public TextComponentBuilder<TTarget> Insertion(string? insertion)
    {
        _insertion = insertion;
        return this;
    }

    /// <summary>
    /// Sets the click event of the component.
    /// </summary>
    /// <param name="clickEvent">The click event.</param>
    /// <returns>This instance.</returns>
    public TextComponentBuilder<TTarget> ClickEvent(ClickEvent? clickEvent)
    {
        _clickEvent = clickEvent;
        return this;
    }

    /// <summary>
    /// Sets the hover event of the component.
    /// </summary>
    /// <param name="hoverEvent">The hover event.</param>
    /// <returns>This instance.</returns>
    public TextComponentBuilder<TTarget> HoverEvent(HoverEvent? hoverEvent)
    {
        _hoverEvent = hoverEvent;
        return this;
    }

    /// <summary>
    /// Sets the list of components to be appended after this component.
    /// </summary>
    /// <param name="extras">The extra components.</param>
    /// <returns>This instance.</returns>
    public TextComponentBuilder<TTarget> Extras(IList<TextComponent>? extras)
    {
        _extras = extras;
        return this;
    }

    /// <summary>
    /// Sets the shadow color of the component.
    /// </summary>
    /// <param name="color">The shadow color.</param>
    /// <returns>This instance.</returns>
    public TextComponentBuilder<TTarget> ShadowColor(Color? color)
    {
        _shadowColor = color;
        return this;
    }

    /// <summary>
    /// Gathers text component data and return them encapsulated in a new instance of the
    /// <see cref="TextComponentCreationInfo"/> structure.
    /// </summary>
    protected TextComponentCreationInfo CreateData()
    {
        return new TextComponentCreationInfo
        {
            Bold = _bold ? true : null,
            Italic = _italic ? true : null,
            Strikethrough = _strikethrough ? true : null,
            Underline = _underlined ? true : null,
            Obfuscated = _obfuscated ? true : null,

            HoverEvent = _hoverEvent,
            ClickEvent = _clickEvent,

            Insertion = _insertion,

            Font = _font,
            Color = _color,

            Extra = _extras?.AsReadOnly(),
            ShadowColor = _shadowColor
        };
    }

    /// <summary>
    /// Assembles the result of this builder.
    /// </summary>
    /// <returns>The result.</returns>
    [PublicAPI]
    public abstract TTarget Build();
}