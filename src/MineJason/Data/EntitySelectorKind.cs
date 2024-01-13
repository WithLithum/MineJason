namespace MineJason.Data;

/// <summary>
/// An enumeration of all possible kinds of a target selector.
/// </summary>
public enum EntitySelectorKind
{
    /// <summary>
    /// Selects all online players.
    /// </summary>
    AllPlayers,
    /// <summary>
    /// Selects the nearest player. If the executor is a player, then the executor would be selected
    /// unless arguments excludes the executor.
    /// </summary>
    NearestPlayer,
    /// <summary>
    /// Selects a random player from all online players.
    /// </summary>
    RandomPlayer,
    /// <summary>
    /// Selects all entities that are alive.
    /// </summary>
    AllEntities,
    /// <summary>
    /// Selects the executor.
    /// </summary>
    Executor
}