// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Components;

using JetBrains.Annotations;

/// <summary>
/// Provides static methods to work with <see cref="IChatColor"/>.
/// </summary>
public static class ChatColor
{
    private static readonly Dictionary<char, IChatColor> FormattingColorTable = new()
    {
        { '0', KnownColor.Black },
        { '1', KnownColor.DarkBlue },
        { '2', KnownColor.DarkGreen },
        { '3', KnownColor.DarkAqua },
        { '4', KnownColor.DarkRed },
        { '5', KnownColor.DarkPurple },
        { '6', KnownColor.Gold },
        { '7', KnownColor.Gray },
        { '8', KnownColor.DarkGray },
        { '9', KnownColor.Blue },
        { 'a', KnownColor.Green },
        { 'b', KnownColor.Aqua },
        { 'c', KnownColor.Red },
        { 'd', KnownColor.LightPurple },
        { 'e', KnownColor.Yellow },
        { 'f', KnownColor.White }
    };
    
    /// <summary>
    /// Converts the specified legacy colour code character to its <see cref="KnownColor"/> equivalent.
    /// The colour code must be known to the Java Edition. Bedrock-specific codes are not supported.
    /// </summary>
    /// <param name="code">The code character (the character after the section symbol).</param>
    /// <returns>The corresponding <see cref="KnownColor"/> result.</returns>
    /// <exception cref="ArgumentException">The <paramref name="code"/> does not exist.</exception>
    [PublicAPI]
    public static IChatColor FromColorCode(char code)
    {
        if (!FormattingColorTable.TryGetValue(code, out var result))
        {
            throw new ArgumentException("Unknown formatting code.", nameof(code));
        }

        return result;
    }
}