namespace MineJason.Colors;

using System.Globalization;
using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

// ReSharper disable MemberCanBePrivate.Global

/// <summary>
/// Represents a chat color format in RGB.
/// </summary>
[JsonConverter(typeof(RgbColorConverter))]
public sealed class RgbChatColor(byte r, byte g, byte b) : IChatColor, IEquatable<RgbChatColor>
{
    /// <summary>
    /// Gets the Red component of this instance.
    /// </summary>
    public byte R { get; } = r;

    /// <summary>
    /// Gets the Green component of this instance.
    /// </summary>
    public byte G { get; } = g;

    /// <summary>
    /// Gets the Blue component of this instance.
    /// </summary>
    public byte B { get; } = b;

    /// <inheritdoc />
    public string GenerateColorText()
    {
        return $"#{R:x2}{G:x2}{B:x2}";
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return GenerateColorText();
    }

    /// <summary>
    /// Attempts to parse the specified string to its equivalent instance representation.
    /// </summary>
    /// <param name="from">The string to parse.</param>
    /// <param name="result">The result to parse.</param>
    /// <returns><see langword="true"/> if the parse is successful; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string from, out RgbChatColor? result)
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

        if (!byte.TryParse(rStr, NumberStyles.HexNumber, null, out var red)
            || !byte.TryParse(gStr, NumberStyles.HexNumber, null, out var green)
            || !byte.TryParse(bStr, NumberStyles.HexNumber, null, out var blue))
        {
            return false;
        }

        result = new RgbChatColor(red, green, blue);
        return true;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(R, G, B);
    }

    /// <inheritdoc />
    public bool Equals(RgbChatColor? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        #if DEBUG
        
        Console.WriteLine("RgbColor: color equals check");
        Console.WriteLine("RgbColor: this: {0}, {1}, {2}", R, G, B);
        Console.WriteLine("RgbColor: that: {0}, {1}, {2}", other.R, other.G, other.B);

        
        #endif

        return R == other.R && G == other.G && B == other.B;
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
#if DEBUG

        Console.WriteLine("ChatColor: equals");
        Console.WriteLine(obj is RgbChatColor);
        
#endif
        
        return ReferenceEquals(this, obj) || obj is RgbChatColor other && Equals(other);
    }
}