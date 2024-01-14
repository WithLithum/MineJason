// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Selectors;

using System.Text;
using JetBrains.Annotations;

/// <summary>
/// Represents a game mode matching.
/// </summary>
[PublicAPI]
public sealed class GameModeMatch : IEquatable<GameModeMatch>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GameModeMatch"/> structure.
    /// </summary>
    public GameModeMatch() {}

    /// <summary>
    /// Initializes a new instance of the <see cref="GameModeMatch"/> structure.
    /// </summary>
    /// <param name="include">The game mode that the player must be in to fulfill this requirement.</param>
    public GameModeMatch(GameMode include)
    {
        Include = include;
    }
    
    /// <summary>
    /// Gets or sets the game mode that the player must be in to fulfill this requirement.
    /// </summary>
    public GameMode? Include { get; set; }

    /// <summary>
    /// Gets or sets the game modes that the player must not be in to fulfill this requirement.
    /// </summary>
    public List<GameMode> Exclude { get; } = [];

    /// <inheritdoc />
    public override string ToString()
    {
        var builder = new StringBuilder();
        var first = false;

        void Comma()
        {
            if (first)
            {
                builder.Append(',');
            }

            first = true;
        }
        
        if (Include.HasValue)
        {
            Comma();
            builder.Append("gamemode=").Append(Include.Value.ToString().ToLowerInvariant());
        }

        if (Exclude.Count != 0)
        {
            foreach (var mode in Exclude)
            {
                Comma();
                builder.Append("gamemode=!").Append(mode.ToString().ToLowerInvariant());
            }
        }

        return builder.ToString();
    }

    internal void WriteToBuilder(EntitySelectorArgumentBuilder builder)
    {
        if (Include.HasValue)
        {
            builder.WritePair("gamemode", Include.Value.ToString().ToLowerInvariant());
        }

        if (Exclude.Count != 0)
        {
            foreach (var mode in Exclude)
            {
                builder.WritePair("gamemode", $"!{mode.ToString().ToLowerInvariant()}");
            }
        }
    }

    /// <inheritdoc />
    [System.Diagnostics.Contracts.Pure]
    public bool Equals(GameModeMatch? other)
    {
        return other is not null && Include == other.Include && Exclude.SequenceEqual(other.Exclude);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is GameModeMatch other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Include, Exclude);
    }

    public static bool operator ==(GameModeMatch? left, GameModeMatch? right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(GameModeMatch? left, GameModeMatch? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Add condition to match the specified game mode.
    /// </summary>
    /// <param name="mode">The game mode.</param>
    /// <returns>The created instance.</returns>
    public void MatchExact(GameMode mode)
    {
        Include = mode;
    }

    /// <summary>
    /// Add conditions that matches game modes except for the ones
    /// specified.
    /// </summary>
    /// <param name="modes">The game modes to exclude.</param>
    /// <returns>The created instance.</returns>
    public void MatchExclude(params GameMode[] modes)
    {
        Exclude.AddRange(modes);
    }
}