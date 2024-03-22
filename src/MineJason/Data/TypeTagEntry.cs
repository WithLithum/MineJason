// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data;

using System.Text;
using JetBrains.Annotations;
using MineJason.SNbt;
using MineJason.SNbt.Values;

/// <summary>
/// Represents an entry in a type/tag collection.
/// </summary>
public record struct TypeTagEntry : ISNbtWritable, ISNbtValue
{
    /// <summary>
    /// Initialises a new instance of the <see cref="TypeTagEntry"/> structure.
    /// </summary>
    /// <param name="isTag">If <see langword="true"/>, this instance is regarded as a reference to a tag.</param>
    /// <param name="identifier">The identifier.</param>
    public TypeTagEntry(bool isTag, ResourceLocation identifier)
    {
        IsTag = isTag;
        Identifier = identifier;
    }
    
    /// <summary>
    /// Gets or sets a value indicating whether this instance is a reference to a tag rather than to a type.
    /// </summary>
    [PublicAPI]
    public bool IsTag { get; set; }
    
    /// <summary>
    /// Gets or sets the identifier value of this instance.
    /// </summary>
    [PublicAPI]
    public ResourceLocation Identifier { get; set; }

    /// <summary>
    /// Converts this instance to its string representation.
    /// </summary>
    /// <returns>The string representation.</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        if (IsTag)
        {
            builder.Append('#');
        }

        builder.Append(Identifier);
        
        return builder.ToString();
    }

    /// <summary>
    /// Converts this instance to its String NBT representation.
    /// </summary>
    /// <returns>The String NBT representation.</returns>
    public string ToSNbtString()
    {
        return SNbtStringValue.EscapeString(ToString());
    }

    /// <summary>
    /// Writes this instance to the specified writer.
    /// </summary>
    /// <param name="writer">The writer.</param>
    public void WriteTo(SNbtWriter writer)
    {
        writer.WriteValue(ToString());
    }

    /// <summary>
    /// Parses the specified string representation and converts it to the instance representation.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="result">The parsed result.</param>
    /// <returns><see langword="true"/> if the input is valid; otherwise, <see langword="false/>.</returns>
    public static bool TryParse(string input, out TypeTagEntry result)
    {
        var isTag = false;
        var parse = input;
        
        if (input.StartsWith('#'))
        {
            isTag = true;
            parse = input[1..];
        }

        if (!ResourceLocation.TryParse(parse, out var id))
        {
            result = default;
            return false;
        }

        result = new TypeTagEntry(isTag, id);
        return true;
    }
}