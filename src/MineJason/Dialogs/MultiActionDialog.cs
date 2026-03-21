// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs;

using System.Diagnostics.CodeAnalysis;
using MineJason.Utilities;

/// <summary>
/// Defines a data-driven GUI screen showing multiple buttons arranged in columns.
/// </summary>
public sealed record MultiActionDialog : Dialog
{
    /// <summary>
    /// The identifier of the dialog type.
    /// </summary>
    public static readonly ResourceLocation Id = new("minecraft", "multi_action");

    private int? _columns;

    /// <summary>
    /// Initializes a new instance of <see cref="MultiActionDialog"/> class.
    /// </summary>
    public MultiActionDialog()
    {
    }

    [SetsRequiredMembers]
    internal MultiActionDialog(DialogCreationInfo creationInfo,
        IReadOnlyList<DialogButton> actions) : base(creationInfo)
    {
        Actions = actions;
    }

    /// <summary>
    /// Gets the list of actions.
    /// </summary>
    public required IReadOnlyList<DialogButton> Actions { get; init; }

    /// <summary>
    /// Gets the amount of columns to arrange buttons to.
    /// </summary>
    /// <value>
    /// The amount of columns, must be positive. Defaults to <c>2</c> if omitted.
    /// </value>
    public int? Columns
    {
        get => _columns;
        set
        {
            ThrowHelper.ThrowIfNegative(value, nameof(value));

            _columns = value;
        }
    }

    /// <summary>
    /// Gets the definition for the button displayed at the bottom of the screen.
    /// </summary>
    /// <value>
    /// The button at the bottom. If <see langword="null"/>, the button is not shown.
    /// </value>
    public DialogButton? ExitAction { get; init; }
}
