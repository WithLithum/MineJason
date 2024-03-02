// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Items;

using MineJason.Utilities;

/// <summary>
/// Represents a chat component that sets the name of the item. This class cannot be inherited.
/// </summary>
public sealed class CustomNameItemComponent : TypedItemComponent<ChatComponent>
{
    /// <summary>
    /// The type ID of this component.
    /// </summary>
    public static readonly ResourceLocation Id = new("minecraft", "custom_name");

    /// <summary>
    /// Initialises a new instance of the <see cref="ChatComponent"/> class.
    /// </summary>
    /// <param name="value"></param>
    public CustomNameItemComponent(ChatComponent value) : base(value)
    {
    }

    /// <inheritdoc/>
    public override bool IsValid()
    {
        return true;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return SpecificValueUtil.ToEscapedComponentString(Value);
    }
}
