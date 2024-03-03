// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;

/// <summary>
/// Represents a type of item component.
/// </summary>
[PublicAPI]
public readonly struct ItemComponentType : IEquatable<ItemComponentType>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemComponentType"/> class.
    /// </summary>
    /// <param name="acceptedType">The accepted type of the value.</param>
    /// <param name="identifier">The identifier of this instance.</param>
    public ItemComponentType(Type acceptedType, ResourceLocation identifier)
    {
        AcceptedType = acceptedType;
        Identifier = identifier;
    }

    /// <summary>
    /// Gets the identifier of this instance.
    /// </summary>
    public ResourceLocation Identifier { get; }
    
    /// <summary>
    /// Gets the type of that is accepted.
    /// </summary>
    public Type AcceptedType { get; }

    /// <inheritdoc />
    public bool Equals(ItemComponentType other)
    {
        return Identifier.Equals(other.Identifier)
               && AcceptedType == other.AcceptedType;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ItemComponentType other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Identifier, AcceptedType);
    }

    /// <summary>
    /// Determines whether the instance to the left is equivalent with the instance to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the values are equivalent; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(ItemComponentType left, ItemComponentType right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether the instance to the left is different than the instance to the right.
    /// </summary>
    /// <param name="left">The instance to the left.</param>
    /// <param name="right">The instance to the right.</param>
    /// <returns><see langword="true"/> if the values are different; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(ItemComponentType left, ItemComponentType right)
    {
        return !(left == right);
    }
}