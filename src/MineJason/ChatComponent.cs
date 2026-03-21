// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason;

using System.Drawing;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Components;
using MineJason.Components.Builders;
using MineJason.Data.Coordinates;
using MineJason.Data.Selectors;
using MineJason.Events;
using MineJason.Events.Hover;
using MineJason.Serialization.TextJson;
using MineJason.Text;
using MineJason.Text.Builders;

/// <summary>
/// Represents a text component.
/// </summary>
[JsonConverter(typeof(TextComponentConverter))]
[PublicAPI]
public abstract record ChatComponent
{
    /// <summary>
    /// Initializes a new instance of <see cref="ChatComponent"/> class.
    /// </summary>
    private protected ChatComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ChatComponent"/> class with the specified data.
    /// </summary>
    /// <param name="creationInfo">The data.</param>
    private protected ChatComponent(in TextComponentCreationInfo creationInfo)
    {
        Bold = creationInfo.Bold;
        Italic = creationInfo.Italic;
        Obfuscated = creationInfo.Obfuscated;
        Underline = creationInfo.Underline;
        Strikethrough = creationInfo.Strikethrough;
        Color = creationInfo.Color;
        ShadowColor = creationInfo.ShadowColor;
        Font = creationInfo.Font;
        Insertion = creationInfo.Insertion;
        ClickEvent = creationInfo.ClickEvent;
        HoverEvent = creationInfo.HoverEvent;
        Extra = creationInfo.Extra;
    }

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
    public IChatColor? Color { get; init; }

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
    public IReadOnlyList<ChatComponent>? Extra { get; init; }

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
    public static ChatComponent CreateTranslatable(string text,
        string? fallback = null,
        IReadOnlyList<ChatComponent>? with = null)
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
    /// <returns>The scoreboard chat component.</returns>
    [PublicAPI]
    public static ChatComponent CreateScore(string name, string objective)
    {
        return new ScoreboardChatComponent(new ScoreboardChatComponent.Data(name, objective));
    }

    /// <summary>
    /// Creates a scoreboard component.
    /// </summary>
    /// <param name="name">The name of the scoreboard entity to display the score of.</param>
    /// <param name="objective">The objective to display the score of.</param>
    /// <param name="value">The value to display instead of the score.</param>
    /// <returns>The scoreboard chat component.</returns>
    [PublicAPI]
    [Obsolete("The 'value' parameter has no use. Use CreateScore(name, objective) instead.")]
    public static ChatComponent CreateScore(string name, string objective, string? value)
    {
        return CreateScore(name, objective);
    }

    /// <summary>
    /// Returns a new instance of <see cref="ScoreTextComponentBuilder"/> that can be used to
    /// create <see cref="ScoreboardChatComponent"/> fluently.
    /// </summary>
    /// <returns>The builder.</returns>
    public static ScoreTextComponentBuilder CreateScore()
    {
        return new ScoreTextComponentBuilder();
    }

    /// <summary>
    /// Creates an entity selector component.
    /// </summary>
    /// <param name="selector">The selector.</param>
    /// <param name="separator">The chat component that is used to separate between multiple entities.</param> 
    /// <returns>The selector component.</returns>
    [PublicAPI]
    public static ChatComponent CreateSelector(IEntitySelector selector, ChatComponent? separator = null)
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

    /// <summary>
    /// Initiates the construction of an NBT chat component.
    /// </summary>
    /// <returns>The NBT chat component building initiator.</returns>
    [PublicAPI]
    public static NbtComponentBuilderFactory CreateNbt()
    {
        return new NbtComponentBuilderFactory();
    }

    /// <summary>
    /// Creates an NBT component with a block as the source.
    /// </summary>
    /// <param name="block">The position of the block.</param>
    /// <param name="path">The NBT path.</param>
    /// <returns>The created component.</returns>
    [PublicAPI]
    public static ChatComponent CreateNbt(BlockPosition block, string path)
    {
        return new BlockNbtChatComponent(block, path);
    }

    /// <summary>
    /// Creates an NBT component with an entity as the source.
    /// </summary>
    /// <param name="entity">The selector that selects a single entity as a source.</param>
    /// <param name="path">The NBT path.</param>
    /// <returns>The created component.</returns>
    [PublicAPI]
    public static ChatComponent CreateNbt(IEntitySelector entity, string path)
    {
        return new EntityNbtChatComponent(entity, path);
    }

