﻿// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Events;

using System.Text.Json.Serialization;
using JetBrains.Annotations;

/// <summary>
/// Represents a click event that jumps to a specified page in a book.
/// </summary>
/// <param name="value"></param>
[PublicAPI]
public sealed class ChangePageClickEvent(int value) : ClickEvent, IEquatable<ChangePageClickEvent>
{
    /// <summary>
    /// Gets or sets the page to jump to.
    /// </summary>
    [JsonPropertyName("value")]
    public int Value { get; } = value;

    /// <inheritdoc />
    public bool Equals(ChangePageClickEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is ChangePageClickEvent other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Value;
    }
}