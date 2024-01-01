﻿namespace MineJason.Data;
using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the entity and score search properties of a scoreboard chat component.
/// </summary>
/// <param name="name">The name of the target entity.</param>
/// <param name="objective">The objective.</param>
/// <param name="value">The value to display instead of the score.</param>
public struct ScoreboardSearcher(string name, string objective, string? value = null) : IEquatable<ScoreboardSearcher>
{
    /// <summary>
    /// Gets or sets the target scoreboard entity of this instance.
    /// </summary>
    /// <value>
    /// If set to <c>*</c>, selects the reader; if a name is specified, selects the player or virtual entity with such name;
    /// if a target selector that selects only one entity is specified, selects the entity. Anything else is invalid.
    /// </value>
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;

    /// <summary>
    /// Gets or sets the objective to display the score of.
    /// </summary>
    /// <value>
    /// The identifier of the objective to display the score of.
    /// </value>
    [JsonPropertyName("objective")]
    public string Objective { get; set; } = objective;

    /// <summary>
    /// Gets or sets the text to display instead of the score.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; } = value;

    public override bool Equals(object? obj)
    {
        return obj is ScoreboardSearcher other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Objective, Value);
    }

    public bool Equals(ScoreboardSearcher other)
    {
        return Name == other.Name && Objective == other.Objective && Value == other.Value;
    }

    public static bool operator ==(ScoreboardSearcher left, ScoreboardSearcher right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ScoreboardSearcher left, ScoreboardSearcher right)
    {
        return !(left == right);
    }
}