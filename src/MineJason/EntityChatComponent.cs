// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason;

using JetBrains.Annotations;
using MineJason.Data.Selectors;
using MineJason.Serialization.TextJson;
using MineJason.Text;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

/// <summary>
/// Represents an entity selector chat component that resolves into a list of entity names upon being
/// presented to the user.
/// </summary>
[PublicAPI]
[JsonConverter(typeof(TextComponentConverter))]
public sealed record EntityChatComponent :
    ChatComponent,
    IEquatable<EntityChatComponent>
{
    /// <summary>
    /// Initializes a new instance of <see cref="EntityChatComponent"/>.
    /// </summary>
    public EntityChatComponent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="EntityChatComponent"/> with the specified selector
    /// and separator.
    /// </summary>
    /// <param name="selector">The selector.</param>
    /// <param name="separator">The chat component to separate entity names.</param>
    [SetsRequiredMembers]
    public EntityChatComponent(IEntitySelector selector, ChatComponent? separator = null)
    {
        Selector = selector;
        Separator = separator;
    }

    internal EntityChatComponent(in TextComponentCreationInfo creationInfo) : base(creationInfo)
    {
    }

    /// <summary>
    /// Gets the entity selector.
    /// </summary>
    public required IEntitySelector Selector { get; init; }

    /// <summary>
    /// Gets chat component to separate entity names.
    /// </summary>
    public ChatComponent? Separator { get; init; }

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
