// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Selectors;

using System.Globalization;
using MineJason.Data.Selectors.Advancements;
using MineJason.Exceptions;
using MineJason.Utilities;

public static partial class EntitySelectorStringFormatter
{
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
        #if DEBUG
        Console.WriteLine(s);        
        #endif
        
        if (!s.StartsWith('[') || !s.EndsWith(']') || s.Length <= 5)
        {
            throw new FormatException("Invalid value");
        }

        GameModeMatch? gameModeMatch = null;
        Vector3D? position = null;
        Vector3D? diagonal = null;
        DistanceRange? distanceRange = null;

        var pairs = EntitySelectorParser.ParsePairSet(s[1..^1]);
        #if DEBUG
        Console.WriteLine(pairs);        
        #endif

        if (TryGetXyzValue(pairs, out var resultPos))
        {
            position = resultPos;
        }
        
        // diagonal
        if (TryGetXyzValue(pairs, out var resultDPos, "dx", "dy", "dz"))
        {
            diagonal = resultDPos;
        }

        foreach (var pair in pairs)
        {
            EntitySelectorParser.ParsePair(pair, out var key, out var value);
            Console.WriteLine("ParseSelectorArguments: pair (key={0}, value={1})", key, value);
            
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
                    selector.Level = EntitySelectorParser.ParseIntegralRange(value);
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
                    // Don't do anything here, just skip.
                    // We already handled them above.
                    break;
                case "nbt":
                    if (value.StartsWith('!'))
                    {
                        selector.Nbt.Exclude.Add(new NbtProvider(value[1..]));
                    }
                    else
                    {
                        selector.Nbt.Include.Add(new NbtProvider(value));
                    }
                    break;
                case "advancements":
                    ParseAdvancements(value, selector.Advancements);
                    break;
                default:
                    throw new FormatException($"Unrecognised argument name {key}");
            }
        }
        
        // Assemble all values!
        
        // position
        selector.Position = position;
        
        // diagonal
        selector.DiagonalRange = diagonal;
        
        // distance
        selector.Distance = distanceRange;
    }

    private static bool TryGetXyzValue(IEnumerable<string> pairs, out Vector3D vector, string xName = "x", string yName = "y", string zName = "z")
    {
        double? x = null;
        double? y = null;
        double? z = null;
        vector = default;
        
        foreach (var pair in pairs)
        {
            EntitySelectorParser.ParsePair(pair, out var key, out var value);

            if (string.Equals(key, xName, StringComparison.Ordinal))
            {
                x = double.Parse(value, CultureInfo.InvariantCulture);
            }
            else if (string.Equals(key, yName, StringComparison.Ordinal))
            {
                y = double.Parse(value, CultureInfo.InvariantCulture);
            }
            else if (string.Equals(key, zName, StringComparison.Ordinal))
            {
                z = double.Parse(value, CultureInfo.InvariantCulture);
            }
        }

        if (x.HasValue && y.HasValue && z.HasValue)
        {
            vector = new Vector3D(x.Value, y.Value, z.Value);
            return true;
        }

        return false;
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

    public static DistanceRange ParseDistanceRange(string value)
    {
        DistanceRange range;
        
        if (value.StartsWith("..", StringComparison.InvariantCultureIgnoreCase))
        {
#if DEBUG
            Console.WriteLine("Distance: Max - {0}", value[2..]);                
#endif
                
            range = DistanceRange.MatchRange(null, double.Parse(value[2..], CultureInfo.InvariantCulture));
        }
        else if (value.EndsWith("..", StringComparison.InvariantCultureIgnoreCase))
        {
#if DEBUG
            Console.WriteLine("Distance: Min - {0}", value[..^2]);      
#endif
                
            range = DistanceRange.MatchRange(double.Parse(value[2..], CultureInfo.InvariantCulture), null);
        }
        else if (value.Contains("..", StringComparison.InvariantCultureIgnoreCase))
        {
            var rangeSplit = value.Split("..", StringSplitOptions.TrimEntries);
            range = DistanceRange.MatchRange(double.Parse(rangeSplit[0], CultureInfo.InvariantCulture), 
                double.Parse(rangeSplit[1], CultureInfo.InvariantCulture));
        }
        else
        {
            range = DistanceRange.MatchExact(double.Parse(value, CultureInfo.InvariantCulture));
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
        if (!value.StartsWith('{') || !value.EndsWith('}') || value.Length < 3)
        {
            throw new FormatException("not start or end with braces, or not a valid specifier");
        }

        #if DEBUG
        Console.WriteLine(value[1..^1]);
        #endif
        
        var split = value[1..^1].Split(',');
        var pairNumber = 0;

        foreach (var pair in split)
        {
            var actualPair = pair.Split('=');

            if (actualPair.Length != 2)
            {
                // Invalid syntax: invalid pair
                throw new FormatException($"Pair index {pairNumber} is invalid.");
            }

            var objective = actualPair[0];
            var match = actualPair[1];
            IScoreboardRange range;
            
            if (match.StartsWith("..", StringComparison.InvariantCultureIgnoreCase))
            {
#if DEBUG
                Console.WriteLine("Match: Max - {0}", match[2..]);                
#endif
                
                range = new ScoreboardRangeMatch(objective, new IntegralRange(null, int.Parse(match[2..],
                    CultureInfo.InvariantCulture)));
            }
            else if (match.EndsWith("..", StringComparison.InvariantCultureIgnoreCase))
            {
                #if DEBUG
                Console.WriteLine("Match: Min - {0}", match[..^2]);      
                #endif
                
                range = new ScoreboardRangeMatch(objective, new IntegralRange(int.Parse(match[..^2],
                    CultureInfo.InvariantCulture), null));
            }
            else if (match.Contains("..", StringComparison.InvariantCultureIgnoreCase))
            {
                var rangeSplit = match.Split("..", StringSplitOptions.TrimEntries);
                range = new ScoreboardRangeMatch(objective, 
                    new IntegralRange(
                        int.Parse(rangeSplit[0], CultureInfo.InvariantCulture), 
                        int.Parse(rangeSplit[1], CultureInfo.InvariantCulture)
                        ));
            }
            else
            {
                range = new ScoreboardExactMatch(objective, int.Parse(match, CultureInfo.InvariantCulture));
            }
            
            collection.Add(range);
            pairNumber++;
        }
    }

    internal static IAdvancementCondition ParseCondition(string value)
    {
        if (value.StartsWith('{'))
        {
            if (!value.EndsWith('}')
                || !CriterionAdvancementCondition.TryParse(value[1..^1], out var result))
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
    
    public static void ParseAdvancements(string value, SelectorAdvancementMatchCollection collection)
    {
        if (!value.StartsWith('{') || !value.EndsWith('}') || value.Length < 3)
        {
            throw new SelectorFormatException("Invalid advancement condition set format.", value);
        }

        var set = EntitySelectorParser.ParsePairSet(value[1..^1]);

        foreach (var pair in set)
        {
            EntitySelectorParser.ParsePair(pair, out var name, out var conditionString);

            if (!ResourceLocation.TryParse(name, out var location))
            {
                throw new FormatException("Invalid advancement resource location!");
            }
            
            collection.Add(new SelectorAdvancementMatch(location,
                ParseCondition(conditionString)));
        }
    }
}