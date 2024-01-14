namespace MineJason.Data.Selectors;

/// <summary>
/// Provides building services for <see cref="EntitySelector"/>.
/// </summary>
public class EntitySelectorBuilder
{
    private readonly EntitySelector _selector;
    
    internal EntitySelectorBuilder(EntitySelector selector)
    {
        _selector = selector;
    }

    /// <summary>
    /// Sets the position for the selector to start searching from.
    /// </summary>
    /// <param name="value">The position to set to.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder Position(Vector3D value)
    {
        _selector.Position = value;
        return this;
    }

    /// <summary>
    /// Sets the exact Euclidean distance for the selector to search in.
    /// </summary>
    /// <param name="exact">The exact distance.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder Distance(int exact)
    {
        _selector.Distance = DistanceRange.MatchExact(exact);
        return this;
    }

    /// <summary>
    /// Sets the Euclidean distance range for the selector to search in.
    /// </summary>
    /// <param name="min">The minimum distance. If omitted, the minimum distance is not considered.</param>
    /// <param name="max">The maximum distance. If omitted, the maximum distance is not considered.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder Distance(int? min, int? max)
    {
        _selector.Distance = DistanceRange.MatchRange(min, max);
        return this;
    }

    /// <summary>
    /// Sets the diagonal range for the selector to search in.
    /// </summary>
    /// <param name="range">The diagonal range.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder DiagonalRange(Vector3D range)
    {
        _selector.DiagonalRange = range;
        return this;
    }

    public EntitySelectorBuilder Score(IScoreboardRange range)
    {
        _selector.Scores.Add(range);
        return this;
    }

    public EntitySelectorBuilder ScoreExact(string objective, int exact)
    {
        _selector.Scores.Add(new ScoreboardExactMatch(objective, exact));
        return this;
    }
    
    public EntitySelectorBuilder ScoreRange(string objective, int? min, int? max)
    {
        _selector.Scores.Add(new ScoreboardRangeMatch(objective, new IntegralRange(min, max)));
        return this;
    }

    public EntitySelectorBuilder IncludeTag(string tag)
    {
        _selector.Tags.Add(new TagSelector(tag, true));
        return this;
    }

    public EntitySelectorBuilder ExcludeTag(string tag)
    {
        _selector.Tags.Add(new TagSelector(tag, false));
        return this;
    }

    public EntitySelectorBuilder InTeam(string team)
    {
        _selector.Team.Include = team;
        return this;
    }

    public EntitySelectorBuilder ExcludeTeam(string team)
    {
        _selector.Team.Exclude.Add(team);
        return this;
    }

    public EntitySelectorBuilder HasName(string name)
    {
        _selector.Name.Include = name;
        return this;
    }

    public EntitySelectorBuilder ExcludeName(string name)
    {
        _selector.Name.Exclude.Add(name);
        return this;
    }

    public EntitySelectorBuilder InGameMode(GameMode mode)
    {
        _selector.GameMode.Include = mode;
        
        return this;
    }

    public EntitySelectorBuilder ExcludeGameMode(GameMode mode)
    {
        _selector.GameMode.Exclude.Add(mode);

        return this;
    }

    public EntitySelectorBuilder HasLevel(IntegralRange range)
    {
        _selector.Level = range;
        return this;
    }

    public EntitySelectorBuilder VerticalRotation(DistanceRange range)
    {
        _selector.VerticalRotation = range;
        return this;
    }

    public EntitySelectorBuilder HorizontalRotation(DistanceRange range)
    {
        _selector.HorizontalRotation = range;
        return this;
    }

    public EntitySelectorBuilder MatchType(ResourceLocation type)
    {
        _selector.Type.Include = type;
        return this;
    }

    public EntitySelectorBuilder ExcludeType(ResourceLocation location)
    {
        _selector.Type.Exclude.Add(location);
        return this;
    }
    
    public EntitySelectorBuilder SortBy(EntitySelectorSortMode mode)
    {
        _selector.Sort = mode;
        return this;
    }

    public EntitySelectorBuilder Limit(int limit)
    {
        _selector.Limit = limit;
        return this;
    }
}