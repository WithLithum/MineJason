// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

namespace MineJason.Text.Behaviour.Click;

/// <summary>
/// Represents a click event that copies the specified text to the clipboard. This class cannot be
/// inherited.
/// </summary>
[JsonConverter(typeof(ClickEventConverter))]
public sealed class CopyToClipboardClickEvent : ClickEvent, IEquatable<CopyToClipboardClickEvent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CopyToClipboardClickEvent"/> class.
    /// </summary>
    public CopyToClipboardClickEvent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="CopyToClipboardClickEvent"/> with the specified
    /// value.
    /// </summary>
    /// <param name="value">The text to copy.</param>
    [SetsRequiredMembers]
    public CopyToClipboardClickEvent(string value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets the text to copy to the clipboard.
    /// </summary>
    public required string Value { get; init; }

    /// <inheritdoc />
    public bool Equals(CopyToClipboardClickEvent? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is CopyToClipboardClickEvent other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
