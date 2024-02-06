// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Selectors.Advancements;

using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using JetBrains.Annotations;

/// <summary>
/// Represents a criterion advancement condition.
/// </summary>
public sealed class CriterionAdvancementCondition : Collection<CriterionRule>,
    IAdvancementCondition
{
    /// <inheritdoc />
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append('{');
        var first = false;
        
        foreach (var value in this)
        {
            if (first)
            {
                builder.Append(',');
            }
            
            builder.Append(value);
            first = true;
        }

        builder.Append('}');
        return builder.ToString();
    }

    /// <summary>
    /// Attempts to parse the specified string representation to its instance representation.
    /// </summary>
    /// <param name="from">The string to parse.</param>
    /// <param name="result">The parse result.</param>
    /// <returns><see langword="true"/> if the syntax is correct; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(string from, [NotNullWhen(true)] out CriterionAdvancementCondition? result)
    {
        if (!from.StartsWith('{') || !from.EndsWith('}'))
        {
            // FAIL: Not surrounded by braces
            result = null;
            return false;
        }

        var split = from[1..^1].Split(',');

        result = null;
        
        if (split.Length < 1)
        {
            // No conditions... Do not allow this be valid. FAIL.
            return false;
        }

        result = new CriterionAdvancementCondition();
        
        foreach (var set in split)
        {
            if (!CriterionRule.TryParse(set, out var rule))
            {
                // FAIL: rule invalid.
                result = null;
                return false;
            }
            
            result.Add(rule);
        }

        return true;
    }
}