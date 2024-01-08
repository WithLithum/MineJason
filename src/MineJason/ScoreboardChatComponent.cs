// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason;

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Data;

/// <summary>
/// Represents a chat component that resolves to the value of the scoreboard entity for the specified
/// objective upon being presented to the user.
/// </summary>
/// <param name="score">The scoreboard details.</param>
[PublicAPI]
public sealed class ScoreboardChatComponent(ScoreboardSearcher score) : ChatComponent("score"),
    IEquatable<ScoreboardChatComponent>
{
    /// <summary>
    /// Gets the scoreboard details.
    /// </summary>
    [JsonPropertyName("score")]
    public ScoreboardSearcher Score { get; } = score;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"score={{name={Score.Name},objective={Score.Objective},value={Score.Value ?? "null"}}}";
    }

    /// <inheritdoc />
    public override bool Equals(ChatComponent? other)
    {
        return other is not null 
               && base.Equals(other) 
               && other is ScoreboardChatComponent component 
               && Score.Equals(component.Score);
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is ChatComponent component)
        {
            return Equals(component);
        }
        
        return ReferenceEquals(this, obj) || obj is ScoreboardChatComponent other && Equals(other);
    }
    
    /// <inheritdoc />
    public bool Equals(ScoreboardChatComponent? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return base.Equals(other) && Score.Equals(other.Score);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Score.GetHashCode();
    }
}