namespace MineJason.Data;

using System.Collections;
using System.Text;
using MineJason.Data.Selectors;

/// <summary>
/// Represents a collection of tag selectors.
/// </summary>
public class ScoreboardRangeCollection : ICollection<IScoreboardRange>
{
    private readonly List<IScoreboardRange> _list = [];

    /// <inheritdoc />
    public IEnumerator<IScoreboardRange> GetEnumerator()
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
    public void Add(IScoreboardRange item)
    {
        _list.Add(item);
    }

    /// <summary>
    /// Adds a new tag selector to this collection.
    /// </summary>
    /// <param name="objective">The objective to check for.</param>
    /// <param name="range">The range to match for.</param>
    public void Add(string objective, IntegralRange range)
    {
        Add(new ScoreboardRangeMatch(objective, range));
    }

    /// <inheritdoc />
    public void Clear()
    {
        _list.Clear();
    }

    /// <inheritdoc />
    public bool Contains(IScoreboardRange item)
    {
        return _list.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(IScoreboardRange[] array, int arrayIndex)
    {
        _list.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(IScoreboardRange item)
    {
        return _list.Remove(item);
    }

    /// <inheritdoc />
    public int Count => _list.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append('{');
        var first = false;
        
        foreach (var selector in _list)
        {
            if (first)
            {
                builder.Append(',');
            }
            
            first = true;

            builder.Append(selector.Objective);
            builder.Append('=');
            builder.Append(selector.GetString());
        }

        builder.Append('}');

        return builder.ToString();
    }
}