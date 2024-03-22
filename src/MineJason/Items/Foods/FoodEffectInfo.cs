// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items.Foods;

using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using MineJason.Data.Nbt;
using MineJason.SNbt;
using MineJason.SNbt.Values;

/// <summary>
/// Represents information about an effect can be applied when eating certain items.
/// </summary>
[PublicAPI]
public readonly record struct FoodEffectInfo : ISNbtValue, ISNbtWritable
{
    /// <summary>
    /// Initialises a new instance of the <see cref="FoodEffectInfo"/> structure.
    /// </summary>
    /// <param name="effect">The effect to apply.</param>
    /// <param name="probability">A single-precision floating-point number ranging between <c>0.0</c> and <c>1.0</c>, that is the chance of this effect being applied.</param>
    public FoodEffectInfo(ResourceLocation effect, float probability)
    {
        Effect = effect;
        Probability = probability;
    }
    
    /// <summary>
    /// Gets or sets the identifier of the effect to apply.
    /// </summary>
    public ResourceLocation Effect { get; }

    /// <summary>
    /// Gets or sets the probability of such effect being applied to the player eating this
    /// item.
    /// </summary>
    /// <value>
    /// A normalised single-precision floating-point number ranging between <c>0.0</c> and <c>1.0</c>.
    /// </value>
    /// <exception cref="ArgumentOutOfRangeException">Argument is not within the range.</exception>
    [Range(0f, 1f)]
    public float Probability { get; }

    /// <summary>
    /// Converts this instance to its string NBT representation.
    /// </summary>
    /// <returns>The string NBT representation.</returns>
    public string ToSNbtString()
    {
        var stream = new StringWriter();
        using (var writer = new SNbtWriter(stream))
        {
            WriteTo(writer);
        }

        return stream.ToString();
    }

    /// <summary>
    /// Writes the data of this instance to the specified writer.
    /// </summary>
    /// <param name="writer">The writer.</param>
    public void WriteTo(SNbtWriter writer)
    {
        writer.WriteBeginCompound();
        writer.WriteProperty("effect", new SNbtResourceLocationValue(Effect));
        writer.WriteProperty("probability", Probability);
        writer.WriteEndCompound();
    }
}