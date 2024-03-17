// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;

/// <summary>
/// A dictionary of item components.
/// </summary>
[PublicAPI]
public sealed class ItemComponentDictionary : Dictionary<ResourceLocation, IItemComponent>
{
}