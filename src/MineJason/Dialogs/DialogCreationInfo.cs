// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs;

using MineJason.Dialogs.Input;
using System.Collections.Generic;

internal readonly ref struct DialogCreationInfo
{
    /// <summary>
    /// Gets the title of this dialog screen.
    /// </summary>
    public required ChatComponent Title { get; init; }

    /// <summary>
    /// Gets the text to display on a button leading this dialog in a dialog list screen.
    /// </summary>
    public ChatComponent? ExternalTitle { get; init; }

    /// <summary>
    /// Gets the body messages of this dialog screen.
    /// </summary>
    public IReadOnlyList<DialogMessage>? Body { get; init; }

    /// <summary>
    /// Gets the input controls of this dialog screen.
    /// </summary>
    public IReadOnlyList<DialogInput>? Inputs { get; init; }

    /// <summary>
    /// Gets a value indicating whether the dialog can be closed with the Escape (<c>Esc</c>) key.
    /// </summary>
    public bool CanCloseWithEscape { get; init; }

    /// <summary>
    /// Gets a value indicating whether a single-player session will pause whilst the dialog is
    /// being displayed.
    /// </summary>
    /// <remarks>
    /// <note type="warning">
    /// A value of <see langword="true"/> is not compatible with
    /// <see cref="DialogAfterAction.None"/>, and although accepted for the object model, the
    /// combination is not allowed for serialization both by the library and by Minecraft.
    /// </note>
    /// </remarks>
    /// <value>
    /// <see langword="true"/> if a single-player session would pause; otherwise,
    /// <see langword="false"/>. The default value, if omitted in serialized form, is
    /// <see langword="true"/>.
    /// </value>
    public bool Pause { get; init; }

    /// <summary>
    /// Gets the action to perform when the close action has been executed.
    /// </summary>
    /// <remarks>
    /// <note type="warning">
    /// A value of <see cref="DialogAfterAction.None"/> is not compatible with <see cref="Pause"/>
    /// being set to <see langword="true"/>, and although accepted for the object model, the
    /// combination is not allowed for serialization both by the library and by Minecraft.
    /// </note>
    /// </remarks>
    public DialogAfterAction AfterAction { get; init; }
}
