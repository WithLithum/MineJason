// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Serialization.TextJson;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Text.Json.Serialization;

namespace MineJason.Text.Colors;

/// <summary>
/// Encapsulates a color value for use with text components. This class cannot be inherited.
/// </summary>
[JsonConverter(typeof(RgbTextColorConverter))]
public sealed class RgbTextColor : ITextColor, IRGBLike, IEquatable<RgbTextColor>, IEquatable<Color>
{
    /// <summary>
    /// Initializes a new instance of <see cref="RgbTextColor"/> with the specified Red, Green and
    /// Blue components.
    /// </summary>
    /// <param name="r">The red component.</param>
    /// <param name="g">The green component.</param>
    /// <param name="b">The blue component.</param>
    public RgbTextColor(byte r, byte g, byte b) : this(Color.FromArgb(r, g, b))
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="RgbTextColor"/> with the specified color value.
    /// </summary>
    /// <param name="color">The color value.</param>
    public RgbTextColor(Color color)
    {
        Color = color;
    }

    /// <summary>
    /// Gets the color value of this instance.
    /// </summary>
    public Color Color { get; }

    /// <inheritdoc />
    public bool Equals(RgbTextColor? other)
    {
        return other != null && Color.Equals(other.Color);
    }

    /// <summary>
    /// Determines whether the value of this instance is equivalent to the specified color value.
    /// </summary>
    /// <param name="other">The color value to compare to.</param>
    /// <returns><see langword="true"/> if the two values are equivalent; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Color other)
    {
        return Color == other;
    }

    /// <inheritdoc />
    public string GenerateColorText()
    {
        return $"#{AsTriplet():x6}";
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as RgbTextColor);
    }

    /// <summary>
    /// Gets the hash code of this instance.
    /// </summary>
    /// <returns>The hash code of this instance.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Color.R, Color.G, Color.B);
    }

    /// <inheritdoc />
    public int AsTriplet()
    {
        return (Color.R << 16) + (Color.G << 8) + Color.B;
    }

    /// <summary>
    /// Creates a new instance of <see cref="RgbTextColor"/> from the specified RGB value.
    /// </summary>
    /// <param name="value">The RGB value. Must not exceed <c>0xFFFFFF</c>.</param>
    /// <returns>The resulting color.</returns>
    public static RgbTextColor FromRgb(int value)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 0xFFFFFF);
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 0x0);

        // Decode RGB
        var r = value >> 16;
        var g = (value >> 8) & 0xff;
        var b = value & 0xff;

        return new RgbTextColor(Color.FromArgb(r, g, b));
    }

    /// <summary>
    /// Converts the string containing hash symbol prefixed RGB triplet color value to a new
    /// instance of <see cref="RgbTextColor"/>. A return value indicates whether the conversion
    /// succeeded.
    /// </summary>
    /// <param name="text">The string to parse.</param>
    /// <param name="result">The conversion result value.</param>
    /// <returns><see langword="true"/> if the conversion succeeded; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string text, [NotNullWhen(true)] out RgbTextColor? result)
    {
        if (text is not ['#', _, _, _, _, _, _])
        {
            result = null;
            return false;
        }

        var span = text.AsSpan();

        var triplet = span[1..];
        if (!int.TryParse(triplet, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var rgb))
        {
            result = null;
            return false;
        }

        // Decode RGB
        result = FromRgb(rgb);
        return true;
    }
}
