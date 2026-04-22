// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Extras.Selectors.Matching;

using System;
using System.Collections.ObjectModel;

/// <summary>
/// A collection of <see cref="EntityNameMatch"/>.
/// </summary>
public class EntityNameMatchCollection : Collection<EntityNameMatch>
{
    /// <summary>
    /// Adds a new match rule.
    /// </summary>
    /// <param name="name">The name to match.</param>
    /// <param name="match">Whether the entity is required to have the specified name or the name is required to not have the specified name.</param>
    public void Add(string name, bool match = true)
    {
        Add( new EntityNameMatch(name, match));
    }
    
    internal void WriteToBuilder(EntitySelectorArgumentBuilder builder)
    {
        foreach (var item in this)
        {
            builder.WritePair("name", item.Value ? item.Name : $"!{item.Name}");
        }
    }
}
