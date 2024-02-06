// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Selectors.Advancements;

using System.Collections.ObjectModel;
using System.Text;

/// <summary>
/// Represents a set of selector advancement matches.
/// </summary>
public sealed class SelectorAdvancementMatchCollection : Collection<SelectorAdvancementMatch>
{
    /// <summary>
    /// Returns the string representation of this instance.
    /// </summary>
    /// <returns>The string representation.</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.Append('{');
        var first = false;

        foreach (var match in this)
        {
            if (first)
            {
                builder.Append(',');
            }
            
            builder.Append(match);
            first = true;
        }

        builder.Append('}');
        return builder.ToString();
    }
}