// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Serialization.TextJson;

namespace MineJason.Text.Behaviour.Click;

/// <summary>
/// Represents a click event that executes a command.
/// </summary>
[JsonConverter(typeof(ClickEventConverter))]
public sealed class RunCommandClickEvent : ClickEvent, IEquatable<RunCommandClickEvent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RunCommandClickEvent"/> class.
    /// </summary>
    [PublicAPI]
    public RunCommandClickEvent()
    {
    }
    
    /// <summary>
    /// Initializes a new instance of <see cref="RunCommandClickEvent"/> with the specified value.
    /// </summary>
    /// <param name="value">The command to execute.</param>
    [SetsRequiredMembers]
    public RunCommandClickEvent(string value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets the command to execute.
    /// </summary>
    [JsonPropertyName("value")]
    public required string Value { get; init; }
    
    /// <inheritdoc />
    public bool Equals(RunCommandClickEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is RunCommandClickEvent other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
