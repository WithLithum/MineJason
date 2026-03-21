// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs;

/// <summary>
/// Specifies the actions that the player can use to react to a dialog.
/// </summary>
public enum DialogType
{
    /// <summary>
    /// A dialog with a single button, which the exit action is by default that button.
    /// </summary>
    Notice,
    /// <summary>
    /// A dialog with "Yes" and "No" actions. The default exit action is the "No" action.
    /// </summary>
    Confirmation,
    /// <summary>
    /// A dialog with multiple custom actions defined in columns.
    /// </summary>
    MultiAction,
    /// <summary>
    /// A dialog that shows buttons arranged in columns that are bound to server links, defined by
    /// the server, with an optional exit action.
    /// </summary>
    ServerLinks,
    /// <summary>
    /// A dialog that shows buttons arranged in columns that are bound to a list of specified
    /// dialog, with an optional exit action.
    /// </summary>
    DialogList
}
