// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs.Input;

using MineJason.Utilities;

/// <summary>
/// Represents a dialog control that receives text input from user.
/// </summary>
public sealed record TextDialogInput : DialogInput
{
    /// <summary>
    /// The type identifier of the dialog input type.
    /// </summary>
    public static readonly ResourceLocation Id = new("minecraft", "text");

    /// <summary>
    /// Configures multi-line behaviour of the control.
    /// </summary>
    public record MultilineSettings
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private int? _height;

        /// <summary>
        /// Gets the maximum amount of lines that the input control can accept.
        /// </summary>
        public int? MaxLines { get; init; }

        /// <summary>
        /// Gets the height of the control.
        /// </summary>
        /// <value>
        /// The height of the control, ranging from <c>1</c> to <c>1024</c> (inclusive). If the value
        /// is <c>0</c>, the <c>height</c> field is omitted for serialization.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The value is outside the allowed range of <c>1</c> to <c>1024</c> (inclusive) and is not
        /// <c>0</c>.
        /// </exception>
        public int? Height
        {
            get => _height;
            init
            {
                ThrowHelper.ThrowIfNegativeOrZero(value, nameof(value));
                ThrowHelper.ThrowIfGreaterThan(value, 512, nameof(value));

                _height = Height;
            }
        }
    }

    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    private int? _width;

    /// <summary>
    /// Gets the width of this input control.
    /// </summary>
    /// <value>
    /// The width of the control, ranging from <c>1</c> to <c>1024</c> (inclusive). If the value is
    /// <see langword="null"/>, the <c>width</c> field is omitted for serialization.
    /// </value>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The value is outside the allowed range of <c>1</c> to <c>1024</c> (inclusive) and is not
    /// <c>0</c>.
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
    /// Gets a value indicating whether the label of the input control is visible. 
    /// </summary>
    public bool? LabelVisible { get; init; }

    /// <summary>
    /// Gets the initial value of the input control.
    /// </summary>
    public string? Initial { get; init; }

    /// <summary>
    /// Gets the maximum amount of characters that the input control accepts.
    /// </summary>
    public int? MaxLength { get; init; }

    /// <summary>
    /// Gets the multi-line behaviour settings of the input control, which its absence indicates
    /// that the control does not accept multi-line input.
    /// </summary>
    /// <value>
    /// The multi-line behaviour settings of the input control. If <see langword="null" />, the
    /// control rejects multi-line text.
    /// </value>
    public MultilineSettings? Multiline { get; init; }
}
