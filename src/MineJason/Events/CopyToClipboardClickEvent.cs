namespace MineJason.Events;

public sealed class CopyToClipboardClickEvent(string value)
{
    public string Value { get; set; } = value;
}
