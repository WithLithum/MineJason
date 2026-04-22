// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Serialization.TextJson;

namespace MineJason.Text.Behaviour.Click;

/// <summary>
/// Represents a click event that enters the specified text into the chat box.
/// </summary>
[PublicAPI]
[JsonConverter(typeof(ClickEventConverter))]
public sealed class SuggestCommandClickEvent : ClickEvent, IEquatable<SuggestCommandClickEvent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SuggestCommandClickEvent"/> class.
    /// </summary>
    public SuggestCommandClickEvent()
    {
    }
    
    /// <summary>
    /// Initializes a new instance of <see cref="SuggestCommandClickEvent"/> with the specified
    /// value.
    /// </summary>
    /// <param name="value">The text to copy into the chat box.</param>
    [SetsRequiredMembers]
    public SuggestCommandClickEvent(string value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets the text to copy into the chat box.
    /// </summary>
    [JsonPropertyName("value")]
    public required string Value { get; init; }

    /// <inheritdoc />
    public bool Equals(SuggestCommandClickEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is SuggestCommandClickEvent other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
