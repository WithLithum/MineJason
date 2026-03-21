// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Components;

using JetBrains.Annotations;
using MineJason.Text.Colors;

/// <summary>
/// Provides static methods to work with <see cref="IChatColor"/>.
/// </summary>
public static class ChatColor
{
    private static readonly Dictionary<char, IChatColor> FormattingColorTable = new()
    {
        { '0', NamedTextColor.Black },
        { '1', NamedTextColor.DarkBlue },
        { '2', NamedTextColor.DarkGreen },
        { '3', NamedTextColor.DarkAqua },
        { '4', NamedTextColor.DarkRed },
        { '5', NamedTextColor.DarkPurple },
        { '6', NamedTextColor.Gold },
        { '7', NamedTextColor.Gray },
        { '8', NamedTextColor.DarkGray },
        { '9', NamedTextColor.Blue },
        { 'a', NamedTextColor.Green },
        { 'b', NamedTextColor.Aqua },
        { 'c', NamedTextColor.Red },
        { 'd', NamedTextColor.LightPurple },
        { 'e', NamedTextColor.Yellow },
        { 'f', NamedTextColor.White }
    };
    
    /// <summary>
    /// Converts the specified legacy colour code character to its <see cref="NamedTextColor"/> equivalent.
    /// The colour code must be known to the Java Edition. Bedrock-specific codes are not supported.
    /// </summary>
    /// <param name="code">The code character (the character after the section symbol).</param>
    /// <returns>The corresponding <see cref="NamedTextColor"/> result.</returns>
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