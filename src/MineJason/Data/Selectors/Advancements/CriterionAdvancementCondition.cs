// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

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
        return TryParse(from.AsSpan(), out result);
    }

    /// <summary>
    /// Attempts to parse the specified string representation to its instance representation.
    /// </summary>
    /// <param name="from">The character sequence to parse.</param>
    /// <param name="result">The parse result.</param>
    /// <returns><see langword="true"/> if the syntax is correct; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> from,
        [NotNullWhen(true)] out CriterionAdvancementCondition? result)
    {
        if (!from.StartsWith('{') || !from.EndsWith('}'))
        {
            // FAIL: Not surrounded by braces
            result = null;
            return false;
        }

        var inner = from[1..^1];
        var split = inner.Split(',');

        result = [];

        var readCount = 0;
        foreach (var set in split)
        {
            readCount++;
            if (!CriterionRule.TryParse(inner[set], out var rule))
            {
                // FAIL: rule invalid.
                result = null;
                return false;
            }

            result.Add(rule);
        }

        if (readCount == 0)
        {
            // FAIL: No criterions.
            return false;
        }

        return true;
    }
}