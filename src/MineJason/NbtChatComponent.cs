namespace MineJason;

using System.Text.Json.Serialization;
using MineJason.Data;

public class NbtChatComponent(NbtDataSource source, string path, string? block, string? entity, string? storage, ChatComponent? separator = null)
    : ChatComponent("nbt"),
        IEquatable<NbtChatComponent>
{
    [JsonPropertyName("source")]
    public NbtDataSource Source { get; } = source;

    [JsonPropertyName("path")]
    public string Path { get; } = path;

    [JsonPropertyName("block")]
    public string? Block { get; } = block;

    [JsonPropertyName("entity")]
    public string? Entity { get; } = entity;

    [JsonPropertyName("storage")]
    public string? Storage { get; } = storage;

    [JsonPropertyName("separator")]
    public ChatComponent? Separator { get; } = separator;

    public bool Equals(NbtChatComponent? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return base.Equals(other) && Source == other.Source && Path == other.Path && Block == other.Block && Entity == other.Entity && Storage == other.Storage && Equals(Separator, other.Separator);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return Equals((NbtChatComponent)obj);
    }

    public override bool Equals(ChatComponent? other)
    {
        return base.Equals(other) && other is NbtChatComponent component && this.Equals(component); 
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Source, Path, Block, Entity, Storage, Separator);
    }
}