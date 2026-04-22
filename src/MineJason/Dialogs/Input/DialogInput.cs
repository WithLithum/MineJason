// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Text;

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
    public required TextComponent Label { get; init; }
}
