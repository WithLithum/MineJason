﻿using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Events;
using MineJason.Events.Hover;
using MineJason.Serialization.TextJson;

namespace MineJason;

[JsonConverter(typeof(ChatComponentConverter))]
public abstract class ChatComponent(string? type)
{
    protected string? Type { get; set; } = type;

    /// <summary>
    /// Gets or sets whether this component is displayed in bold font.
    /// </summary>
    public bool? Bold { get; set; }

    /// <summary>
    /// Gets or sets whether this component is displayed in italic font.
    /// </summary>
    public bool? Italic { get; set; }

    /// <summary>
    /// Gets or sets whether the text of this component is displayed obfuscated.
    /// </summary>
    public bool? Obfuscated { get; set; }

    /// <summary>
    /// Gets or sets whether to render underline below the text of this component.
    /// </summary>
    public bool? Underline { get; set; }

    /// <summary>
    /// Gets or sets whether to render a line at the middle of this component.
    /// </summary>
    public bool? Strikethrough { get; set; }

    /// <summary>
    /// Gets or sets the color of this component.
    /// </summary>
    public IChatColor? Color { get; set; }

    /// <summary>
    /// Gets or sets the font of this component.
    /// </summary>
    public string? Font { get; set; }

    public string? Insertion { get; set; }

    public ClickEvent? ClickEvent { get; set; }

    public HoverEvent? HoverEvent { get; set; }

    public IEnumerable<ChatComponent>? Extra { get; set; }

    [PublicAPI]
    public static ChatComponent CreateText(string text)
    {
        return new TextChatComponent(text);
    }
}
