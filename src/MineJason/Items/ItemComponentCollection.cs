// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using System.Collections.ObjectModel;
using JetBrains.Annotations;

/// <summary>
/// A collection of item data components. This class cannot be inherited.
/// </summary>
[PublicAPI]
public sealed class ItemComponentCollection : Collection<IItemComponent>
{
    /// <summary>
    /// Initialises a new instance of the <see cref="ItemComponentCollection"/> class.
    /// </summary>
    /// <param name="type">The type of components to accept.</param>
    public ItemComponentCollection(ItemComponentType type)
    {
        Type = type;
    }
    
    /// <summary>
    /// Gets the component type that this instance accepts.
    /// </summary>
    public ItemComponentType Type { get; }

    /// <inheritdoc />
    /// <exception cref="ArgumentException">The item specified is not the accepted item component type.</exception>
    protected override void InsertItem(int index, IItemComponent item)
    {
        ValidateItem(item);

        base.InsertItem(index, item);
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentException">The item specified is not the accepted item component type.</exception>
    protected override void SetItem(int index, IItemComponent item)
    {
        ValidateItem(item);
        
        base.SetItem(index, item);
    }

    private void ValidateItem(IItemComponent item)
    {
        var type = item.GetType();
        if (type != Type.AcceptedType)
        {
            throw new ArgumentException("The specified component is invalid.", nameof(item));
        }
    }
}