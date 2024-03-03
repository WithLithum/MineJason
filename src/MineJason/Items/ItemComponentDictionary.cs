// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using BaseCollection = ICollection<KeyValuePair<ItemComponentType, ItemComponentCollection>>;

/// <summary>
/// A dictionary of item components.
/// </summary>
[PublicAPI]
public sealed class ItemComponentDictionary : IDictionary<ItemComponentType, ItemComponentCollection>
{
    private readonly Dictionary<ItemComponentType, ItemComponentCollection> _dictionary
        = new();

    public IEnumerator<KeyValuePair<ItemComponentType, ItemComponentCollection>> GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_dictionary).GetEnumerator();
    }

    /// <inheritdoc />
    public void Add(KeyValuePair<ItemComponentType, ItemComponentCollection> item)
    {
        Add(item.Key, item.Value);
    }

    /// <summary>
    /// Removes all items from this instance.
    /// </summary>
    public void Clear()
    {
        _dictionary.Clear();
    }

    /// <inheritdoc />
    public bool Contains(KeyValuePair<ItemComponentType, ItemComponentCollection> item)
    {
        return _dictionary.Contains(item);
    }

    /// <summary>
    /// Unsupported operation.
    /// </summary>
    /// <param name="array">The array.</param>
    /// <param name="arrayIndex">The array index.</param>
    /// <exception cref="NotSupportedException">This operation is not supported.</exception>
    void BaseCollection.CopyTo(KeyValuePair<ItemComponentType, ItemComponentCollection>[] array, int arrayIndex)
    {
        throw new NotSupportedException();
    }

    bool BaseCollection.Remove(KeyValuePair<ItemComponentType, ItemComponentCollection> item)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Gets the number of the elements currently in this instance.
    /// </summary>
    public int Count => _dictionary.Count;

    /// <summary>
    /// Gets a value indicating whether this instance is read only.
    /// </summary>
    public bool IsReadOnly => ((BaseCollection)_dictionary).IsReadOnly;

    /// <inheritdoc />
    public void Add(ItemComponentType key, ItemComponentCollection value)
    {
        CheckValue(key, value);
        
        _dictionary.Add(key, value);
    }

    /// <inheritdoc />
    public bool ContainsKey(ItemComponentType key)
    {
        return _dictionary.ContainsKey(key);
    }

    /// <inheritdoc />
    public bool Remove(ItemComponentType key)
    {
        return _dictionary.Remove(key);
    }

    /// <inheritdoc />
    public bool TryGetValue(ItemComponentType key, 
        [MaybeNullWhen(false)] out ItemComponentCollection value)
    {
        return _dictionary.TryGetValue(key, out value);
    }

    /// <summary>
    /// Gets the collection associated with the specified item component type. If the collection does not exist yet,
    /// it will be created.
    /// </summary>
    /// <param name="key">The key.</param>
    public ItemComponentCollection this[ItemComponentType key]
    {
        get => SafeGet(key);
        set => SafeSet(key, value);
    }

    private ItemComponentCollection SafeGet(ItemComponentType key)
    {
        if (_dictionary.TryGetValue(key, out var value))
        {
            return value;
        }

        value = new ItemComponentCollection(key);
        _dictionary.Add(key, value);
        return value;
    }

    private void SafeSet(ItemComponentType key, ItemComponentCollection value)
    {
        CheckValue(key, value);

        _dictionary[key] = value;
    }

    private static void CheckValue(ItemComponentType type, ItemComponentCollection value)
    {
        if (!value.Type.Equals(type))
        {
            throw new ArgumentException("The specified collection is invalid to set.", nameof(value));
        }
    }

    /// <inheritdoc />
    public ICollection<ItemComponentType> Keys => _dictionary.Keys;

    /// <inheritdoc />
    public ICollection<ItemComponentCollection> Values => _dictionary.Values;
}