// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs.Input;

/// <summary>
/// Represents a duo-state checkbox dialog input.
/// </summary>
public sealed record BooleanDialogInput : DialogInput
{
    /// <summary>
    /// The identifier of the dialog type.
    /// </summary>
    public static readonly ResourceLocation Id = new("minecraft", "boolean");

    /// <summary>
    /// Gets a value indicating whether the control is set to checked by default.
    /// </summary>
    public bool? Initial { get; init; }

    /// <summary>
    /// Gets the string to substitute the template in <see cref="DynamicCommandDialogAction"/> with
    /// if the control is checked when that action was executed.
    /// </summary>
    /// <value>
    /// The text to substitute. If <see langword="null"/>, the field is omitted and will default to
    /// <c>true</c> in-game.
    /// </value>
    public string? OnTrue { get; init; }

    /// <summary>
    /// Gets the string to substitute the template in <see cref="DynamicCommandDialogAction"/> with
    /// if the control is unchecked when that action was executed.
    /// </summary>
    /// /// <value>
    /// The text to substitute. If <see langword="null"/>, the field is omitted and will default to
    /// <c>false</c> in-game.
    /// </value>
    public string? OnFalse { get; init; }
}
