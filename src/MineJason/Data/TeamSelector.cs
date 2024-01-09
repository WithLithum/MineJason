namespace MineJason.Data;

using System.Text;
using JetBrains.Annotations;

/// <summary>
/// Represents a team target selector argument.
/// </summary>
[PublicAPI]
public struct TeamSelector
{
    /// <summary>
    /// Gets or sets the team that the player must be in to be selected.
    /// </summary>
    public string? Team { get; set; }
    
    /// <summary>
    /// Gets or sets that the teams that player must not be in to be selected.
    /// </summary>
    public IEnumerable<string>? Exclude { get; set; }

    /// <summary>
    /// Determines whether this instance is valid.
    /// </summary>
    /// <returns><see langword="true"/> if this instance is valid; otherwise, <see langword="false"/>.</returns>
    public bool IsValid()
    {
        return Team is null || Exclude is null;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        if (!IsValid())
        {
            return "<invalid team selector>";
        }
        
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
            builder.Append("team=!{").Append(exclude);

            if (first)
            {
                first = false;
            }
            else
            {
                builder.Append(',');
            }
        }

        return builder.ToString();
    }
}