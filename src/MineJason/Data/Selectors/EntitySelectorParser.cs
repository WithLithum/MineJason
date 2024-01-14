// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Selectors;

using System.Globalization;
using System.Text;

/// <summary>
/// Provides parsing service for entity target selectors.
/// </summary>
public static class EntitySelectorParser
{
    private const char LeftBrace = '{';
    private const char RightBrace = '}';
    private const char Comma = ',';
    private const char EqualSign = '=';
    
    private static readonly IReadOnlyDictionary<string, EntitySelectorSortMode> sortModes =
        new Dictionary<string, EntitySelectorSortMode>
        {
            { "arbitrary", EntitySelectorSortMode.Arbitrary },
            { "random", EntitySelectorSortMode.Random },
            { "furthest", EntitySelectorSortMode.Furthest },
            { "nearest", EntitySelectorSortMode.Nearest }
        };

    public static IEnumerable<string> ParsePairSet(string from)
    {
        var inBrace = false;

        var list = new List<string>();
        var builder = new StringBuilder();

        if (from.StartsWith(',') || from.EndsWith(','))
        {
            throw new FormatException("Pair set invalid.");
        }
        
        foreach (var ch in from)
        {
            switch (ch)
            {
                case LeftBrace when inBrace:
                    throw new FormatException("No double bracing!");
                case LeftBrace:
                    inBrace = true;
                    break;
                case RightBrace when !inBrace:
                    throw new FormatException("Not even bracing yet!");
                case RightBrace:
                    inBrace = false;
                    break;
                case Comma when !inBrace:
                    list.Add(builder.ToString());
                    builder.Clear();
                    continue;
            }
            
            builder.Append(ch);
        }

        list.Add(builder.ToString());
        
        return list;
    }
    
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
    public static void ParseNameValue(string value, NameMatch match)
    {
        Console.WriteLine("ParseNameValue: {0}", value);
        
        if (!value.StartsWith('!'))
        {
            if (match.Include != null)
            {
                throw new FormatException("Duplicate teams notations are not allowed.");
            }

            match.Include = value;
        }
        else
        {
            match.Exclude.Add(value[1..]);
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
        #if DEBUG
        Console.WriteLine(value);
        #endif
        
        if (!value.StartsWith('{') || !value.EndsWith('}'))
        {
            throw new FormatException("Invalid scores value.");
        }

        var realValue = value[1..^1];

        foreach (var set in realValue.Split(','))
        {
            ParsePair(set, out var name, out var range);

            var realRange = ParseScoresRange(name, range);
            collection.Add(realRange);
        }
    }

    public static IntegralRange ParseIntegralRange(string value)
    {
        if (value.StartsWith("..", StringComparison.Ordinal))
        {
            var maxValue = int.Parse(value[2..], CultureInfo.InvariantCulture);

            return new IntegralRange(null, maxValue);
        }
        else if (value.EndsWith("..", StringComparison.Ordinal))
        {
            var minValue = int.Parse(value[..^2], CultureInfo.InvariantCulture);

            return new IntegralRange(minValue, null);
        }
        else if (value.Contains(".."))
        {
            var split = value.Split("..");
            if (split.Length != 2)
            {
                throw new FormatException("Invalid range format");
            }
            
            var minValue = int.Parse(split[0], CultureInfo.InvariantCulture);
            var maxValue = int.Parse(split[1], CultureInfo.InvariantCulture);

            return new IntegralRange(minValue, maxValue);
        }
        else
        {
            return new IntegralRange(int.Parse(value, CultureInfo.InvariantCulture));
        }
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
        if (value.StartsWith("..", StringComparison.Ordinal))
        {
            var maxValue = int.Parse(value[2..], CultureInfo.InvariantCulture);
            
            return new ScoreboardRangeMatch()
            {
                Objective = objective,
                Range = new IntegralRange(null, maxValue)
            };
        }
        else if (value.EndsWith("..", StringComparison.Ordinal))
        {
            var minValue = int.Parse(value[..^2], CultureInfo.InvariantCulture);
            
            return new ScoreboardRangeMatch()
            {
                Objective = objective,
                Range = new IntegralRange(minValue, null)
            };
        }
        else if (value.Contains(".."))
        {
            var split = value.Split("..");
            if (split.Length != 2)
            {
                throw new FormatException("Invalid range format");
            }
            
            var minValue = int.Parse(split[0], CultureInfo.InvariantCulture);
            var maxValue = int.Parse(split[1], CultureInfo.InvariantCulture);
            
            return new ScoreboardRangeMatch()
            {
                Objective = objective,
                Range = new IntegralRange(minValue, maxValue)
            };
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
    /// <exception cref="FormatException"></exception>
    public static void ParsePair(string input, out string name, out string value)
    {
        var inBrace = false;
        var isValue = false;
        var builder = new StringBuilder();
        name = "=";
        
        foreach (var x in input)
        {
            if (x == EqualSign && !isValue)
            {
                isValue = true;
                name = builder.ToString();
                builder.Clear();
                continue;
            }

            if (isValue && x == LeftBrace)
            {
                inBrace = true;
            }

            if (x == RightBrace)
            {
                if (!inBrace)
                {
                    throw new FormatException("Was not even in a brace!");
                }

                inBrace = false;
            }

            if (!inBrace && isValue && x == EqualSign)
            {
                throw new FormatException("Not a pair, but more than a pair!!!");
            }

            builder.Append(x);
        }

        if (!isValue || name == "=")
        {
            throw new FormatException("Not a pair but merely a value!");
        }

        if (inBrace)
        {
            throw new FormatException("Brace never ends!");
        }

        value = builder.ToString();
    }

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