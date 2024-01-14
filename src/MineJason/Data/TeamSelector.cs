// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data;

using System.Text;
using JetBrains.Annotations;
using MineJason.Data.Selectors;

/// <summary>
/// Represents a team target selector argument.
/// </summary>
[PublicAPI]
public sealed class TeamSelector
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TeamSelector"/> class.
    /// </summary>
    public TeamSelector()
    {
        
    }
    
    /// <summary>
    /// Gets or sets the team that the player must be in to be selected.
    /// </summary>
    public string? Include { get; set; }

    /// <summary>
    /// Gets or sets that the teams that player must not be in to be selected.
    /// </summary>
    public IList<string> Exclude { get; } = new List<string>();

    /// <inheritdoc />
    public override string ToString()
    {
        // If includes..
        
        if (Include != null)
        {
            return $"team={Include}";
        }

        // Or if excludes..
        
        var builder = new StringBuilder();
        var first = true;
        
        foreach (var exclude in Exclude!)
        {
            if (first)
            {
                first = false;
            }
            else
            {
                builder.Append(',');
            }
            
            builder.Append("team=!").Append(exclude);
        }

        return builder.ToString();
    }

    internal void WriteToBuilder(EntitySelectorArgumentBuilder builder)
    {
        // If includes..
        
        if (Include != null)
        {
            builder.WritePair("team", Include);
        }

        // Or if excludes..
        
        foreach (var exclude in Exclude!)
        {
            builder.WritePair("team", $"!{exclude}");
        }
    }
}