    /// <summary>
    /// Creates an NBT component with a storage as the source.
    /// </summary>
    /// <param name="storage">The identifier of the storage.</param>
    /// <param name="path">The NBT path.</param>
    /// <returns>The created component.</returns>
    [PublicAPI]
    public static ChatComponent CreateNbt(ResourceLocation storage, string path)
    {
        return new StorageNbtChatComponent(storage, path);
    }

    /// <summary>
    /// Creates a new object text component that displays the specified sprite and optionally
    /// source the sprite from the specified atlas.
    /// </summary>
    /// <param name="sprite">The sprite to display.</param>
    /// <param name="atlas">
    /// The atlas to source the sprite from.  If <see langword="null"/>, uses
    /// <c>minecraft:blocks</c>.
    /// </param>
    /// <returns>The created component.</returns>
    public static ChatComponent CreateAtlasObject(ResourceLocation sprite,
        ResourceLocation? atlas = null)
    {
        return new AtlasObjectTextComponent(sprite, atlas);
    }

    /// <summary>
    /// Creates a new object text component that displays the specified sprite and optionally
    /// source the sprite from the specified atlas.
    /// </summary>
    /// <param name="sprite">The sprite to display.</param>
    /// <param name="atlas">
    /// The atlas to source the sprite from. If <see langword="null"/>, uses
    /// <c>minecraft:blocks</c>.
    /// </param>
    /// <param name="fallback">
    /// The text component to display instead in places where the object is not supported.
    /// </param>
    /// <returns>The created component.</returns>
    public static ChatComponent CreateAtlasObject(ResourceLocation sprite,
        ResourceLocation? atlas,
        ChatComponent? fallback)
    {
        return new AtlasObjectTextComponent(sprite, atlas)
        {
            Fallback = fallback
        };
    }

    #endregion

    #region Fluent syntax methods

    /// <summary>
    /// Returns a new copy of this instance with the specified bold status.
    /// </summary>
    /// <param name="bold">The bold status to set to.</param>
    /// <returns>A new copy of this instance with bold status set.</returns>
    public ChatComponent Embolden(bool bold = true)
    {
        return this with { Bold = bold };
    }

    /// <summary>
    /// Returns a new copy of this instance with the specified italic status.
    /// </summary>
    /// <param name="italic">The italic status to set to.</param>
    /// <returns>A new copy of this instance with the italic status set.</returns>
    public ChatComponent Italicise(bool italic = true)
    {
        return this with { Italic = italic };
    }

    /// <summary>
    /// Sets the color of this instance.
    /// </summary>
    /// <param name="color">The color of this instance.</param>
    /// <returns>This instance for chaining.</returns>
    public ChatComponent WithColor(IChatColor? color)
    {
        return this with { Color = color };
    }

    /// <summary>
    /// Returns a new copy of this instance with the specified shadow color.
    /// </summary>
    /// <param name="color">The shadow color to set to.</param>
    /// <returns>A new copy of this instance with the specified shadow color.</returns>
    public ChatComponent WithShadowColor(Color color)
    {
        return this with { ShadowColor = color };
    }

    /// <summary>
    /// Sets the font of this instance.
    /// </summary>
    /// <param name="font">The font of this instance.</param>
    /// <returns>This instance for chaining.</returns>
    public ChatComponent WithFont(ResourceLocation? font)
    {
        return this with { Font = font };
    }

    /// <summary>
    /// Appends the specified component as the extra component to this component.
    /// </summary>
    /// <param name="component">The component to append.</param>
    /// <returns>This instance for chaining.</returns>
    public ChatComponent Append(ChatComponent component)
    {
        ArgumentNullException.ThrowIfNull(component);

        List<ChatComponent> newList = Extra != null
            ? [.. Extra]
            : [];

        newList.Add(component);

        return this with { Extra = newList };
    }

    #endregion

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(Bold);
        hashCode.Add(Italic);
        hashCode.Add(Underline);
        hashCode.Add(Strikethrough);
        hashCode.Add(Color);
        hashCode.Add(Font);
        hashCode.Add(Insertion);
        hashCode.Add(ClickEvent);
        hashCode.Add(HoverEvent);
        hashCode.Add(Extra);

        return hashCode.ToHashCode();
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return
            $"<color={Color},font={Font},bold={Bold},italic={Italic},underlined={Underline},strikethrough={Strikethrough},{Environment.NewLine}" +
            $"clickEvent={ClickEvent}{Environment.NewLine}" +
            $"hoverEvent={HoverEvent}{Environment.NewLine}" +
            $"extra={Extra}>";
    }
}