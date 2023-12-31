namespace MineJason.Events;

public abstract class ChangePageClickEvent(int value) 
{
    public int Value { get; set; } = value;
}