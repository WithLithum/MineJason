namespace MineJason;

using System.Text.Json.Serialization;
using MineJason.Data;

public sealed class ScoreboardChatComponent(ScoreboardSearcher score) : ChatComponent("score")
{
    [JsonPropertyName("score")]
    public ScoreboardSearcher Score { get; } = score;

    public override bool Equals(ChatComponent? other)
    {
        return other is not null 
               && base.Equals(other) 
               && other is ScoreboardChatComponent component 
               && Score.Equals(component.Score);
    }

    public override string ToString()
    {
        return $"score={{name={Score.Name},objective={Score.Objective},value={Score.Value ?? "null"}}}";
    }

    public override int GetHashCode()
    {
        return Score.GetHashCode();
    }
}