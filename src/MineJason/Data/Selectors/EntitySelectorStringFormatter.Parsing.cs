// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Globalization;
using MineJason.Data.Nbt;
using MineJason.Data.Selectors.Advancements;
using MineJason.Data.Selectors.Predicates;
using MineJason.Data.Selectors.Utilities;
using MineJason.Exceptions;
using MineJason.Utilities;

namespace MineJason.Data.Selectors;

public static partial class EntitySelectorStringFormatter
{
    /// <summary>
    /// Converts the specified string representation of an entity selector to its object
    /// representation.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>
    /// An instance of <see cref="EntitySelector"/> equivalent to the entity selector sequence in
    /// <paramref name="value"/>.
    /// </returns>
    /// <exception cref="FormatException">
    /// <paramref name="value"/> is not in a correct format.
    /// </exception>
    public static EntitySelector ParseSelector(string value)
    {
        if (value.Length < 2)
        {
            throw new FormatException("Invalid selector.");
        }

        var kind = ParseSelectorKind(value[..2]);
        var selector = new EntitySelector(kind);

        if (value.Length > 2)
        {
            ParseSelectorArguments(value[2..], selector);
        }

        return selector;
    }

    private static void ParseSelectorArguments(string s, EntitySelector selector)
    {
        if (!s.StartsWith('[') || !s.EndsWith(']') || s.Length <= 5)
        {
            throw new FormatException("Invalid value");
        }

        DistanceRange? distanceRange = null;

        var pairs = EntitySelectorParser.ParsePairSet(s[1..^1]);
#if DEBUG
        Console.WriteLine(pairs);
#endif

        var coords = EvaluateXyz(pairs);

        foreach (var pair in pairs)
        {
            EntitySelectorParser.ParsePair(pair, out var key, out var value);

            switch (key)
            {
                case "gamemode":
                    if (!value.StartsWith('!'))
                    {
                        if (selector.GameMode.Include != null)
                        {
                            throw new FormatException("Already existing game mode include condition.");
                        }

                        selector.GameMode.Include = ParseGameMode(value);
                    }
                    else
                    {
                        selector.GameMode.Exclude.Add(ParseGameMode(value[1..]));
                    }

                    break;
                case "distance":
                    var distValue = ParseDistanceRange(value);
                    distanceRange = distValue;
                    break;
                case "tag":
                    EntitySelectorParser.ParseTagValue(value, selector.Tags);
                    break;
                case "scores":
                    EntitySelectorParser.ParseScoresValue(value, selector.Scores);
                    break;
                case "team":
                    EntitySelectorParser.ParseTeamsValue(value, selector.Team);
                    break;
                case "limit":
                    selector.Limit = int.Parse(value, CultureInfo.InvariantCulture);
                    break;
                case "sort":
                    selector.Sort = EntitySelectorParser.ParseSortMode(value);
                    break;
                case "level":
                    selector.Level = IntegralRange.Parse(value);
                    break;
                case "name":
                    EntitySelectorParser.ParseNameValue(value, selector.Name);
                    break;
                case "x_rotation":
                    selector.VerticalRotation = ParseDistanceRange(value);
                    break;
                case "y_rotation":
                    selector.HorizontalRotation = ParseDistanceRange(value);
                    break;
                case "type":
                    var exclude = false;
                    if (value.StartsWith('!'))
                    {
                        exclude = true;
                        value = value[1..];
                    }

                    if (!ResourceLocation.TryParse(value, out var resourceLocation))
                    {
                        throw new FormatException("Invalid resource location for type");
                    }

                    if (exclude)
                    {
                        selector.Type.Exclude.Add(resourceLocation);
                    }
                    else
                    {
                        if (selector.Type.Include.HasValue)
                        {
                            throw new FormatException("Multiple include type selector!");
                        }

                        selector.Type.Include = resourceLocation;
                    }

                    break;
                case "x":
                case "y":
                case "z":
                case "dx":
                case "dy":
                case "dz":
                    // Don't do anything here, just skip.
                    // We already handled them above.
                    break;
                case "nbt":
                    if (value.StartsWith('!'))
                    {
                        selector.Nbt.Exclude.Add(new RawNbtDataProvider(value[1..]));
                    }
                    else
                    {
                        selector.Nbt.Include.Add(new RawNbtDataProvider(value));
                    }

                    break;
                case "advancements":
                    ParseAdvancements(value, selector.Advancements);
                    break;
                case "predicate":
                    if (!PredicateCondition.TryParse(value, out var predicateCondition))
                    {
                        throw SelectorFormatException.InvalidPredicateCondition(value);
                    }

                    selector.Predicates.Add(predicateCondition);
                    break;
                default:
                    throw new FormatException($"Unrecognised argument name {key}");
            }
        }

        // Assemble all values!

        // position
        selector.Position = coords.Origin;
        selector.DiagonalRange = coords.Diagonal;

        // distance
        selector.Distance = distanceRange;
    }

