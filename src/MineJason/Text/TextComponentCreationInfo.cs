// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Drawing;
using MineJason.Text.Behaviour.Click;
using MineJason.Text.Behaviour.Hover;
using MineJason.Text.Colors;

namespace MineJason.Text;

/// <summary>
/// Encapsulates data for common properties of a text component. 
/// </summary>
public readonly ref struct TextComponentCreationInfo
{
    /// <summary>
    /// Gets or sets whether this component is displayed in bold font.
    /// </summary>
    public bool? Bold { get; init; }

    /// <summary>
    /// Gets or sets whether this component is displayed in italic font.
    /// </summary>
    public bool? Italic { get; init; }

    /// <summary>
    /// Gets or sets whether the text of this component is displayed obfuscated.
    /// </summary>
    public bool? Obfuscated { get; init; }

    /// <summary>
    /// Gets or sets whether to render underline below the text of this component.
    /// </summary>
    public bool? Underline { get; init; }

    /// <summary>
    /// Gets or sets whether to render a line at the middle of this component.
    /// </summary>
    public bool? Strikethrough { get; init; }

    /// <summary>
    /// Gets or sets the color of this component.
    /// </summary>
    public ITextColor? Color { get; init; }

    /// <summary>
    /// Gets or sets the shadow color of this text component.
    /// </summary>
    public Color? ShadowColor { get; init; }

    /// <summary>
    /// Gets or sets the font of this component.
    /// </summary>
    public ResourceLocation? Font { get; init; }

    /// <summary>
    /// Gets or sets the text to insert into the chat box whenever this component was clicked while
    /// <c>SHIFT</c> is held.
    /// </summary>
    public string? Insertion { get; init; }

    /// <summary>
    /// Gets or sets the click event of this component.
    /// </summary>
    public ClickEvent? ClickEvent { get; init; }

    /// <summary>
    /// Gets or sets the hover event of this component.
    /// </summary>
    public HoverEvent? HoverEvent { get; init; }

    /// <summary>
    /// Gets or sets the extra components that displays after this component and uses the
    /// styles of this component by default.
    /// </summary>
    public IReadOnlyList<TextComponent>? Extra { get; init; }
}
