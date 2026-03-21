// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Defines a data-driven GUI screen with a "Yes" and a "No" button.
/// </summary>
public sealed record ConfirmationDialog : Dialog
{
    /// <summary>
    /// The identifier of the dialog type.
    /// </summary>
    public static readonly ResourceLocation Id = new("minecraft", "confirmation");

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfirmationDialog"/> class.
    /// </summary>
    public ConfirmationDialog()
    {
    }

    [SetsRequiredMembers]
    internal ConfirmationDialog(DialogCreationInfo creationInfo,
        DialogButton yes,
        DialogButton no) : base(creationInfo)
    {
        Yes = yes;
        No = no;
    }

    /// <summary>
    /// Gets the definition for the "Yes" button.
    /// </summary>
    public required DialogButton Yes { get; init; }

    /// <summary>
    /// Gets the definition for the "No"  button.
    /// </summary>
    public required DialogButton No { get; init; }
}
