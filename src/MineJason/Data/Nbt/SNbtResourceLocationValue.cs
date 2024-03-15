// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Nbt;

using JetBrains.Annotations;
using MineJason.SNbt.Values;

/// <summary>
/// Represents a resource location as String Nbt value.
/// </summary>
public record struct SNbtResourceLocationValue : ISNbtValue
{
    /// <summary>
    /// Initialises a new instance of the <see cref="SNbtResourceLocationValue"/> structure.
    /// </summary>
    /// <param name="value">The value.</param>
    public SNbtResourceLocationValue(ResourceLocation value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets or sets the value of this instance.
    /// </summary>
    [PublicAPI]
    public ResourceLocation Value { get; set; }
    
    /// <summary>
    /// Converts this instance to its string NBT equivalent.
    /// </summary>
    /// <returns>The converted string.</returns>
    public string ToSNbtString()
    {
        return SNbtStringValue.EscapeString(Value.ToString(), false);
    }
}