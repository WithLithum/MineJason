// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using JetBrains.Annotations;
using MineJason.SNbt.Values;

/// <summary>
/// Represents an item component of the chat component value.
/// </summary>
public abstract class ChatComponentItemComponent : IItemComponent
{
    /// <summary>
    /// Initialises a new instance of the <see cref="ChatComponentItemComponent"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    protected ChatComponentItemComponent(ChatComponent value)
    {
        Value = value;
    }
    
    /// <summary>
    /// Gets or sets the value of this instance.
    /// </summary>
    [PublicAPI]
    public ChatComponent Value { get; set; }

    /// <inheritdoc />
    public string GetString()
    {
        return SNbtStringValue.EscapeString(Value.ToString());
    }

    /// <inheritdoc />
    public bool IsValid()
    {
        return true;
    }
}