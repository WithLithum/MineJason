// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Extras.Selectors.Matching;

/// <summary>
/// Represents a tag search operation.
/// </summary>
public readonly struct TagMatch(string tag, bool present) : IEquatable<TagMatch>
{
    /// <summary>
    /// Gets or sets the tag to check for.
    /// </summary>
    public string Tag { get; } = tag;

    /// <summary>
    /// Gets or sets a value indicating whether the tag is required to be present, or required to be not present. 
    /// </summary>
    public bool Present { get; } = present;

    /// <inheritdoc />
    public bool Equals(TagMatch other)
    {
        return Tag == other.Tag && Present == other.Present;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is TagMatch other && Equals(other);
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
    public static bool operator ==(TagMatch left, TagMatch right)
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
    public static bool operator !=(TagMatch left, TagMatch right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Returns a copy of the specified <paramref name="value"/> with <see cref="Present"/> property negated.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The specified <paramref name="value"/> with <see cref="Present"/> property negated.</returns>
    public static TagMatch operator !(TagMatch value)
    {
        return new TagMatch(value.Tag, !value.Present);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{(Present ? "" : "!")}{Tag}";
    }
}