    private static SelectorXyz EvaluateXyz(IEnumerable<string> pairs)
    {
        double? x = null, y = null, z = null, dx = null, dy = null, dz = null;

        void EvalOne(ReadOnlySpan<char> k,
            ReadOnlySpan<char> v,
            ReadOnlySpan<char> name, ref double? variable)
        {
            if (k.Equals(name, StringComparison.Ordinal))
            {
                variable = double.Parse(v, CultureInfo.InvariantCulture);
            }
        }

        foreach (var pair in pairs)
        {
            var pairBuf = pair.AsSpan();
            EntitySelectorParser.ParsePair(pairBuf,
                out var key,
                out var value);

            EvalOne(key, value, "x", ref x);
            EvalOne(key, value, "y", ref y);
            EvalOne(key, value, "z", ref z);
            EvalOne(key, value, "dx", ref dx);
            EvalOne(key, value, "dy", ref dy);
            EvalOne(key, value, "dz", ref dz);
        }

        return new SelectorXyz
        {
            Origin = x.HasValue && y.HasValue && z.HasValue
                ? new Vector3D(x.Value, y.Value, z.Value)
                : null,

            Diagonal = dx.HasValue && dy.HasValue && dz.HasValue
                ? new Vector3D(dx.Value, dy.Value, dz.Value)
                : null,
        };
    }

    /// <summary>
    /// Converts the specified string representation of <see cref="EntitySelectorKind"/> to its instance
    /// representation.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>The kind.</returns>
    /// <exception cref="FormatException">Unknown kind.</exception>
    public static EntitySelectorKind ParseSelectorKind(string value) =>
        value switch
        {
            "@a" => EntitySelectorKind.AllPlayers,
            "@e" => EntitySelectorKind.AllEntities,
            "@p" => EntitySelectorKind.NearestPlayer,
            "@r" => EntitySelectorKind.RandomPlayer,
            "@s" => EntitySelectorKind.Executor,
            "@n" => EntitySelectorKind.NearestEntity,
            _ => throw new FormatException($"Not a valid selector kind - '{value}'.")
        };

    /// <summary>
    /// Converts the specified string representation of <see cref="EntitySelectorSortMode"/> to its instance
    /// representation.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>The sort mode.</returns>
    /// <exception cref="FormatException">Unknown sort mode.</exception>
    public static EntitySelectorSortMode ParseSortMode(string value) =>
        value switch
        {
            "arbitrary" => EntitySelectorSortMode.Arbitrary,
            "random" => EntitySelectorSortMode.Random,
            "furthest" => EntitySelectorSortMode.Furthest,
            "nearest" => EntitySelectorSortMode.Nearest,
            _ => throw new FormatException($"Not a valid entity selector sort mode - '{value}'.")
        };

    /// <summary>
    /// Converts the specified string representation of <see cref="GameMode"/> to its instance
    /// representation.
    /// </summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>The game mode.</returns>
    /// <exception cref="FormatException">Unknown game mode.</exception>
    public static GameMode ParseGameMode(string value) =>
        value switch
        {
            "survival" => GameMode.Survival,
            "creative" => GameMode.Creative,
            "adventure" => GameMode.Adventure,
            "spectator" => GameMode.Spectator,
            _ => throw new FormatException($"Not a valid game mode - '{value}'.")
        };

