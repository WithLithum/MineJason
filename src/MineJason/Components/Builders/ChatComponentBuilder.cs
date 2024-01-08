// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Components.Builders;

using JetBrains.Annotations;
using MineJason.Events;
using MineJason.Events.Hover;

/// <summary>
/// Provides a base for chat component building.
/// </summary>
[PublicAPI]
public abstract class ChatComponentBuilder<TTarget> where TTarget : ChatComponent
{
    private IChatColor? _color;
    private ResourceLocation? _font;

    private bool _bold;
    private bool _italic;
    private bool _underlined;
    private bool _strikethrough;
    private bool _obfuscated;
    
    private string? _insertion;
    private HoverEvent? _hoverEvent;
    private ClickEvent? _clickEvent;

    /// <summary>
    /// Sets the color of the component.
    /// </summary>
    /// <param name="color">The color to set to.</param>
    /// <returns>This instance.</returns>
    public ChatComponentBuilder<TTarget> Color(IChatColor? color)
    {
        _color = color;
        return this;
    }

    /// <summary>
    /// Sets the font of the component.
    /// </summary>
    /// <param name="location">The location to set to.</param>
    /// <returns>This instance.</returns>
    public ChatComponentBuilder<TTarget> Font(ResourceLocation? location)
    {
        _font = location;
        return this;
    }

    /// <summary>
    /// Enables the bold style of the component.
    /// </summary>
    /// <returns>This instance.</returns>
    public ChatComponentBuilder<TTarget> Bold()
    {
        _bold = true;
        return this;
    }

    /// <summary>
    /// Enables the italic style of the component.
    /// </summary>
    /// <returns>This instance.</returns>
    public ChatComponentBuilder<TTarget> Italic()
    {
        _italic = true;
        return this;
    }

    /// <summary>
    /// Enables the underlined style of the component.
    /// </summary>
    /// <returns>This instance.</returns>
    public ChatComponentBuilder<TTarget> Underline()
    {
        _underlined = true;
        return this;
    }

    /// <summary>
    /// Enables the strikethrough style of the component.
    /// </summary>
    /// <returns>This instance.</returns>
    public ChatComponentBuilder<TTarget> Strikethrough()
    {
        _strikethrough = true;
        return this;
    }

    /// <summary>
    /// Enables the obfuscated style of the component.
    /// </summary>
    /// <returns>This instance.</returns>
    public ChatComponentBuilder<TTarget> Obfuscate()
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
    public ChatComponentBuilder<TTarget> Insertion(string? insertion)
    {
        _insertion = insertion;
        return this;
    }

    /// <summary>
    /// Sets the click event of the component.
    /// </summary>
    /// <param name="clickEvent">The click event.</param>
    /// <returns>This instance.</returns>
    public ChatComponentBuilder<TTarget> ClickEvent(ClickEvent? clickEvent)
    {
        _clickEvent = clickEvent;
        return this;
    }

    /// <summary>
    /// Sets the hover event of the component.
    /// </summary>
    /// <param name="hoverEvent">The hover event.</param>
    /// <returns>This instance.</returns>
    public ChatComponentBuilder<TTarget>  HoverEvent(HoverEvent? hoverEvent)
    {
        _hoverEvent = hoverEvent;
        return this;
    }

    /// <summary>
    /// Applies the properties of this builder to the specified component.
    /// </summary>
    /// <param name="component">The component to apply to.</param>
    protected void Apply(ChatComponent component)
    {
        if (_bold) component.Bold = true;
        if (_italic) component.Italic = true;
        if (_strikethrough) component.Strikethrough = true;
        if (_underlined) component.Underline = true;
        if (_obfuscated) component.Obfuscated = true;

        component.HoverEvent = _hoverEvent;
        component.ClickEvent = _clickEvent;

        component.Insertion = _insertion;

        component.Font = _font;
        component.Color = _color;
    }

    /// <summary>
    /// Assembles the result of this builder.
    /// </summary>
    /// <returns>The result.</returns>
    [PublicAPI]
    public abstract TTarget Build();
}