namespace MineJason;
using System;
using System.Text.Json.Serialization;

public sealed class EntityChatComponent(string selector, ChatComponent? separator = null) : ChatComponent("selector"),
    IEquatable<EntityChatComponent>
{
    [JsonPropertyName("selector")]
    public string Selector { get; } = selector;

    [JsonPropertyName("separator")]
    public ChatComponent? Separator { get; } = separator;

    public override int GetHashCode()
    {
        return HashCode.Combine(Selector, Separator);
    }

    public override bool Equals(ChatComponent? other)
    {
        return base.Equals(other);
    }

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

        return base.Equals(other) && Selector == other.Selector && Equals(Separator, other.Separator);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is EntityChatComponent other && Equals(other);
    }
}
