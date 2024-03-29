﻿// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Selectors;

using System.Text;
using JetBrains.Annotations;

/// <summary>
/// Represents a name matching condition.
/// </summary>
[PublicAPI]
public sealed class NameMatch : IEquatable<NameMatch>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NameMatch"/> class.
    /// </summary>
    public NameMatch()
    {
    }

    /// <summary>
    /// Gets or sets the name that the player must have to fulfill this requirement.
    /// </summary>
    public string? Include { get; set; }

    /// <summary>
    /// Gets or sets the names that the player must not have to fulfill this requirement.
    /// </summary>
    public List<string> Exclude { get; } = [];

    internal void WriteToBuilder(EntitySelectorArgumentBuilder builder)
    {
        if (Include != null)
        {
#if DEBUG
            Console.WriteLine("NameMatch ToString: include");            
#endif
            
            builder.WritePair("name", Include);
        }

        if (Exclude != null)
        {
#if DEBUG
            Console.WriteLine("NameMatch ToString: exclude, {0} items", Exclude.Count);            
#endif
            
            foreach (var name in Exclude)
            {
                builder.WritePair("name", $"!{name}");
            }
        }
    }
    
    /// <inheritdoc />
    public override string ToString()
    {
        var builder = new StringBuilder();
        var first = false;

        void Comma()
        {
            if (first)
            {
                builder.Append(',');
            }

            first = true;
        }
        
        if (Include != null)
        {
            #if DEBUG
            Console.WriteLine("NameMatch ToString: include");            
            #endif
            
            Comma();
            builder.Append("name=").Append(Include);
        }

        if (Exclude != null)
        {
#if DEBUG
            Console.WriteLine("NameMatch ToString: exclude, {0} items", Exclude.Count);            
#endif
            
            foreach (var name in Exclude)
            {
                Comma();
                builder.Append("name=!").Append(name);
            }
        }

        return builder.ToString();
    }

    /// <inheritdoc />
    public bool Equals(NameMatch other)
    {
        return Include == other.Include && Exclude.Equals(other.Exclude);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is NameMatch other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Include, Exclude);
    }

    /// <summary>
    /// Creates a new instance of <see cref="NameMatch"/> that matches for an exact name.
    /// </summary>
    /// <param name="name">The name to match.</param>
    /// <returns>The created <see cref="NameMatch"/>.</returns>
    public static NameMatch MatchExact(string name)
    {
        return new NameMatch
        {
            Include = name
        };
    }

    /// <summary>
    /// Creates a new instance of <see cref="NameMatch"/> that matches all players except for the players
    /// with the specified names.
    /// </summary>
    /// <param name="names">The names to exclude.</param>
    /// <returns>The created <see cref="NameMatch"/>.</returns>
    public static NameMatch MatchExclude(params string[] names)
    {
        var match = new NameMatch();
        match.Exclude.AddRange(names);
        return match;
    }

    public static bool operator ==(NameMatch left, NameMatch right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(NameMatch left, NameMatch right)
    {
        return !(left == right);
    }
}