namespace MineJason.Data.Selectors;

using System.Diagnostics;
using System.Globalization;
using System.Numerics;

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
        var teams = new TeamSelector();
        var names = new NameMatch();

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
                    gameModeMatch ??= new GameModeMatch();
                    var modeValue = gameModeMatch.Value;

                    if (!value.StartsWith('!'))
                    {
                        if (gameModeMatch.Value.Include != null)
                        {
                            throw new FormatException("Already existing gamemode include condition.");
                        }

                        modeValue.Include = ParseGameMode(value);
                    }
                    else
                    {
                        modeValue.Exclude.Add(ParseGameMode(value[1..]));
                    }

                    gameModeMatch = modeValue;
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
                    EntitySelectorParser.ParseTeamsValue(value, ref teams);
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
                    EntitySelectorParser.ParseNameValue(value, ref names);
                    break;
            }
        }
        
        // Assemble all values!
        
        // position
        selector.Position = position;
        
        // diagonal
        selector.DiagonalRange = diagonal;
        
        // game mode
        selector.GameMode = gameModeMatch;
        
        // distance
        selector.Distance = distanceRange;
        
        // teams
        selector.Team = teams;
        
        // names
        selector.Name = names;
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
}