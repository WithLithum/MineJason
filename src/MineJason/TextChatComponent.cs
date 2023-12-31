namespace MineJason;

public sealed class TextChatComponent(string text) : ChatComponent("text"), IEquatable<TextChatComponent>
{
    public string Text { get; } = text;

    public bool Equals(TextChatComponent? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Text == other.Text;
    }

    public override bool Equals(ChatComponent? other)
    {
        return other is TextChatComponent component && StyleEquals(this, component) && Equals(component);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is TextChatComponent other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Text.GetHashCode();
    }
}