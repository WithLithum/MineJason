// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Selectors;

using JetBrains.Annotations;

/// <summary>
/// Matches NBT conditions for a target selector.
/// </summary>
[PublicAPI]
public sealed class SelectorNbtMatch
{
    /// <summary>
    /// Gets a list of NBT data that the entity must contain for it to be selected.
    /// </summary>
    public IList<NbtProvider> Include { get; } = new List<NbtProvider>();
    
    /// <summary>
    /// Gets a list of NBT data that must absent on entity for it to be selected.
    /// </summary>
    public IList<NbtProvider> Exclude { get; } = new List<NbtProvider>();

    internal void WriteToBuilder(EntitySelectorArgumentBuilder builder)
    {
        foreach (var nbt in Include)
        {
            builder.WritePair("nbt", nbt.ToString());
        }

        foreach (var nbt in Exclude)
        {
            builder.WritePair("nbt", $"!{nbt}");
        }
    }
}