// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Collections;
using System.Text;

namespace MineJason.Extras.Selectors.Matching;

/// <summary>
/// Represents a collection of tag selectors.
/// </summary>
public class TagMatchCollection : ICollection<TagMatch>
{
    private readonly List<TagMatch> _list = [];

    /// <inheritdoc />
    public IEnumerator<TagMatch> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Adds a tag selector to this collection.
    /// </summary>
    /// <param name="item">The selector.</param>
    public void Add(TagMatch item)
    {
        _list.Add(item);
    }

    /// <summary>
    /// Adds a new tag selector to this collection.
    /// </summary>
    /// <param name="tag">The tag to check for.</param>
    /// <param name="present">If <see langword="true"/>, the tag is required to be present; otherwise, the tag is required to be not present.</param>
    public void Add(string tag, bool present)
    {
        Add(new TagMatch(tag, present));
    }

    /// <inheritdoc />
    public void Clear()
    {
        _list.Clear();
    }

    /// <inheritdoc />
    public bool Contains(TagMatch item)
    {
        return _list.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(TagMatch[] array, int arrayIndex)
    {
        _list.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(TagMatch item)
    {
        return _list.Remove(item);
    }

    /// <inheritdoc />
    public int Count => _list.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <summary>
    /// Converts this collection, along with its entries, into a string representation of an entity
    /// selector tag match set.
    /// </summary>
    /// <returns>The converted string representation.</returns>
    public override string ToString()
    {
        var first = false;
        var builder = new StringBuilder();

        foreach (var tag in _list)
        {
            if (first)
            {
                builder.Append(',');
            }

            builder.Append(tag);

            first = true;
        }

        return builder.ToString();
    }

    internal void WriteToBuilder(EntitySelectorArgumentBuilder builder)
    {
        foreach (var tag in _list)
        {
            builder.WritePair("tag", tag.ToString());
        }
    }
}