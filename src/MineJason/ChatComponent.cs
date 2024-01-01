using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Events;
using MineJason.Events.Hover;
using MineJason.Serialization.TextJson;

namespace MineJason;

[JsonConverter(typeof(ChatComponentConverter))]
public abstract class ChatComponent(string? type) : IEquatable<ChatComponent>
{
    protected string? Type { get; set; } = type;

    /// <summary>
    /// Gets or sets whether this component is displayed in bold font.
    /// </summary>
    [JsonPropertyName("bold")]
    public bool? Bold { get; set; }

    /// <summary>
    /// Gets or sets whether this component is displayed in italic font.
    /// </summary>
    [JsonPropertyName("italic")]
    public bool? Italic { get; set; }

    /// <summary>
    /// Gets or sets whether the text of this component is displayed obfuscated.
    /// </summary>
    [JsonPropertyName("obfuscated")]
    public bool? Obfuscated { get; set; }

    /// <summary>
    /// Gets or sets whether to render underline below the text of this component.
    /// </summary>
    [JsonPropertyName("underlined")]
    public bool? Underline { get; set; }

    /// <summary>
    /// Gets or sets whether to render a line at the middle of this component.
    /// </summary>
    [JsonPropertyName("strikethrough")]
    public bool? Strikethrough { get; set; }

    /// <summary>
    /// Gets or sets the color of this component.
    /// </summary>
    [JsonPropertyName("color")]
    public IChatColor? Color { get; set; }

    /// <summary>
    /// Gets or sets the font of this component.
    /// </summary>
    [JsonPropertyName("font")]
    public string? Font { get; set; }

    [JsonPropertyName("insertion")]
    public string? Insertion { get; set; }

    [JsonPropertyName("clickEvent")]
    public ClickEvent? ClickEvent { get; set; }

    [JsonPropertyName("hoverEvent")]
    public HoverEvent? HoverEvent { get; set; }

    [JsonPropertyName("extra")]
    public IEnumerable<ChatComponent>? Extra { get; set; }

    [PublicAPI]
    public static ChatComponent CreateText(string text)
    {
        return new TextChatComponent(text);
    }

    [PublicAPI]
    public static ChatComponent CreateTranslatable(string text)
    {
        return new TranslatableChatComponent(text);
    }

    public abstract bool Equals(ChatComponent? other);

    public abstract override int GetHashCode();

    public override bool Equals(object? obj)
    {
        return obj is ChatComponent component && this.Equals(component);
    }

    public static bool StyleEquals(ChatComponent component1, ChatComponent component2)
    {
        return component1.Color == component2.Color
               && component1.Font == component2.Font
               && component1.Insertion == component2.Insertion
               && component1.Italic == component2.Italic
               && component1.Underline == component2.Underline
               && component1.Strikethrough == component2.Strikethrough
               && Equals(component1.ClickEvent, component2.ClickEvent)
               && Equals(component1.HoverEvent, component2.HoverEvent) 
               && Equals(component1.Extra, component2.Extra);
    }
}
