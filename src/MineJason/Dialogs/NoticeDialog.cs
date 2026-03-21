// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Defines a data-driven GUI screen with one button, that is, the "OK" button.
/// </summary>
public sealed record NoticeDialog : Dialog
{
    /// <summary>
    /// The identifier of the dialog type.
    /// </summary>
    public static readonly ResourceLocation Id = new("minecraft", "notice");

    /// <summary>
    /// Initializes a new instance of <see cref="NoticeDialog"/> class.
    /// </summary>
    public NoticeDialog()
    {
    }

    [SetsRequiredMembers]
    internal NoticeDialog(DialogCreationInfo creationInfo) : base(creationInfo)
    {
    }
    
    /// <summary>
    /// Gets the definition for the "OK" button at the bottom of the screen.
    /// </summary>
    public DialogButton? Action { get; init; }
}
