// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using MineJason.Text;
using MineJason.Utilities;

namespace MineJason.Dialogs;

/// <summary>
/// Represents a button shown on a dialog.
/// </summary>
public record DialogButton
{
    private int? _width;

    /// <summary>
    /// Gets the text shown on the button.
    /// </summary>
    public required TextComponent Label { get; init; }
    
    /// <summary>
    /// Gets the optional tool tip shown when the mouse hovers over the button.
    /// </summary>
    public TextComponent? Tooltip { get; init; }

    /// <summary>
    /// Gets the width of this button.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The value is either zero, negative or greater than <c>1024</c>.
    /// </exception>
    public int? Width
    {
        get => _width;
        init
        {
            ThrowHelper.ThrowIfNegativeOrZero(value, nameof(value));
            ThrowHelper.ThrowIfGreaterThan(value, 1024, nameof(value));

            _width = value;
        }
    }

    /// <summary>
    /// Gets the action that is performed when the button is clicked.
    /// </summary>
    public DialogAction? Action { get; init; }
}
