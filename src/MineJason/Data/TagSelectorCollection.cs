namespace MineJason.Data;

using System.Collections;
using System.Text;

/// <summary>
/// Represents a collection of tag selectors.
/// </summary>
public class TagSelectorCollection : ICollection<TagSelector>
{
    private readonly List<TagSelector> _list = [];

    /// <inheritdoc />
    public IEnumerator<TagSelector> GetEnumerator()
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
    public void Add(TagSelector item)
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
        Add(new TagSelector(tag, present));
    }

    /// <inheritdoc />
    public void Clear()
    {
        _list.Clear();
    }

    /// <inheritdoc />
    public bool Contains(TagSelector item)
    {
        return _list.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(TagSelector[] array, int arrayIndex)
    {
        _list.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(TagSelector item)
    {
        return _list.Remove(item);
    }

    /// <inheritdoc />
    public int Count => _list.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

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
}