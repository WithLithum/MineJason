namespace MineJason.Data.Selectors;

using System.Collections.ObjectModel;
using System.Globalization;

/// <summary>
/// Provides parsing service for entity target selectors.
/// </summary>
public static class EntitySelectorParser
{
    private static readonly IReadOnlyDictionary<string, EntitySelectorSortMode> sortModes =
        new Dictionary<string, EntitySelectorSortMode>
        {
            { "arbitrary", EntitySelectorSortMode.Arbitrary },
            { "random", EntitySelectorSortMode.Random },
            { "furthest", EntitySelectorSortMode.Furthest },
            { "nearest", EntitySelectorSortMode.Nearest }
        };

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
    public static void ParseNameValue(string value, ref NameMatch match)
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
        var split = input.Split('=', StringSplitOptions.TrimEntries);

        if (split.Length != 2)
        {
            throw new FormatException("The key pair format was invalid.");
        }

        name = split[0];
        value = split[1];
    }

    public static void ParseTeamsValue(string value, ref TeamSelector selector)
    {
        if (!value.StartsWith('!'))
        {
            if (selector.Team != null)
            {
                throw new FormatException("Duplicate teams notations are not allowed.");
            }

            selector.Team = value;
        }
        else
        {
            selector.Exclude.Add(value[1..]);
        }
    }
}