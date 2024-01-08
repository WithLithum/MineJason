// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason;

using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

/// <summary>
/// Represents a named text colour.
/// </summary>
[JsonConverter(typeof(KnownColorConverter))]
public sealed class KnownColor : IChatColor
{
    private readonly string _colorName;

    private static readonly HashSet<string> KnownColorNames =
    [
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
    ];

    internal KnownColor(string colorName)
    {
        if (!KnownColorNames.Contains(colorName))
        {
            throw new ArgumentException($"Invalid color name {colorName}", nameof(colorName));
        }

        _colorName = colorName;
    }

    /// <summary>
    /// Gets the known color name of this instance.
    /// </summary>
    /// <returns>The known color name.</returns>
    public string GenerateColorText()
    {
        return _colorName;
    }

    /// <summary>
    /// Gets the <c>black</c> color.
    /// </summary>
    public static readonly KnownColor Black = new("black");

    /// <summary>
    /// Gets the <c>dark_blue</c> color.
    /// </summary>
    public static readonly KnownColor DarkBlue = new("dark_blue");

    /// <summary>
    /// Gets the <c>dark_green</c> color.
    /// </summary>
    public static readonly KnownColor DarkGreen = new("dark_green");

    /// <summary>
    /// Gets the <c>dark_aqua</c> color.
    /// </summary>
    public static readonly KnownColor DarkAqua = new("dark_aqua");

    /// <summary>
    /// Gets the <c>dark_red</c> color.
    /// </summary>
    public static readonly KnownColor DarkRed = new("dark_red");

    /// <summary>
    /// Gets the <c>dark_purple</c> color.
    /// </summary>
    public static readonly KnownColor DarkPurple = new("dark_purple");

    /// <summary>
    /// Gets the <c>gold</c> color.
    /// </summary>
    public static readonly KnownColor Gold = new("gold");

    /// <summary>
    /// Gets the <c>gray</c> color.
    /// </summary>
    public static readonly KnownColor Gray = new("gray");

    /// <summary>
    /// Gets the <c>dark_gray</c> color.
    /// </summary>
    public static readonly KnownColor DarkGray = new("dark_gray");

    /// <summary>
    /// Gets the <c>blue</c> color.
    /// </summary>
    public static readonly KnownColor Blue = new("blue");

    /// <summary>
    /// Gets the <c>green</c> color.
    /// </summary>
    public static readonly KnownColor Green = new("green");

    /// <summary>
    /// Gets the <c>aqua</c> color.
    /// </summary>
    public static readonly KnownColor Aqua = new("aqua");

    /// <summary>
    /// Gets the <c>red</c> color.
    /// </summary>
    public static readonly KnownColor Red = new("red");

    /// <summary>
    /// Gets the <c>light_purple</c> color.
    /// </summary>
    public static readonly KnownColor LightPurple = new("light_purple");

    /// <summary>
    /// Gets the <c>yellow</c> color.
    /// </summary>
    public static readonly KnownColor Yellow = new("yellow");

    /// <summary>
    /// Gets the <c>white</c> color.
    /// </summary>
    public static readonly KnownColor White = new("white");
}
