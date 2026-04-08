// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Text.Behaviour.Click;

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

/// <summary>
/// Represents a click action that contains arbitrary string values indicating a custom action to
/// be interpreted and triggered by the server. This class cannot be inherited.
/// </summary>
[JsonConverter(typeof(ClickEventConverter))]
public sealed class CustomClickEvent : ClickEvent, IEquatable<CustomClickEvent>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomClickEvent"/> class.
    /// </summary>
    public CustomClickEvent()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="CustomClickEvent"/> with the specified ID and
    /// an optional payload.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="payload">The payload.</param>
    [SetsRequiredMembers]
    public CustomClickEvent(ResourceLocation id, string? payload = null)
    {
        Id = id;
        Payload = payload;
    }

    /// <summary>
    /// Gets the identifier of this custom action.
    /// </summary>
    public required ResourceLocation Id { get; init; }

    /// <summary>
    /// Gets the payload of this custom action.
    /// </summary>
    public string? Payload { get; init; }

    /// <inheritdoc />
    public bool Equals(CustomClickEvent? other)
    {
        return other is not null &&
            (ReferenceEquals(this, other) || (
            // Conditions for value equality

            Id.Equals(other.Id) &&
            string.Equals(Payload, other.Payload, StringComparison.Ordinal)
            ));
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as CustomClickEvent);
    }

    /// <summary>
    /// Gets the hash code of this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Payload);
    }
}
