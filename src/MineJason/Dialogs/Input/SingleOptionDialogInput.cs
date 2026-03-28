// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs.Input;

using System.Diagnostics.CodeAnalysis;
using MineJason.Text;
using MineJason.Utilities;

/// <summary>
/// Represents a dialog input control consisting of multiple single-select options ('radio box').
/// </summary>
public sealed record SingleOptionDialogInput : DialogInput
{
    /// <summary>
    /// The identifier of the input control type.
    /// </summary>
    public static readonly ResourceLocation Type = new("minecraft", "single_option");

    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    private int? _width;

    /// <summary>
    /// Initializes a new instance of the <see cref="SingleOptionDialogInput"/> class.
    /// </summary>
    public SingleOptionDialogInput()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="SingleOptionDialogInput"/> with the specified
    /// properties.
    /// </summary>
    /// <param name="key">The key used for submitting data.</param>
    /// <param name="label">The label to display alongside.</param>
    /// <param name="options">A list of available options.</param>
    /// <param name="labelVisible">If <see langword="true"/>, the label is displayed.</param>
    /// <param name="width">
    /// The width of the control. If <c>0</c>, uses the Minecraft default.
    /// </param>
    [SetsRequiredMembers]
    public SingleOptionDialogInput(string key,
        TextComponent label,
        IReadOnlyList<Item> options,
        bool labelVisible = true,
        int width = 0)
    {
        Key = key;
        Label = label;
        Options = options;
        LabelVisible = labelVisible;
        Width = width;
    }

    /// <summary>
    /// Represents an item in a single option dialog input control.
    /// </summary>
    public record Item
    {
        /// <summary>
        /// Gets or sets the value to be submitted for completing the dialog with this option
        /// selected.
        /// </summary>
        public required string Id { get; init; }

        /// <summary>
        /// Gets the text label to display for this item.
        /// </summary>
        public TextComponent? Display { get; init; }

        /// <summary>
        /// Gets a value indicating whether this option is the default.
        /// </summary>
        /// <remarks>
        /// <note type="warning">
        /// Although allowed for in-memory model, Minecraft does reject more than one item with
        /// this property set to <see langword="true"/>.
        /// </note>
        /// </remarks>
        public bool? Initial { get; init; }
    }

    /// <summary>
    /// Gets a value indicating whether the label of this control is displayed.
    /// </summary>
    public bool? LabelVisible { get; init; }

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
    /// Gets the list of available options.
    /// </summary>
    public required IReadOnlyList<Item> Options { get; init; }
}
