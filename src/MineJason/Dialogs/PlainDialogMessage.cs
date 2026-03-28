// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs;

using System.Diagnostics.CodeAnalysis;
using MineJason.Text;
using MineJason.Utilities;

/// <summary>
/// Represents a dialog message that displays a text label.
/// </summary>
[SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
public sealed record PlainDialogMessage : DialogMessage
{
    /// <summary>
    /// The identifier of this message type.
    /// </summary>
    public static readonly ResourceLocation Type = new("minecraft", "plain_message");

    private int? _width;

    /// <summary>
    /// Gets the contents of this instance.
    /// </summary>
    public required TextComponent Contents { get; init; }

    /// <summary>
    /// Gets the width of this message.
    /// </summary>
    /// <value>
    /// The width of the label, ranging from <c>1</c> to <c>1024</c> (inclusive). Defaults to
    /// <c>200</c>.
    /// </value>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The value is outside the allowed range of <c>1</c> to <c>1024</c> (inclusive).
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
}
