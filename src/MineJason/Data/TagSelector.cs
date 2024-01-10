namespace MineJason.Data;

using JetBrains.Annotations;

/// <summary>
/// Represents a tag search operation.
/// </summary>
[PublicAPI]
public struct TagSelector(string tag, bool present) : IEquatable<TagSelector>
{
    /// <summary>
    /// Gets or sets the tag to check for.
    /// </summary>
    public string Tag { get; set; } = tag;

    /// <summary>
    /// Gets or sets a value indicating whether the tag is required to be present, or required to be not present. 
    /// </summary>
    public bool Present { get; set; } = present;

    /// <inheritdoc />
    public bool Equals(TagSelector other)
    {
        return Tag == other.Tag && Present == other.Present;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is TagSelector other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Tag, Present);
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> is equivalent to the instance
    /// to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The left instance.</param>
    /// <param name="right">The right instance.</param>
    /// <returns><see langword="true"/> if the instance to the <paramref name="left"/> is equivalent to the instance
    /// to the <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(TagSelector left, TagSelector right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> does not equals to the instance
    /// to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The left instance.</param>
    /// <param name="right">The right instance.</param>
    /// <returns><see langword="true"/> if the instance to the <paramref name="left"/> is not equivalent to the instance
    /// to the <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(TagSelector left, TagSelector right)
    {
        return !(left == right);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"tag={(Present ? "" : "!")}{Tag}";
    }
}