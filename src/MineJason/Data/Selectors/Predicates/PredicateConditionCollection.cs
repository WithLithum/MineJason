// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Selectors.Predicates;

using System.Collections.ObjectModel;
using JetBrains.Annotations;

/// <summary>
/// Represents a collection of predicate conditions. This class cannot be inherited.
/// </summary>
public sealed class PredicateConditionCollection : Collection<PredicateCondition>
{
    /// <summary>
    /// Adds a condition to the end of this collection.
    /// </summary>
    /// <param name="predicate">The predicate to match.</param>
    /// <param name="match">Whether the entity should pass or fail the predicate check to be selected.</param>
    [PublicAPI]
    public void Add(ResourceLocation predicate, bool match)
    {
        Add(new PredicateCondition(predicate, match));
    }
    
    internal void WriteToBuilder(EntitySelectorArgumentBuilder builder)
    {
        foreach (var condition in this)
        {
            builder.WritePair("predicate", condition.ToString());   
        }
    }
}