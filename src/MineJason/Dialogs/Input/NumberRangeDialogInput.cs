// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs.Input;

using System.Diagnostics.CodeAnalysis;
using MineJason.Text;
using MineJason.Utilities;

/// <summary>
/// Represents a numeric slider input control.
/// </summary>
public sealed record NumberRangeDialogInput : DialogInput
{
    /// <summary>
    /// The identifier of the input control type.
    /// </summary>
    public static readonly ResourceLocation Id = new("minecraft", "number_range");

    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    private int? _width;

    /// <summary>
    /// Initializes a new instance of the <see cref="NumberRangeDialogInput"/> class.
    /// </summary>
    public NumberRangeDialogInput()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="NumberRangeDialogInput"/> with the specified
    /// options.
    /// </summary>
    /// <param name="key">The key of the value submitted.</param>
    /// <param name="label">The label to display alongside the control.</param>
    /// <param name="start">The minimum value of the slider.</param>
    /// <param name="end">The maximum value of the slider.</param>
    /// <param name="labelFormat">A translation key to the format of the label to show.</param>
    /// <param name="step">The step size. If <see langword="null"/>, any value is accepted.</param>
    /// <param name="initial">
    /// The initial value. If <see langword="null"/>, defaults to the middle of the range.
    /// </param>
    /// <param name="width">The width of the control. If <c>0</c>, uses the default width.</param>
    [SetsRequiredMembers]
    public NumberRangeDialogInput(string key,
        TextComponent label,
        float start,
        float end,
        string? labelFormat = null,
        float? step = null,
        int? initial = null,
        int width = 0)
    {
        Key = key;
        Label = label;
        Start = start;
        End = end;

        LabelFormat = labelFormat;
        Step = step;
        Initial = initial;

        Width = width;
    }

    /// <summary>
    /// Gets the translation key used for displaying the label, with the first argument being the
    /// original label and the second argument for the value.
    /// </summary>
    /// <value>
    /// The translation key used for displaying the label. If <see langword="null"/>, the game uses
    /// the default value of <c>options.generic_value</c>.
    /// </value>
    public string? LabelFormat { get; init; }

    /// <summary>
    /// Gets the width of this input control.
    /// </summary>
    /// <value>
    /// The width of the control, ranging from <c>1</c> to <c>1024</c> (inclusive). If the value is
    /// <see langword="null"/>, the game will use <c>200</c> as the default value.
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
    /// Gets the minimum value of the slider.
    /// </summary>
    public required float Start { get; init; }

    /// <summary>
    /// Gets the maximum value of the slider.
    /// </summary>
    public required float End { get; init; }

    /// <summary>
    /// Gets the step size of the slider.
    /// </summary>
    /// <value>
    /// The step size of the slider. If <see langword="null"/>, any value is accepted.
    /// </value>
    public float? Step { get; init; }

    /// <summary>
    /// Gets the initial value of the slider.
    /// </summary>
    /// <value>
    /// The initial value of the slider. If <see langword="null"/>, defaults to the middle of the
    /// range.
    /// </value>
    public int? Initial { get; init; }
}
