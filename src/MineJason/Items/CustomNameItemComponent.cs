// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;
using MineJason.Utilities;

/// <summary>
/// Represents a chat component that sets the name of the item. This class cannot be inherited.
/// </summary>
[PublicAPI]
public sealed class CustomNameItemComponent : ChatComponentItemComponent
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ItemComponentType Type = new(typeof(CustomNameItemComponent),
        new ResourceLocation("minecraft", "custom_name"));

    /// <summary>
    /// Initialises a new instance of the <see cref="ChatComponent"/> class.
    /// </summary>
    /// <param name="value"></param>
    public CustomNameItemComponent(ChatComponent value) : base(value)
    {
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return SpecificValueUtil.ToEscapedComponentString(Value);
    }
}
