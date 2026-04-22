// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

namespace MineJason.Text.Behaviour.Click;

/// <summary>
/// Represents a click event that jumps to a specified page in a book. This class cannot be
/// inherited.
/// </summary>
[JsonConverter(typeof(ClickEventConverter))]
public sealed class ChangePageClickEvent : ClickEvent, IEquatable<ChangePageClickEvent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangePageClickEvent"/> class.
    /// </summary>
    public ChangePageClickEvent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ChangePageClickEvent"/> with the specified value.
    /// </summary>
    /// <param name="value">The value</param>
    [SetsRequiredMembers]
    public ChangePageClickEvent(int value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets or sets the page to jump to.
    /// </summary>
    public required int Value { get; init; }

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