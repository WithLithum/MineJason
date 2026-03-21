// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs;

using System.Diagnostics.CodeAnalysis;
using MineJason.Utilities;

/// <summary>
/// Defines a data-driven GUI screen that shows server links.
/// </summary>
[SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
public sealed record ServerLinksDialog : Dialog
{
    /// <summary>
    /// The identifier of the dialog type.
    /// </summary>
    public static readonly ResourceLocation Id = new("minecraft", "server_links");

    private int? _columns;
    private int? _buttonWidth;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServerLinksDialog"/> class.
    /// </summary>
    public ServerLinksDialog()
    {
    }

    [SetsRequiredMembers]
    internal ServerLinksDialog(DialogCreationInfo creationInfo) : base(creationInfo)
    {
    }

    /// <summary>
    /// Gets the definition for the button on the footer, which trigger for an attempt to leave the
    /// dialog.
    /// </summary>
    public DialogButton? ExitAction { get; init; }

    /// <summary>
    /// Gets the amount of columns to arrange buttons to.
    /// </summary>
    /// <value>
    /// The amount of columns, must be positive. Defaults to <c>2</c>.
    /// </value>
    public int? Columns
    {
        get => _columns;
        init
        {
            ThrowHelper.ThrowIfNegativeOrZero(value, nameof(value));
            ThrowHelper.ThrowIfGreaterThan(value, 1024, nameof(value));

            _columns = value;
        }
    }

    /// <summary>
    /// Gets the width of the button.
    /// </summary>
    /// <value>
    /// The width of the button, ranging from <c>1</c> to <c>1024</c> (inclusive). Defaults to
    /// <c>150</c>.
    /// </value>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The value is outside the allowed range of <c>1</c> to <c>1024</c> (inclusive).
    /// </exception>
    public int? ButtonWidth
    {
        get => _buttonWidth;
        init
        {
            ThrowHelper.ThrowIfNegativeOrZero(value, nameof(value));
            ThrowHelper.ThrowIfGreaterThan(value, 1024, nameof(value));

            _buttonWidth = value;
        }
    }
}
