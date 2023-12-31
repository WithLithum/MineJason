namespace MineJason;

public sealed class TranslatableChatComponent(string translate, string? fallback = null, IList<ChatComponent>? with = null) : ChatComponent("translatable"), IEquatable<TranslatableChatComponent>
{
    public string Translate { get; } = translate;

    public string? Fallback { get; } = fallback;

    public IList<ChatComponent>? With { get; } = with;

    public override bool Equals(ChatComponent? other)
    {
        return other is TranslatableChatComponent component && StyleEquals(this, component) && Equals(component);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is TranslatableChatComponent other && Equals(other);
    }

    public bool Equals(TranslatableChatComponent? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Translate == other.Translate && Fallback == other.Fallback && Equals(With, other.With);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Translate, Fallback, With);
    }
}