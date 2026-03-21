// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Text.Colors;

using System.Text.Json.Serialization;
using MineJason.Serialization.TextJson;

/// <summary>
/// Represents a named text colour.
/// </summary>
[JsonConverter(typeof(KnownColorConverter))]
public sealed class NamedTextColor : IChatColor
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

    internal NamedTextColor(string colorName)
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
    /// Attempts to create a new instance of <see cref="NamedTextColor"/> representing the specified
    /// known text color.
    /// </summary>
    /// <param name="colorName">The color name.</param>
    /// <returns>The text color, if exists; otherwise, <see langword="false"/>.</returns>
    public static NamedTextColor? CreateOrDefault(string colorName)
    {
        if (!KnownColorNames.Contains(colorName))
        {
            return null;
        }

        return new NamedTextColor(colorName);
    }

    /// <summary>
    /// Gets the <c>black</c> color.
    /// </summary>
    public static readonly NamedTextColor Black = new("black");

    /// <summary>
    /// Gets the <c>dark_blue</c> color.
    /// </summary>
    public static readonly NamedTextColor DarkBlue = new("dark_blue");

    /// <summary>
    /// Gets the <c>dark_green</c> color.
    /// </summary>
    public static readonly NamedTextColor DarkGreen = new("dark_green");

    /// <summary>
    /// Gets the <c>dark_aqua</c> color.
    /// </summary>
    public static readonly NamedTextColor DarkAqua = new("dark_aqua");

    /// <summary>
    /// Gets the <c>dark_red</c> color.
    /// </summary>
    public static readonly NamedTextColor DarkRed = new("dark_red");

    /// <summary>
    /// Gets the <c>dark_purple</c> color.
    /// </summary>
    public static readonly NamedTextColor DarkPurple = new("dark_purple");

    /// <summary>
    /// Gets the <c>gold</c> color.
    /// </summary>
    public static readonly NamedTextColor Gold = new("gold");

    /// <summary>
    /// Gets the <c>gray</c> color.
    /// </summary>
    public static readonly NamedTextColor Gray = new("gray");

    /// <summary>
    /// Gets the <c>dark_gray</c> color.
    /// </summary>
    public static readonly NamedTextColor DarkGray = new("dark_gray");

    /// <summary>
    /// Gets the <c>blue</c> color.
    /// </summary>
    public static readonly NamedTextColor Blue = new("blue");

    /// <summary>
    /// Gets the <c>green</c> color.
    /// </summary>
    public static readonly NamedTextColor Green = new("green");

    /// <summary>
    /// Gets the <c>aqua</c> color.
    /// </summary>
    public static readonly NamedTextColor Aqua = new("aqua");

    /// <summary>
    /// Gets the <c>red</c> color.
    /// </summary>
    public static readonly NamedTextColor Red = new("red");

    /// <summary>
    /// Gets the <c>light_purple</c> color.
    /// </summary>
    public static readonly NamedTextColor LightPurple = new("light_purple");

    /// <summary>
    /// Gets the <c>yellow</c> color.
    /// </summary>
    public static readonly NamedTextColor Yellow = new("yellow");

    /// <summary>
    /// Gets the <c>white</c> color.
    /// </summary>
    public static readonly NamedTextColor White = new("white");
}
