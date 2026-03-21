// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs.Reference;
/// <summary>
/// Represents a dialog reference that defines a dialog in-line.
/// </summary>
public sealed record InlineDialogSource : OneDialogSource
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InlineDialogSource"/> class.
    /// </summary>
    /// <param name="dialog">The dialog to contain.</param>
    public InlineDialogSource(Dialog dialog)
    {
        Dialog = dialog;
    }

    /// <summary>
    /// Gets the dialog contained within this instance.
    /// </summary>
    public Dialog Dialog { get; }
}
