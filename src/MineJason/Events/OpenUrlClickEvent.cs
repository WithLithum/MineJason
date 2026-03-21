// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Events;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Serialization.TextJson;

/// <summary>
/// Represents an open URL click event, that shows a prompt to open a URL in the default browser.
/// </summary>
[PublicAPI]
[JsonConverter(typeof(ClickEventConverter))]
public sealed class OpenUrlClickEvent : ClickEvent, IEquatable<OpenUrlClickEvent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OpenUrlClickEvent"/> class.
    /// </summary>
    public OpenUrlClickEvent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="OpenUrlClickEvent"/> with the specified value.
    /// </summary>
    /// <param name="value">The URL to open.</param>
    [SetsRequiredMembers]
    public OpenUrlClickEvent(Uri value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets the URL to open.
    /// </summary>
    [JsonPropertyName("value")]
    public required Uri Value { get; init; }

    /// <inheritdoc />
    public bool Equals(OpenUrlClickEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value.Equals(other.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is OpenUrlClickEvent other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}