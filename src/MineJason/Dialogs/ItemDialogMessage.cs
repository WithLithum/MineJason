// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Dialogs;

using JetBrains.Annotations;
using MineJason.Data;
using MineJason.Text;

/// <summary>
/// Represents a dialog control that shows an item.
/// </summary>
public sealed record ItemDialogMessage : DialogMessage
{
    /// <summary>
    /// The type identifier.
    /// </summary>
    public static readonly ResourceLocation Type = new("minecraft", "item");
    
    private int _width;
    private int _height;
    
    /// <summary>
    /// Configures the description text to display alongside the item.
    /// </summary>
    public record DescriptionLabel
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private int _width;

        /// <summary>
        /// Gets the text component to display.
        /// </summary>
        public required TextComponent Contents { get; init; }

        /// <summary>
        /// Gets the width of the text to display.
        /// </summary>
        /// <value>
        /// The width, must be between <c>1</c> and <c>1024</c>. The value of <c>0</c> causes the
        /// property to be omitted for serialization.
        /// </value>
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public int Width
        {
            get => _width;
            init
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(value));
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 1024, nameof(value));

                _width = value;
            }
        }
    }

    /// <summary>
    /// Gets the item stack to display.
    /// </summary>
    public required DisplayItemStack Item { get; init; }

    /// <summary>
    /// Gets the description label to display alongside the item.
    /// </summary>
    public DescriptionLabel? Description { get; init; }

    /// <summary>
    /// Gets a value indicating whether to show the count text and durability bar over the item
    /// icon.
    /// </summary>
    public bool ShowDecoration { get; init; }
    
    /// <summary>
    /// Gets a value indicating whether to show the item tooltip when the item icon is hovered by
    /// the mouse pointer.
    /// </summary>
    public bool ShowTooltip { get; init; }

    /// <summary>
    /// Gets the horizontal size of the element.
    /// </summary>
    /// <value>
    /// The horizontal size, between <c>1</c> and <c>256</c>. The value <c>0</c> causes the
    /// property to be omitted during serialization.
    /// </value>
    public int Width
    {
        get => _width;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(value));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 256, nameof(value));

            _width = value;
        }
    }

    /// <summary>
    /// Gets the vertical size of the element.
    /// </summary>
    /// <value>
    /// The vertical size, between <c>1</c> and <c>256</c>. The value <c>0</c> causes the property
    /// to be omitted during serialization.
    /// </value>
    public int Height
    {
        get => _height;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(value));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 256, nameof(value));

            _height = value;
        }
    }
}