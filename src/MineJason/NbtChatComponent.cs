namespace MineJason;

using System.Text.Json.Serialization;
using MineJason.Data;

/// <summary>
/// Represents a chat component that resolves into the value of the NBT entry upon being displayed.
/// </summary>
/// <param name="source">The source of the NBT value.</param>
/// <param name="path">The path to the NBT value.</param>
/// <param name="block">The block to read block NBT values from.</param>
/// <param name="entity">The entity to read entity NBT values from.</param>
/// <param name="storage">The storage to read entity NBT values from.</param>
/// <param name="separator">The separator to separate between multiple NBT files.</param>
public class NbtChatComponent(NbtDataSource source, string path, string? block, string? entity, string? storage, ChatComponent? separator = null)
    : ChatComponent("nbt"),
        IEquatable<NbtChatComponent>
{
    /// <summary>
    /// Gets the source of the NBT value.
    /// </summary>
    [JsonPropertyName("source")]
    public NbtDataSource Source { get; } = source;

    /// <summary>
    /// Gets the path to the NBT entry.
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; } = path;

    /// <summary>
    /// Gets the block to read NBT from.
    /// </summary>
    [JsonPropertyName("block")]
    public string? Block { get; } = block;

    /// <summary>
    /// Gets the entity to read NBT from.
    /// </summary>
    [JsonPropertyName("entity")]
    public string? Entity { get; } = entity;

    /// <summary>
    /// Gets the storage to read NBT from.
    /// </summary>
    [JsonPropertyName("storage")]
    public string? Storage { get; } = storage;

    /// <summary>
    /// Gets the separator to separate between NBT values.
    /// </summary>
    [JsonPropertyName("separator")]
    public ChatComponent? Separator { get; } = separator;

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <inheritdoc />
    public override bool Equals(ChatComponent? other)
    {
        return base.Equals(other) && other is NbtChatComponent component && this.Equals(component); 
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine((int)Source, Path, Block, Entity, Storage, Separator);
    }
}