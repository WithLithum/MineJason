namespace MineJason;

using System.Text.Json.Serialization;
using MineJason.Data;

/// <summary>
/// Represents a chat component that resolves to the value of the scoreboard entity for the specified
/// objective upon being presented to the user.
/// </summary>
/// <param name="score">The scoreboard details.</param>
public sealed class ScoreboardChatComponent(ScoreboardSearcher score) : ChatComponent("score")
{
    /// <summary>
    /// Gets the scoreboard details.
    /// </summary>
    [JsonPropertyName("score")]
    public ScoreboardSearcher Score { get; } = score;

    /// <inheritdoc />
    public override bool Equals(ChatComponent? other)
    {
        return other is not null 
               && base.Equals(other) 
               && other is ScoreboardChatComponent component 
               && Score.Equals(component.Score);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"score={{name={Score.Name},objective={Score.Objective},value={Score.Value ?? "null"}}}";
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Score.GetHashCode();
    }
}