namespace MineJason.Data.Selectors;

using System.Text;
using JetBrains.Annotations;

/// <summary>
/// Represents a game mode matching.
/// </summary>
[PublicAPI]
public struct GameModeMatch : IEquatable<GameModeMatch>
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

        if (Exclude != null && Exclude.Count != 0)
        {
            foreach (var mode in Exclude)
            {
                Comma();
                builder.Append("gamemode=!").Append(mode.ToString().ToLowerInvariant());
            }
        }

        return builder.ToString();
    }

    /// <inheritdoc />
    [System.Diagnostics.Contracts.Pure]
    public bool Equals(GameModeMatch other)
    {
        return Include == other.Include && Exclude.SequenceEqual(other.Exclude);
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

    public static bool operator ==(GameModeMatch left, GameModeMatch right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(GameModeMatch left, GameModeMatch right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Creates a new instance of <see cref="GameModeMatch"/> that matches an exact game mode.
    /// </summary>
    /// <param name="mode">The game mode.</param>
    /// <returns>The created instance.</returns>
    public static GameModeMatch MatchExact(GameMode mode)
    {
        return new GameModeMatch(mode);
    }

    /// <summary>
    /// Creates a new instance of <see cref="GameModeMatch"/> that matches game modes except for the ones
    /// specified.
    /// </summary>
    /// <param name="modes">The game modes to exclude.</param>
    /// <returns>The created instance.</returns>
    public static GameModeMatch MatchExclude(params GameMode[] modes)
    {
        var result = new GameModeMatch();
        result.Exclude.AddRange(modes);
        return result;
    }
}