// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Data.Selectors;

using JetBrains.Annotations;
using MineJason.Data.Nbt;

/// <summary>
/// Matches NBT conditions for a target selector.
/// </summary>
[PublicAPI]
public sealed class SelectorNbtMatch
{
    /// <summary>
    /// Gets a list of NBT data that the entity must contain for it to be selected.
    /// </summary>
    public IList<INbtDataProvider> Include { get; } = new List<INbtDataProvider>();
    
    /// <summary>
    /// Gets a list of NBT data that must absent on entity for it to be selected.
    /// </summary>
    public IList<INbtDataProvider> Exclude { get; } = new List<INbtDataProvider>();

    internal void WriteToBuilder(EntitySelectorArgumentBuilder builder)
    {
        foreach (var nbt in Include)
        {
            builder.WritePair("nbt", nbt.GetRawNbt());
        }

        foreach (var nbt in Exclude)
        {
            builder.WritePair("nbt", $"!{nbt.GetRawNbt()}");
        }
    }
}