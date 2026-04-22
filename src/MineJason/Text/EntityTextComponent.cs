// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Serialization.TextJson;

namespace MineJason.Text;

/// <summary>
/// Represents a text components that resolves into a list of entity names selected by the
/// specified selector. This class cannot be inherited
/// </summary>
/// <remarks>
/// <note type="warning">
/// This class makes no attempt to validate the selector string, other than ensuring that it is not
/// null, empty or consisted only of whitespace characters. It is the caller's responsibility to
/// ensure that the selector is valid according to Minecraft's selector syntax.
/// </note>
/// <para>
/// This type of component is resolved on the server side and will display nothing if resolved on
/// the client as-is.
/// </para>
/// </remarks>
[PublicAPI]
[JsonConverter(typeof(TextComponentConverter))]
public sealed record EntityTextComponent :
    TextComponent,
    IEquatable<EntityTextComponent>
{
    /// <summary>
    /// Initializes a new instance of <see cref="EntityTextComponent"/>.
    /// </summary>
    public EntityTextComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="EntityTextComponent"/> with the specified selector
    /// and separator.
    /// </summary>
    /// <param name="selector">The selector pattern.</param>
    /// <param name="separator">The chat component to separate entity names.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="selector"/> is empty or consisted only of white space characters.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="selector"/> is <see langword="null"/>.
    /// </exception>
    [SetsRequiredMembers]
    public EntityTextComponent(string selector, TextComponent? separator = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(selector);

        Selector = selector;
        Separator = separator;
    }

    internal EntityTextComponent(in TextComponentCreationInfo creationInfo) : base(creationInfo)
    {
    }

    /// <summary>
    /// Gets the entity selector pattern.
    /// </summary>
    public required string Selector { get; init; }

    /// <summary>
    /// Gets chat component to separate entity names.
    /// </summary>
    public TextComponent? Separator { get; init; }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Selector, Separator);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"selector{{{Selector}, {Separator}}}";
    }
}
