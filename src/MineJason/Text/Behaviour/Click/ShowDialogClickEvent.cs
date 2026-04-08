// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using JetBrains.Annotations;
using System.Text.Json.Serialization;
using MineJason.Dialogs.Reference;
using MineJason.Serialization.TextJson;

namespace MineJason.Text.Behaviour.Click;

/// <summary>
/// Represents a click event that shows a dialog. This class cannot be inherited.
/// </summary>
[JsonConverter(typeof(ClickEventConverter))]
[PublicAPI]
public sealed class ShowDialogClickEvent : ClickEvent, IEquatable<ShowDialogClickEvent>
{
    /// <summary>
    /// Gets the reference to the dialog to be displayed.
    /// </summary>
    public required DialogReference Dialog { get; init; }

    /// <inheritdoc />
    public bool Equals(ShowDialogClickEvent? other)
    {
        return Equals(Dialog, other?.Dialog);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as ShowDialogClickEvent);
    }

    /// <summary>
    /// Returns the hash code of the current object.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Dialog, nameof(ShowDialogClickEvent));
    }
}
