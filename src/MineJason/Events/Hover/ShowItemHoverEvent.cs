namespace MineJason.Events.Hover;

using System.Text.Json.Serialization;

public sealed class ShowItemHoverEvent(string id, int count, string nbt) : HoverEvent, IEquatable<ShowItemHoverEvent>
{
    [JsonPropertyName("id")]
    public string Id { get; } = id;

    [JsonPropertyName("count")]
    public int Count { get; } = count;

    [JsonPropertyName("nbt")]
    public string Nbt { get; } = nbt;

    public bool Equals(ShowItemHoverEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Count == other.Count && Nbt == other.Nbt;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is ShowItemHoverEvent other && Equals(other);
    }

    public override bool Equals(HoverEvent? other)
    {
        return other is ShowItemHoverEvent e && Equals(e);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Count, Nbt);
    }
}
