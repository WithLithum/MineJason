// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Extensions.Drawing;

using System.Drawing;
using System.Globalization;

/// <summary>
/// Represents a chat color that is stored as <see cref="System.Drawing.Color"/>.
/// </summary>
public class DrawingChatColor : IChatColor, IEquatable<DrawingChatColor>, IEquatable<Color>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawingChatColor"/> class.
    /// </summary>
    /// <param name="color">The color.</param>
    public DrawingChatColor(Color color)
    {
        Color = color;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawingChatColor"/> class.
    /// </summary>
    /// <param name="r">The Red component of the color.</param>
    /// <param name="g">The Green component of the color.</param>
    /// <param name="b">The Blue component of the color.</param>
    public DrawingChatColor(byte r, byte g, byte b)
    {
        Color = Color.FromArgb(255, r, g, b);
    }
    
    /// <summary>
    /// Gets the color of this instance.
    /// </summary>
    public Color Color { get; }

    public byte R => Color.R;
    public byte G => Color.G;
    public byte B => Color.B;
    
    public string GenerateColorText()
    {
        return $"#{Color.R:x2}{Color.G:x2}{Color.B:x2}";
    }

    public static bool TryParse(string from, out DrawingChatColor? result)
    {
        result = null;
        
        if (from is not ['#', _, _, _, _, _, _])
        {
            return false;
        }

        var rStr = from[1..3];
        var gStr = from[3..5];
        var bStr = from[5..];
        
        #if DEBUG

        Console.WriteLine("{0}, {1}, {2}", rStr, gStr, bStr);
        
        #endif

        if (!byte.TryParse(rStr, NumberStyles.HexNumber, null, out var r)
            || !byte.TryParse(gStr, NumberStyles.HexNumber, null, out var g)
            || !byte.TryParse(bStr, NumberStyles.HexNumber, null, out var b))
        {
            return false;
        }

        result = new DrawingChatColor(r, g, b);
        return true;
    }

    public bool Equals(DrawingChatColor? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Color.Equals(other.Color);
    }
    
    /// <summary>
    /// Determines whether the specified color is equal to the color of this instance.
    /// </summary>
    /// <param name="other">The color to test.</param>
    /// <returns><see langword="true"/> if the specified color is equal to the color of this instance; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Color other)
    {
        return other.Equals(Color);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as DrawingChatColor);
    }

    public override int GetHashCode()
    {
        return Color.GetHashCode();
    }

    public static implicit operator DrawingChatColor(Color color)
    {
        return new DrawingChatColor(color);
    }

    public override string ToString()
    {
        return $"DrawingChatColor: {Color}";
    }
}