namespace MineJason.Components.Builders;

using JetBrains.Annotations;

/// <summary>
/// Provides a fluent syntax builder for <see cref="TextChatComponent"/>.
/// </summary>
[PublicAPI]
public sealed class TextComponentBuilder : ChatComponentBuilder<TextChatComponent>
{
    private string? _text;

    /// <summary>
    /// Sets the text value of the component.
    /// </summary>
    /// <param name="text">The text value to set to.</param>
    /// <returns>This instance.</returns>
    public TextComponentBuilder Value(string text)
    {
        _text = text;
        return this;
    }
    
    /// <inheritdoc />
    /// <exception cref="InvalidOperationException">The text value was not set.</exception>
    public override TextChatComponent Build()
    {
        if (_text == null)
        {
            throw new InvalidOperationException("The text was not set.");
        }
        
        var component = new TextChatComponent(_text);
        Apply(component);
        return component;
    }
}