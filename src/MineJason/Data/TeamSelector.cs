namespace MineJason.Data;

using System.Text;
using JetBrains.Annotations;
using MineJason.Data.Selectors;

/// <summary>
/// Represents a team target selector argument.
/// </summary>
[PublicAPI]
public struct TeamSelector
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TeamSelector"/> structure.
    /// </summary>
    public TeamSelector()
    {
        
    }
    
    /// <summary>
    /// Gets or sets the team that the player must be in to be selected.
    /// </summary>
    public string? Team { get; set; }

    /// <summary>
    /// Gets or sets that the teams that player must not be in to be selected.
    /// </summary>
    public IList<string> Exclude { get; } = new List<string>();

    /// <inheritdoc />
    public override string ToString()
    {
        // If includes..
        
        if (Team != null)
        {
            return $"team={Team}";
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
        
        if (Team != null)
        {
            builder.WritePair("team", Team);
        }

        // Or if excludes..
        
        foreach (var exclude in Exclude!)
        {
            builder.WritePair("team", $"!{exclude}");
        }
    }
}