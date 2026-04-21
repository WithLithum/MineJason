// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Collections.ObjectModel;
using System.Globalization;
using MineJason.Data;
using MineJason.Extras.Selectors.Matching;

namespace MineJason.Extras.Selectors;

/// <summary>
/// Provides parsing service for entity target selectors.
/// </summary>
public static class EntitySelectorParser
{
    private static readonly ReadOnlyDictionary<string, EntitySelectorSortMode> sortModes =
        new Dictionary<string, EntitySelectorSortMode>
        {
            { "arbitrary", EntitySelectorSortMode.Arbitrary },
            { "random", EntitySelectorSortMode.Random },
            { "furthest", EntitySelectorSortMode.Furthest },
            { "nearest", EntitySelectorSortMode.Nearest }
        }.AsReadOnly();

    /// <summary>
    /// Walks the specified set of entity selector arguments and returns an enumerable containing
    /// each value.
    /// </summary>
    /// <param name="from">The string to parse.</param>
    /// <returns>An enumerable containing each value.</returns>
    public static IEnumerable<string> ParsePairSet(string from)
    {
        // TODO make this 'yield return'
        var resolver = new EntitySelectorPairSetResolver(from);
        resolver.RunToEnd();
        return resolver.Pairs;
    }

    /// <summary>
    /// Converts the specified string representation of <see cref="EntitySelectorSortMode"/> to
    /// its value equivalent.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>The parsed <see cref="EntitySelectorSortMode"/>.</returns>
    /// <exception cref="FormatException">The specified value is invalid.</exception>
    public static EntitySelectorSortMode ParseSortMode(string value)
    {
        if (!sortModes.TryGetValue(value, out var mode))
        {
            throw new FormatException("Specified sort mode is invalid");
        }

        return mode;
    }

    /// <summary>
    /// Resolves a tag argument value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="collection">The collection to add to.</param>
    public static void ParseTagValue(string value, TagSelectorCollection collection)
    {
        collection.Add(value.StartsWith('!')
            ? new TagSelector(value[1..], false)
            : new TagSelector(value, true));
    }

    /// <summary>
    /// Resolves a name argument value.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="match">The collection to add to.</param>
    public static void ParseNameValue(string value, EntityNameMatchCollection match)
    {
        if (value.StartsWith('!'))
        {
            match.Add(new EntityNameMatch(value[1..], false));
        }
        else
        {
            match.Add(new EntityNameMatch(value, true));
        }
    }

    /// <summary>
    /// Resolves a scoreboard selector value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="collection">The collection to put results into.</param>
    /// <exception cref="FormatException">Incorrect argument format.</exception>
    public static void ParseScoresValue(string value, ScoreboardRangeCollection collection)
    {
        if (!value.StartsWith('{') || !value.EndsWith('}'))
        {
            throw new FormatException("Invalid scores value.");
        }

        var realValue = value.AsSpan()[1..^1];

        foreach (var setRange in realValue.Split(','))
        {
            var set = realValue[setRange];
            ParsePair(set, out var name, out var range);

            var realRange = ParseScoresRange(name.ToString(), range);
            collection.Add(realRange);
        }
    }

    /// <summary>
    /// Converts the specified string representation of a range between two intergal numbers to its
    /// <see cref="IntegralRange"/> equivalent.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>
    /// An instance of <see cref="IntegralRange"/> equivalent to the number range contained in
    /// <paramref name="value"/>.
    /// </returns>
    /// <exception cref="FormatException">
    /// <paramref name="value"/> is not in the correct format.
    /// </exception>
    [Obsolete("Use IntegralRange.Parse instead.")]
    public static IntegralRange ParseIntegralRange(string value)
    {
        return IntegralRange.Parse(value);
    }

    /// <summary>
    /// Parses a scoreboard value range.
    /// </summary>
    /// <param name="objective">The objective.</param>
    /// <param name="value">The value.</param>
    /// <returns>The parsed value range.</returns>
    /// <exception cref="FormatException">Value range format incorrect.</exception>
    public static IScoreboardRange ParseScoresRange(string objective, string value)
    {
        return ParseScoresRange(objective, value.AsSpan());
    }

    /// <summary>
    /// Parses a scoreboard value range.
    /// </summary>
    /// <param name="objective">The objective.</param>
    /// <param name="value">The value.</param>
    /// <returns>The parsed value range.</returns>
    /// <exception cref="FormatException">Value range format incorrect.</exception>
    public static IScoreboardRange ParseScoresRange(string objective,
        in ReadOnlySpan<char> value)
    {
        if (value.Contains("..", StringComparison.Ordinal))
        {
            if (!IntegralRange.TryParse(value, out var range))
            {
                throw new FormatException("Invalid range format");
            }

            return new ScoreboardRangeMatch(objective, range);
        }
        else
        {
            return new ScoreboardExactMatch(objective, int.Parse(value, CultureInfo.InvariantCulture));
        }
    }

    /// <summary>
    /// Resolves a selector argument pair. 
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="name">The name.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="FormatException">The argument pair is not in a correct format.</exception>
    public static void ParsePair(string input, out string name, out string value)
    {
        ParsePair(input.AsSpan(),
            out var nameSpan,
            out var valueSpan);

        name = nameSpan.ToString();
        value = valueSpan.ToString();
    }

    /// <summary>
    /// Resolves a selector argument pair. 
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="name">The name.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="FormatException">The argument pair is not in a correct format.</exception>
    public static void ParsePair(in ReadOnlySpan<char> input,
        out ReadOnlySpan<char> name,
        out ReadOnlySpan<char> value)
    {
        var firstEqualSign = input.IndexOf('=');
        if (firstEqualSign == -1)
        {
            throw new FormatException("The argument pair is invalid.");
        }

        name = input[..firstEqualSign];
        value = input[(firstEqualSign + 1)..];
    }

    /// <summary>
    /// Resolves a team name.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="selector">The selector.</param>
    /// <exception cref="FormatException">Duplicate team notations.</exception>
    public static void ParseTeamsValue(string value, TeamSelector selector)
    {
        if (!value.StartsWith('!'))
        {
            if (selector.Include != null)
            {
                throw new FormatException("Duplicate teams notations are not allowed.");
            }

            selector.Include = value;
        }
        else
        {
            selector.Exclude.Add(value[1..]);
        }
    }
}