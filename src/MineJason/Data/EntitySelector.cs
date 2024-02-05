// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data;

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MineJason.Data.Selectors;
using MineJason.Data.Selectors.Advancements;
using MineJason.Serialization.TextJson;

/// <summary>
/// Represents a target selector.
/// </summary>
/// <param name="kind">The kind of the selector.</param>
/// <seealso href="https://minecraft.wiki/w/Target_selectors"/>
[JsonConverter(typeof(EntitySelectorConverter))]
[PublicAPI]
public sealed class EntitySelector(EntitySelectorKind kind) : IEquatable<EntitySelector>
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
    public ScoreboardRangeCollection Scores { get; } = [];

    /// <summary>
    /// Gets or sets the teams condition a player must fulfill for the player to be selected.
    /// </summary>
    public TeamSelector Team { get; } = new();

    /// <summary>
    /// Gets or set the maximum amount of players that this selector can select.
    /// </summary>
    /// <value>
    /// Maximum amount of players that this selector can select. If set to <c>0</c> or less, then this value is
    /// considered unset during serialization.
    /// </value>
    public int Limit { get; set; }
    
    /// <summary>
    /// Gets or sets how this selector sorts its results.
    /// </summary>
    public EntitySelectorSortMode? Sort { get; set; }
    
    /// <summary>
    /// Gets or sets the experience level for a player must be matching to be selected.
    /// </summary>
    public IntegralRange? Level { get; set; }

    /// <summary>
    /// Gets or sets the game mode conditions a player must fulfill to be selected.
    /// </summary>
    public GameModeMatch GameMode { get; } = new();

    /// <summary>
    /// Gets or sets the name conditions a entity must fulfill to be selected. 
    /// </summary>
    public NameMatch Name { get; } = new();
    
    /// <summary>
    /// Gets or sets the vertical rotation range a entity must be at to be selected. This represents the
    /// <c>x_rotation</c> parameter.
    /// </summary>
    public DistanceRange? VerticalRotation { get; set; }
    
    /// <summary>
    /// Gets or sets the horizontal rotation range a entity must be at to be selected. This represents the
    /// <c>y_rotation</c> parameter.
    /// </summary>
    public DistanceRange? HorizontalRotation { get; set; }

    /// <summary>
    /// Gets or sets the type that the entity must belong of to be selected. This represents the
    /// <c>type</c> parameter.
    /// </summary>
    public EntityTypeMatch Type { get; } = new();

    /// <summary>
    /// Gets the exact NBT data conditions that an entity must fulfill to be selected. This represents the
    /// <c>nbt</c> parameter.
    /// </summary>
    public SelectorNbtMatch Nbt { get; } = new();

    /// <summary>
    /// Gets the advancement conditions that a player must fulfill to be selected.
    /// </summary>
    public SelectorAdvancementMatchCollection Advancements { get; } = [];

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
#if DEBUG
        Console.WriteLine("EntitySelector: object equals called");        
#endif
        
        return obj is EntitySelector other && Equals(other);
    }

    /// <inheritdoc />
    public bool Equals(EntitySelector? other)
    {
        #if DEBUG
        Console.WriteLine("EntitySelector: equals called");        
#endif
        
        return other != null && other.ToString().Equals(ToString(), StringComparison.Ordinal);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(ToString());
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return EntitySelectorStringFormatter.ToString(this);
    }
}