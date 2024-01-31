// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason;
using System;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Data;

/// <summary>
/// Represents an entity selector chat component that resolves into a list of entity names upon being
/// presented to the user.
/// </summary>
/// <param name="selector">The selector.</param>
/// <param name="separator">The chat component to separate entity names.</param>
[PublicAPI]
public sealed class EntityChatComponent(EntitySelector selector, ChatComponent? separator = null) : ChatComponent("selector"),
    IEquatable<EntityChatComponent>
{
    /// <summary>
    /// Gets the entity selector.
    /// </summary>
    [JsonPropertyName("selector")]
    public EntitySelector Selector { get; } = selector;

    /// <summary>
    /// Gets chat component to separate entity names.
    /// </summary>
    [JsonPropertyName("separator")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ChatComponent? Separator { get; } = separator;

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
    
    /// <inheritdoc />
    public override bool Equals(ChatComponent? other)
    {
        return base.Equals(other);
    }
    
    /// <inheritdoc />
    public bool Equals(EntityChatComponent? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        #if DEBUG

        Console.WriteLine(base.Equals(other));

        #endif

        return base.Equals(other) && Selector.Equals(other.Selector) && Equals(Separator, other.Separator);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is EntityChatComponent other && Equals(other);
    }
}
