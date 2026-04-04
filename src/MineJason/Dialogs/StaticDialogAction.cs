// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Diagnostics.CodeAnalysis;
using MineJason.Text.Behaviour.Click;

namespace MineJason.Dialogs;

/// <summary>
/// Represents a dialog action that performs an operation with circumstances not affected by input
/// controls.
/// </summary>
public sealed record StaticDialogAction : DialogAction
{
    /// <summary>
    /// Initializes a new instance of <see cref="StaticDialogAction"/> class.
    /// </summary>
    public StaticDialogAction()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="StaticDialogAction"/> with the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    [SetsRequiredMembers]
    public StaticDialogAction(ClickEvent value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the value of this instance.
    /// </summary>
    public required ClickEvent Value { get; init; }

    /// <summary>
    /// Implicitly encapsulates <see cref="ClickEvent"/> into a new instance of
    /// <see cref="StaticDialogAction"/>.
    /// </summary>
    /// <param name="clickEvent">The value to encapsulate.</param>
    public static implicit operator StaticDialogAction(ClickEvent clickEvent)
    {
        return new StaticDialogAction(clickEvent);
    }

    /// <summary>
    /// Implicitly de-encapsulates the specified dialog action.
    /// </summary>
    /// <param name="value">The dialog action to de-encapsulate.</param>
    public static implicit operator ClickEvent(StaticDialogAction value)
    {
        return value.Value;
    }
}
