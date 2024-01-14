// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Selectors;

using JetBrains.Annotations;

/// <summary>
/// Provides building services for <see cref="EntitySelector"/>.
/// </summary>
[PublicAPI]
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

    /// <summary>
    /// Adds a score requirement that an entity must fulfill to be selected.
    /// Entities not being tracked by the specified objective will be automatically excluded.
    /// </summary>
    /// <param name="range">The range to match.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder Score(IScoreboardRange range)
    {
        _selector.Scores.Add(range);
        return this;
    }

    /// <summary>
    /// Adds a score requirement that an entity must fulfill to be selected.
    /// Entities not being tracked by the specified objective will be automatically excluded.
    /// </summary>
    /// <param name="objective">The objective to check for.</param>
    /// <param name="exact">The exact value to match.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder ScoreExact(string objective, int exact)
    {
        _selector.Scores.Add(new ScoreboardExactMatch(objective, exact));
        return this;
    }
    
    /// <summary>
    /// Adds a score requirement that an entity must fulfill to be selected.
    /// Entities not being tracked by the specified objective will be automatically excluded.
    /// </summary>
    /// <param name="objective">The objective to check for.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder ScoreRange(string objective, int? min, int? max)
    {
        _selector.Scores.Add(new ScoreboardRangeMatch(objective, new IntegralRange(min, max)));
        return this;
    }

    /// <summary>
    /// Adds a rule that requires an entity to carry the specified tag in order to be selected.
    /// </summary>
    /// <param name="tag">The tag to include.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder IncludeTag(string tag)
    {
        _selector.Tags.Add(new TagSelector(tag, true));
        return this;
    }

    /// <summary>
    /// Adds a rule that requires an entity to carry the
    /// </summary>
    /// <param name="tag">The tag to exclude.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder ExcludeTag(string tag)
    {
        _selector.Tags.Add(new TagSelector(tag, false));
        return this;
    }
    
    /// <summary>
    /// Sets the team that an entity must be in to be selected. If the team was previously set, it will be overriden.
    /// </summary>
    /// <param name="team">The team to check.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder InTeam(string team)
    {
        _selector.Team.Include = team;
        return this;
    }

    /// <summary>
    /// Adds a rule that requires an entity to be outside of the specified team to be selected.
    /// </summary>
    /// <param name="team">The team.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder ExcludeTeam(string team)
    {
        _selector.Team.Exclude.Add(team);
        return this;
    }

    /// <summary>
    /// Sets the name an entity must have to be selected.
    /// </summary>
    /// <param name="name">The name to match.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder HasName(string name)
    {
        _selector.Name.Include = name;
        return this;
    }

    /// <summary>
    /// Adds a rule that excludes entities with the specified name from being selected.
    /// </summary>
    /// <param name="name">The name to exclude.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder ExcludeName(string name)
    {
        _selector.Name.Exclude.Add(name);
        return this;
    }

    /// <summary>
    /// Sets the game mode a player must be in to be selected. Non-player entities will be automatically excluded.
    /// </summary>
    /// <param name="mode">The game mode to match.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder InGameMode(GameMode mode)
    {
        _selector.GameMode.Include = mode;
        
        return this;
    }

    /// <summary>
    /// Sets the game mode a player must not be in to be selected. Non-player entities will be automatically excluded.
    /// </summary>
    /// <param name="mode">The game mode to exclude.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder ExcludeGameMode(GameMode mode)
    {
        _selector.GameMode.Exclude.Add(mode);

        return this;
    }

    /// <summary>
    /// Sets the experience level requirement that a player must fulfill to be selected.
    /// Non-player entities will be automatically excluded.
    /// </summary>
    /// <param name="range">The range requirement.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder HasLevel(IntegralRange range)
    {
        _selector.Level = range;
        return this;
    }

    /// <summary>
    /// Sets a vertical rotation (X rotation) range requirement that an entity must fulfill
    /// to be selected.
    /// </summary>
    /// <param name="range">The range requirement.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder VerticalRotation(DistanceRange range)
    {
        _selector.VerticalRotation = range;
        return this;
    }

    /// <summary>
    /// Sets a horizontal rotation (Y rotation) range requirement that an entity must fulfill
    /// to be selected.
    /// </summary>
    /// <param name="range">The range requirement.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder HorizontalRotation(DistanceRange range)
    {
        _selector.HorizontalRotation = range;
        return this;
    }

    /// <summary>
    /// Sets the type that an entity must be of to be selected. If the type was previously set, it will be overriden.
    /// </summary>
    /// <param name="type">The type to set to.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder MatchType(ResourceLocation type)
    {
        _selector.Type.Include = type;
        return this;
    }

    /// <summary>
    /// Adds a rule that excludes the entities of the specified type.
    /// </summary>
    /// <param name="location">The resource location of the type.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder ExcludeType(ResourceLocation location)
    {
        _selector.Type.Exclude.Add(location);
        return this;
    }
    
    /// <summary>
    /// Sets how the selector sorts the selected entities.
    /// </summary>
    /// <param name="mode">The mode to set to.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder SortBy(EntitySelectorSortMode mode)
    {
        _selector.Sort = mode;
        return this;
    }

    /// <summary>
    /// Sets the limit of how many entities that the selector may select.
    /// </summary>
    /// <param name="limit">The limit.</param>
    /// <returns>This instance.</returns>
    public EntitySelectorBuilder Limit(int limit)
    {
        _selector.Limit = limit;
        return this;
    }

    /// <summary>
    /// Returns the assembled selector.
    /// </summary>
    /// <returns>The selector.</returns>
    public EntitySelector Build()
    {
        return _selector;
    }
}