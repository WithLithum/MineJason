namespace MineJason.Events;

public sealed class RunCommandClickEvent(string value)
{
    public string Value { get; set; } = value;
}
