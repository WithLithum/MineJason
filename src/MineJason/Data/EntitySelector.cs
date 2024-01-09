namespace MineJason.Data;

/// <summary>
/// Represents a target selector.
/// </summary>
/// <param name="kind">The kind of the selector.</param>
/// <seealso href="https://minecraft.wiki/w/Target_selectors"/>
public class EntitySelector(EntitySelectorKind kind)
{
    /// <summary>
    /// Gets the kind of the selector.
    /// </summary>
    public EntitySelectorKind Kind { get; } = kind;
    
    /// <summary>
    /// Gets or sets the world position to start searching entities from. If not set, the execution position
    /// of the command is used. This represents the <c>x</c>, <c>y</c> and <c>z</c> arguments.
    /// </summary>
    public Vector3D? Position { get; set; }
    
    /// <summary>
    /// Gets or sets the range, relative from <see cref="Position"/> to search entities in. This represents the
    /// <c>distance</c> argument.
    /// </summary>
    public DistanceRange? Distance { get; set; }
    
    /// <summary>
    /// Gets or sets the diagonal range to search entities from. This represents the <c>dx</c>, <c>dy</c> and
    /// <c>dz</c> arguments.
    /// </summary>
    public Vector3D? DiagonalRange { get; set; }

    /// <summary>
    /// Gets a collection of tags conditions that a entity must fulfill to be selected. This represents the <c>tag</c> arguments.
    /// </summary>
    public TagSelectorCollection Tags { get; } = [];

    /// <summary>
    /// Gets a collection of scoreboard conditions that a entity must fulfill to be selected. This represents the
    /// <c>scores</c> argument.
    /// </summary>
    public ScoreboardSelectorCollection Scores { get; } = [];
    
    /// <summary>
    /// Gets or sets the teams condition a player must fulfill for the player to be selected.
    /// </summary>
    public TeamSelector? Team { get; set; }
}