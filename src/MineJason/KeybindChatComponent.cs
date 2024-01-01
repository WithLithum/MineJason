namespace MineJason;
using System;

public sealed class KeybindChatComponent(string keybind) : ChatComponent("keybind"),
    IEquatable<KeybindChatComponent>
{
    public string Keybind { get; } = keybind;

    public bool Equals(KeybindChatComponent? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return base.Equals(other) && Keybind == other.Keybind;
    }

    public override bool Equals(ChatComponent? other)
    {
        return base.Equals(other) && other is KeybindChatComponent component && Equals(component);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is KeybindChatComponent other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Keybind.GetHashCode();
    }
}
