// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items.Buckets;

using JetBrains.Annotations;
using MineJason.Data.Nbt;
using MineJason.SNbt;
using MineJason.SNbt.Values;

/// <summary>
/// Represents a specific set of tags applied to an entity in a bucket.
/// </summary>
[PublicAPI]
public struct BucketEntityData : INbtConvertible, IEquatable<BucketEntityData>
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity in the bucket has no AI.
    /// </summary>
    public bool NoAi { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the entity will not make any sounds.
    /// </summary>
    public bool Silent { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the entity has no gravity.
    /// </summary>
    public bool NoGravity { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the entity will have a glow effect.
    /// </summary>
    public bool Glowing { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the entity will not receive any damage except by Creative mode players.
    /// </summary>
    public bool Invulnerable { get; set; }
    
    /// <summary>
    /// Gets or sets the current health of the entity.
    /// </summary>
    public float Health { get; set; }
    
    /// <summary>
    /// Gets or sets the age of the entity.
    /// </summary>
    public int Age { get; set; }
    
    /// <summary>
    /// Gets or sets the variant for axolotls.
    /// </summary>
    public int? Variant { get; set; }
    
    /// <summary>
    /// Gets or sets the hunting cooldown for axolotls.
    /// </summary>
    public long? HuntingCooldown { get; set; }
    
    /// <summary>
    /// Gets or sets the variant for tropical fishes.
    /// </summary>
    public int? BucketVariantTag { get; set; }

    /// <inheritdoc />
    public ISNbtValue ToNbt()
    {
        return new SNbtObjectBuilder()
            .Property("NoAI", NoAi)
            .Property("Silent", Silent)
            .Property("NoGravity", NoGravity)
            .Property("Glowing", Glowing)
            .Property("Invulnerable", Invulnerable)
            .Property("Health", Health)
            .Property("Age", Age)
            .Property("Variant", Variant)
            .Property("HuntingCooldown", HuntingCooldown)
            .Property("BucketVariantTag", BucketVariantTag);
    }

    /// <inheritdoc />
    public bool Equals(BucketEntityData other)
    {
        return NoAi == other.NoAi 
               && Silent == other.Silent 
               && NoGravity == other.NoGravity
               && Glowing == other.Glowing
               && Invulnerable == other.Invulnerable
               && Health.Equals(other.Health)
               && Age == other.Age
               && Variant == other.Variant
               && HuntingCooldown == other.HuntingCooldown
               && BucketVariantTag == other.BucketVariantTag;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is BucketEntityData other && Equals(other);
    }

    /// <summary>
    /// Calculates the hash code of this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(NoAi);
        hashCode.Add(Silent);
        hashCode.Add(NoGravity);
        hashCode.Add(Glowing);
        hashCode.Add(Invulnerable);
        hashCode.Add(Health);
        hashCode.Add(Age);
        hashCode.Add(Variant);
        hashCode.Add(HuntingCooldown);
        hashCode.Add(BucketVariantTag);
        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> is equivalent to the instance
    /// to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The left instance.</param>
    /// <param name="right">The right instance.</param>
    /// <returns><see langword="true"/> if the instance to the <paramref name="left"/> is equivalent to the instance
    /// to the <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(BucketEntityData left, BucketEntityData right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the <paramref name="left"/> is different than the instance
    /// to the <paramref name="right"/>.
    /// </summary>
    /// <param name="left">The left instance.</param>
    /// <param name="right">The right instance.</param>
    /// <returns><see langword="true"/> if the instance to the <paramref name="left"/> is equivalent to the instance
    /// to the <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(BucketEntityData left, BucketEntityData right)
    {
        return !(left == right);
    }
}