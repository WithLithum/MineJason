namespace MineJason.Events;

public sealed class OpenUrlClickEvent(Uri value)
{
    public Uri Value { get; set; } = value;
}