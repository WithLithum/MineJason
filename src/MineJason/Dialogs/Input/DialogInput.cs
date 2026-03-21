// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs.Input;

/// <summary>
/// Represents a dialog control that receives user input.
/// </summary>
public abstract record DialogInput
{
    internal DialogInput()
    {
    }

    /// <summary>
    /// Gets the macro template name to substitute value with.
    /// </summary>
    public required string Key { get; init; }

    /// <summary>
    /// Gets the label to display to the left of the input.
    /// </summary>
    public required ChatComponent Label { get; init; }
}
