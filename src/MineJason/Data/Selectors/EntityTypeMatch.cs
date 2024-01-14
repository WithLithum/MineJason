// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Selectors;

using JetBrains.Annotations;

/// <summary>
/// Provides matching services for entity types.
/// </summary>
[PublicAPI]
public sealed class EntityTypeMatch : IEquatable<EntityTypeMatch>
{
    /// <summary>
    /// Gets or sets the type the entity must be of to be selected.
    /// </summary>
    public ResourceLocation? Include { get; set; }

    /// <summary>
    /// Gets a list of the types the entity must not be of to be selected.
    /// </summary>
    public IList<ResourceLocation> Exclude { get; } = new List<ResourceLocation>();

    internal void WriteToBuilder(EntitySelectorArgumentBuilder builder)
    {
        if (Include.HasValue)
        {
            builder.WritePair("type", Include.Value.ToString());
        }

        foreach (var type in Exclude)
        {
            builder.WritePair("type", $"!{type.ToString()}");
        }
    }

    /// <inheritdoc />
    public bool Equals(EntityTypeMatch? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Nullable.Equals(Include, other.Include) && Exclude.Equals(other.Exclude);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is EntityTypeMatch other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Include ?? new ResourceLocation("blah", "ruh"), Exclude);
    }
}