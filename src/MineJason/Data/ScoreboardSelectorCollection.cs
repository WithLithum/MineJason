namespace MineJason.Data;

using System.Collections;

/// <summary>
/// Represents a collection of tag selectors.
/// </summary>
public class ScoreboardSelectorCollection : ICollection<ScoreboardSelector>
{
    private readonly List<ScoreboardSelector> _list = [];

    /// <inheritdoc />
    public IEnumerator<ScoreboardSelector> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Adds a scoreboard selector to this collection.
    /// </summary>
    /// <param name="item">The selector.</param>
    public void Add(ScoreboardSelector item)
    {
        _list.Add(item);
    }

    /// <summary>
    /// Adds a new tag selector to this collection.
    /// </summary>
    /// <param name="objective">The objective to check for.</param>
    /// <param name="range">The range to match for.</param>
    public void Add(string objective, ScoreboardRange range)
    {
        Add(new ScoreboardSelector(objective, range));
    }

    /// <inheritdoc />
    public void Clear()
    {
        _list.Clear();
    }

    /// <inheritdoc />
    public bool Contains(ScoreboardSelector item)
    {
        return _list.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(ScoreboardSelector[] array, int arrayIndex)
    {
        _list.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(ScoreboardSelector item)
    {
        return _list.Remove(item);
    }

    /// <inheritdoc />
    public int Count => _list.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;
}