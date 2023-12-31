using System.Collections.ObjectModel;

namespace MineJason;

using JetBrains.Annotations;

[PublicAPI]
public sealed class KnownColor : IChatColor
{
    private readonly string _colorName;

    private static readonly IReadOnlySet<string> KnownColorNames = new HashSet<string>
    {
        "black",
        "dark_blue",
        "dark_green",
        "dark_aqua",
        "dark_red",
        "dark_purple",
        "gold",
        "gray",
        "dark_gray",
        "blue",
        "green",
        "aqua",
        "red",
        "light_purple",
        "yellow",
        "white"
    };

    internal KnownColor(string colorName)
    {
        if (!KnownColorNames.Contains(colorName))
        {
            throw new ArgumentException($"Invalid color name {colorName}", nameof(colorName));
        }

        _colorName = colorName;
    }

    public string GenerateColorText()
    {
        return _colorName;
    }

    public static readonly KnownColor Black = new("black");
    public static readonly KnownColor DarkBlue = new("dark_blue");
    public static readonly KnownColor DarkGreen = new("dark_green");
    public static readonly KnownColor DarkAqua = new("dark_aqua");
    public static readonly KnownColor DarkRed = new("dark_red");
    public static readonly KnownColor DarkPurple = new("dark_purple");
    public static readonly KnownColor Gold = new("gold");
    public static readonly KnownColor Gray = new("gray");
    public static readonly KnownColor DarkGray = new("dark_gray");
    public static readonly KnownColor Blue = new("blue");
    public static readonly KnownColor Green = new("green");
    public static readonly KnownColor Aqua = new("aqua");
    public static readonly KnownColor Red = new("red");
    public static readonly KnownColor LightPurple = new("light_purple");
    public static readonly KnownColor Yellow = new("yellow");
    public static readonly KnownColor White = new("white");
}
