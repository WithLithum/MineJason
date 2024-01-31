// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason;

using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Components.Builders;
using MineJason.Data;
using MineJason.Events;
using MineJason.Events.Hover;
using MineJason.Serialization.TextJson;

/// <summary>
/// Represents a chat component.
/// </summary>
/// <param name="type">The type of the chat component.</param>
[JsonConverter(typeof(ChatComponentConverter))]
[PublicAPI]
public abstract class ChatComponent(string? type) : IEquatable<ChatComponent>
{
    /// <summary>
    /// Gets the serializer options that is required for conforming the Minecraft: Java Edition standards.
    /// </summary>
    [Obsolete("Deserialize and serialize directly.")]
    public static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Gets or sets the type of the chat component.
    /// </summary>
    protected string? Type { get; set; } = type;

    /// <summary>
    /// Gets or sets whether this component is displayed in bold font.
    /// </summary>
    [JsonPropertyName("bold")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Bold { get; set; }

    /// <summary>
    /// Gets or sets whether this component is displayed in italic font.
    /// </summary>
    [JsonPropertyName("italic")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Italic { get; set; }

    /// <summary>
    /// Gets or sets whether the text of this component is displayed obfuscated.
    /// </summary>
    [JsonPropertyName("obfuscated")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Obfuscated { get; set; }

    /// <summary>
    /// Gets or sets whether to render underline below the text of this component.
    /// </summary>
    [JsonPropertyName("underlined")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Underline { get; set; }

    /// <summary>
    /// Gets or sets whether to render a line at the middle of this component.
    /// </summary>
    [JsonPropertyName("strikethrough")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Strikethrough { get; set; }

    /// <summary>
    /// Gets or sets the color of this component.
    /// </summary>
    [JsonPropertyName("color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IChatColor? Color { get; set; }

    /// <summary>
    /// Gets or sets the font of this component.
    /// </summary>
    [JsonPropertyName("font")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ResourceLocation? Font { get; set; }

    /// <summary>
    /// Gets or sets the text to insert into the chat box whenever this component was clicked while
    /// <c>SHIFT</c> is held.
    /// </summary>
    [JsonPropertyName("insertion")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Insertion { get; set; }

    /// <summary>
    /// Gets or sets the click event of this component.
    /// </summary>
    [JsonPropertyName("clickEvent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ClickEvent? ClickEvent { get; set; }

    /// <summary>
    /// Gets or sets the hover event of this component.
    /// </summary>
    [JsonPropertyName("hoverEvent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public HoverEvent? HoverEvent { get; set; }

    /// <summary>
    /// Gets or sets the extra components that displays after this component and uses the
    /// styles of this component by default.
    /// </summary>
    [JsonPropertyName("extra")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<ChatComponent>? Extra { get; set; }

    #region Creation methods
    /// <summary>
    /// Creates a text component.
    /// </summary>
    /// <param name="text">The text to display.</param>
    /// <returns>The text component.</returns>
    [PublicAPI]
    public static ChatComponent CreateText(string text)
    {
        return new TextChatComponent(text);
    }

    /// <summary>
    /// Creates a builder for a text component.
    /// </summary>
    /// <returns>The text component builder.</returns>
    [PublicAPI]
    public static TextComponentBuilder CreateText()
    {
        return new TextComponentBuilder();
    }

    /// <summary>
    /// Creates a translatable component.
    /// </summary>
    /// <param name="text">The translation ID to display.</param>
    /// <param name="fallback">
    /// The fallback text to display if the translation ID was not found. If <see langword="null"/>,
    /// uses the translation ID as the fallback.</param>
    /// <param name="with">The arguments of the translated string.</param>
    /// <returns>The translatable chat component.</returns>
    [PublicAPI]
    public static ChatComponent CreateTranslatable(string text, string? fallback = null, IList<ChatComponent>? with = null)
    {
        return new TranslatableChatComponent(text, fallback, with);
    }

    /// <summary>
    /// Creates a builder for a translatable chat component.
    /// </summary>
    /// <returns>The chat component builder.</returns>
    public static TranslatableComponentBuilder CreateTranslatable()
    {
        return new TranslatableComponentBuilder();
    }

    /// <summary>
    /// Creates a scoreboard component.
    /// </summary>
    /// <param name="name">The name of the scoreboard entity to display the score of.</param>
    /// <param name="objective">The objective to display the score of.</param>
    /// <param name="value">The value to display instead of the score.</param>
    /// <returns>The scoreboard chat component.</returns>
    [PublicAPI]
    public static ChatComponent CreateScore(string name, string objective, string? value = null)
    {
        return new ScoreboardChatComponent(new ScoreboardSearcher(name, objective, value));
    }

    /// <summary>
    /// Creates an entity selector component.
    /// </summary>
    /// <param name="selector">The selector.</param>
    /// <param name="separator">The chat component that is used to separate between multiple entities.</param> 
    /// <returns>The selector component.</returns>
    [PublicAPI]
    public static ChatComponent CreateSelector(EntitySelector selector, ChatComponent? separator = null)
    {
        return new EntityChatComponent(selector, separator);
    }

    /// <summary>
    /// Creates a builder that creates an entity selector component.
    /// </summary>
    /// <returns>The builder.</returns>
    [PublicAPI]
    public static EntityComponentBuilder CreateSelector()
    {
        return new EntityComponentBuilder();
    }

    /// <summary>
    /// Create a builder that creates a keybind component.
    /// </summary>
    /// <returns>The builder.</returns>
    [PublicAPI]
    public static KeybindComponentBuilder CreateKeybind()
    {
        return new KeybindComponentBuilder();
    }

    #endregion

    #region Fluent syntax methods

    /// <summary>
    /// Sets the color of this instance.
    /// </summary>
    /// <param name="color">The color of this instance.</param>
    /// <returns>This instance for chaining.</returns>
    public ChatComponent SetColor(IChatColor? color)
    {
        Color = color;
        return this;
    }

    /// <summary>
    /// Sets the font of this instance.
    /// </summary>
    /// <param name="font">The font of this instance.</param>
    /// <returns>This instance for chaining.</returns>
    public ChatComponent SetFont(ResourceLocation? font)
    {
        Font = font;
        return this;
    }

    /// <summary>
    /// Appends the specified component as the extra component to this component.
    /// </summary>
    /// <param name="component">The component to append.</param>
    /// <returns>This instance for chaining.</returns>
    public ChatComponent Append(ChatComponent component)
    {
        ArgumentNullException.ThrowIfNull(component);

        Extra ??= new List<ChatComponent>();

        Extra.Add(component);
        return this;
    }

    #endregion

    /// <inheritdoc />
    public abstract override int GetHashCode();

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ChatComponent component && this.Equals(component);
    }
    
    /// <inheritdoc />
    public virtual bool Equals(ChatComponent? other)
    {
        return other is not null && StyleEquals(this, other);
    }

    /// <summary>
    /// Determines whether the style properties of the first specified component is equivalent to the
    /// other specified component.
    /// </summary>
    /// <param name="component1">The first component.</param>
    /// <param name="component2">The other component.</param>
    /// <returns><see langword="true"/> if the style of the components are equivalent; otherwise, <see langword="false"/>.</returns>
    public static bool StyleEquals(ChatComponent component1, ChatComponent component2)
    {
        Console.WriteLine("Component 1: {0}", component1);
        Console.WriteLine("Component 2: {0}", component2);

        return Equals(component1.Color, component2.Color)
               && component1.Font == component2.Font
               && component1.Insertion == component2.Insertion
               && component1.Italic == component2.Italic
               && component1.Underline == component2.Underline
               && component1.Strikethrough == component2.Strikethrough
               && Equals(component1.ClickEvent, component2.ClickEvent)
               && Equals(component1.HoverEvent, component2.HoverEvent) 
               && Equals(component1.Extra, component2.Extra);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"<color={Color},font={Font},bold={Bold},italic={Italic},underlined={Underline},strikethrough={Strikethrough},{Environment.NewLine}" +
               $"clickEvent={ClickEvent}{Environment.NewLine}" +
               $"hoverEvent={HoverEvent}{Environment.NewLine}" +
               $"extra={Extra}>";
    }
}
