namespace MineJason.Data.Selectors;

using System.Globalization;

/// <summary>
/// Provides parsing service for entity target selectors.
/// </summary>
public static class EntitySelectorParser
{
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

    public static void ParseScoresValue(string value, ScoreboardRangeCollection collection)
    {
        if (!value.StartsWith('{') || !value.StartsWith('}'))
        {
            throw new FormatException("Invalid scores value.");
        }

        var realValue = value[1..^2];

        foreach (var set in realValue.Split(','))
        {
            ParsePair(set, out var name, out var range);

            var realRange = ParseScoresRange(name, range);
            collection.Add(realRange);
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
        var split = input.Split('=');

        if (split.Length != 2)
        {
            throw new FormatException("The key pair format was invalid.");
        }

        name = split[0];
        value = split[1];
    }
}