    /// <summary>
    /// Converts the specified string representation of a number range to its
    /// <see cref="DistanceRange"/> equivalent.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>
    /// A <see cref="DistanceRange"/> equivalent to the number range contained in
    /// <paramref name="value"/>.
    /// </returns>
    public static DistanceRange ParseDistanceRange(string value)
    {
        DistanceRange range;
        var valueSpan = value.AsSpan();

        if (value.StartsWith("..", StringComparison.InvariantCultureIgnoreCase))
        {
            range = DistanceRange.MatchRange(null, double.Parse(valueSpan[2..], CultureInfo.InvariantCulture));
        }
        else if (value.EndsWith("..", StringComparison.InvariantCultureIgnoreCase))
        {
            range = DistanceRange.MatchRange(double.Parse(valueSpan[..^2], CultureInfo.InvariantCulture), null);
        }
        else if (value.Contains("..", StringComparison.InvariantCultureIgnoreCase))
        {
            Span<Range> valueRanges = stackalloc Range[3];
            if (valueSpan.Split(valueRanges, "..", StringSplitOptions.TrimEntries) != 2)
            {
                throw new FormatException("The specified distance range string is invalid.");
            }

            range = DistanceRange.MatchRange(double.Parse(valueSpan[valueRanges[0]],
                    CultureInfo.InvariantCulture),
                double.Parse(valueSpan[valueRanges[1]], CultureInfo.InvariantCulture));
        }
        else
        {
            range = DistanceRange.MatchExact(double.Parse(valueSpan, CultureInfo.InvariantCulture));
        }

        return range;
    }

    /// <summary>
    /// Interprets the specified string and converts it to the scoreboard ranges.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="collection">The collection to store results to.</param>
    /// <exception cref="FormatException">The value is invalid.</exception>
    public static void ParseScores(string value, ScoreboardRangeCollection collection)
    {
        var valueSpan = value.AsSpan()[1..^1];

        if (!value.StartsWith('{') || !value.EndsWith('}') || value.Length < 3)
        {
            throw new FormatException("not start or end with braces, or not a valid specifier");
        }

        var split = valueSpan.Split(',');
        var pairNumber = 0;

        Span<Range> actualPairRange = stackalloc Range[3];
        foreach (var pairRange in split)
        {
            var pair = valueSpan[pairRange];

            if (pair.Split(actualPairRange, '=') != 2)
            {
                // Invalid syntax: invalid pair
                throw new FormatException($"Pair index {pairNumber} is invalid.");
            }

            var objective = pair[actualPairRange[0]];
            var match = pair[actualPairRange[1]];
            IScoreboardRange range;

            if (match.Contains("..", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!IntegralRange.TryParse(match, out var scoreRange))
                {
                    throw new FormatException("The specified score selector is invalid.");
                }

                range = new ScoreboardRangeMatch(objective.ToString(), scoreRange);
            }
            else
            {
                range = new ScoreboardExactMatch(objective.ToString(),
                    int.Parse(match, CultureInfo.InvariantCulture));
            }

            collection.Add(range);
            pairNumber++;
        }
    }

    internal static IAdvancementCondition ParseCondition(ReadOnlySpan<char> value)
    {
        if (value.StartsWith('{'))
        {
            if (!value.EndsWith('}')
                || !CriterionAdvancementCondition.TryParse(value, out var result))
            {
                throw new FormatException("Invalid compound advancement condition!");
            }

            return result;
        }

        if (!SpecificValueUtil.TryParseLowerBoolean(value, out var boolean))
        {
            throw new FormatException("Invalid boolean advancement condition!");
        }

        return new BooleanAdvancementCondition(boolean);
    }

    /// <summary>
    /// Parses the string representation of an entity selector advancement match and stores parsed
    /// entries into the specified <paramref name="collection"/>.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="collection">The collection to put entries into..</param>
    /// <exception cref="SelectorFormatException">
    /// <paramref name="value"/> is in an invalid format.
    /// </exception>
    /// <exception cref="FormatException">
    /// A resource location in <paramref name="value"/> is in an invalid format.
    /// </exception>
    public static void ParseAdvancements(string value, SelectorAdvancementMatchCollection collection)
    {
        if (!value.StartsWith('{') || !value.EndsWith('}') || value.Length < 3)
        {
            throw new SelectorFormatException("Invalid advancement condition set format.", value);
        }

        var set = EntitySelectorParser.ParsePairSet(value[1..^1]);

        foreach (var pair in set)
        {
            EntitySelectorParser.ParsePair(pair.AsSpan(),
                out var name,
                out var conditionString);

            if (!ResourceLocation.TryParse(name, out var location))
            {
                throw new FormatException("Invalid advancement resource location!");
            }

            collection.Add(new SelectorAdvancementMatch(location,
                ParseCondition(conditionString)));
        }
    }
}