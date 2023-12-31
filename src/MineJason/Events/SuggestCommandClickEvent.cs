namespace MineJason.Events;

public sealed class SuggestCommandClickEvent(string value)
{
    public string Value { get; set; } = value;
}
