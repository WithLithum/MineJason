// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Dialogs;

/// <summary>
/// Specifies the action to perform upon the dialog submit or back action has been performed.
/// </summary>
public enum DialogAfterAction
{
    /// <summary>
    /// Closes the dialog and return to the most recent none-dialog screen or to the game.
    /// </summary>
    Close,
    /// <summary>
    /// Performs no action and keeps the dialog open.
    /// </summary>
    /// <remarks>
    /// <note type="warning">
    /// This item is not compatible with setting <see cref="Dialog.Pause"/> to
    /// <see langword="true"/>, and although accepted in object model, the combination is invalid
    /// for serialization both by the library and by Minecraft.
    /// </note>
    /// </remarks>
    None,
    /// <summary>
    /// Replaces the dialog with a "Waiting for response" screen. If <see cref="Dialog.Pause"/> is
    /// set to <see langword="true"/>, resumes the game from the pause.
    /// </summary>
    WaitForResponse
}
