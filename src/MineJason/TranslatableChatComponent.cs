﻿namespace MineJason;

/// <summary>
/// Represents a chat component that displays a translatable text.
/// </summary>
/// <param name="translate">The translation key.</param>
/// <param name="fallback">The text to display in case of the translation key is missing.</param>
/// <param name="with">The arguments of the translation.</param>
public sealed class TranslatableChatComponent(string translate, string? fallback = null, IList<ChatComponent>? with = null) : ChatComponent("translatable"), IEquatable<TranslatableChatComponent>
{
    /// <summary>
    /// Gets the translation key.
    /// </summary>
    public string Translate { get; } = translate;

    /// <summary>
    /// Gets the fallback text.
    /// </summary>
    public string? Fallback { get; } = fallback;

    /// <summary>
    /// Gets the arguments.
    /// </summary>
    public IList<ChatComponent>? With { get; } = with;

    /// <inheritdoc />
    public override bool Equals(ChatComponent? other)
    {
        return other is TranslatableChatComponent component && StyleEquals(this, component) && Equals(component);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is TranslatableChatComponent other && Equals(other);
    }

    /// <inheritdoc />
    public bool Equals(TranslatableChatComponent? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Translate == other.Translate && Fallback == other.Fallback && Equals(With, other.With);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Translate, Fallback, With);
    }
}