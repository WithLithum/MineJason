// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Selectors;

using System;
using System.Text;
using MineJason.Exceptions;

/// <summary>
/// Resolves a set of value pairs for entity target selectors. This class cannot be inherited.
/// </summary>
public sealed class EntitySelectorPairSetResolver
{
    private readonly StringBuilder _builder = new();

    // How many secondary braces are there.
    private int _braceLevel;

    private const char EqualSign = '=';
    private const char LeftBrace = '{';
    private const char RightBrace = '}';
    private const char Comma = ',';
    
    /// <summary>
    /// Represents the position within or outside of braces.
    /// </summary>
    public enum BraceState
    {
        /// <summary>
        /// Outside of all braces.
        /// </summary>
        Outside,
        /// <summary>
        /// Inside the first brace, but outside of any secondary braces.
        /// </summary>
        First,
        /// <summary>
        /// Inside any secondary brace.
        /// </summary>
        Second
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="EntitySelectorPairSetResolver"/> class.
    /// </summary>
    /// <param name="read">The string to resolve.</param>
    public EntitySelectorPairSetResolver(string read)
    {
        From = read;
    }
    
    /// <summary>
    /// Gets the current position in brace.
    /// </summary>
    public BraceState Brace { get; private set; }
    
    /// <summary>
    /// Gets a value indicating whether the cursor is currently inside a value pair of a pair.
    /// </summary>
    public bool InValue { get; private set; }
    
    /// <summary>
    /// Gets the string that this instance is reading.
    /// </summary>
    public string From { get; }

    /// <summary>
    /// Gets the position of the cursor.
    /// </summary>
    public int Cursor { get; private set; }

    /// <summary>
    /// Gets the parsed pairs.
    /// </summary>
    public List<string> Pairs { get; } = [];

    /// <summary>
    /// Returns the current character and advances the cursor by <c>1</c>.
    /// </summary>
    /// <returns>The character.</returns>
    /// <exception cref="IndexOutOfRangeException">The cursor have ran out of range.</exception>
    public char Read()
    {
        return From[Cursor++];
    }

    /// <summary>
    /// Determines whether this instance can still read the next character.
    /// </summary>
    /// <param name="offset">The offset to determine.</param>
    /// <returns><see langword="true"/> if it is still readable; otherwise, <see langword="false"/>.</returns>
    public bool CanRead(int offset = 1)
    {
        return Cursor + offset <= From.Length;
    }

    /// <summary>
    /// Executes the parser to the end of the origin string.
    /// </summary>
    /// <remarks>
    /// This class is not reusable. After it finishes its execution, this instance must be dropped.
    /// </remarks>
    public void RunToEnd()
    {
        while (true)
        {
            if (!RunOnce())
            {
                break;
            }
        }
    }

    /// <summary>
    /// Executes the parser function once.
    /// </summary>
    /// <returns><see langword="true"/> if the parser can continue; otherwise, <see langword="false"/> means that the parser completed cleanup and should not be called.</returns>
    public bool RunOnce()
    {
        if (!CanRead())
        {
            OnEnd();
            return false;
        }

        var x = Read();
        
        switch (Brace)
        {
            case BraceState.Outside:
                RunOutsideBrace(x);
                break;
            case BraceState.First:
                RunInsideFirstBrace(x);
                break;
            case BraceState.Second:
                RunInsideSecondBrace(x);
                break;
        }

        return true;
    }

    private void RunOutsideBrace(char x)
    {
        // Attempt to exit brace while outside of braces.
        // Example: "}"
        if (x == RightBrace)
        {
            throw SelectorFormatException.ExitBraceWhileOutside(this);
        }

        if (x == Comma)
        {
            BreakPair();
            return;
        }

        if (InValue)
        {
            if (x == LeftBrace)
            {
                Brace = BraceState.First;

                // Append it here, since we do not need to check below.
                _builder.Append(x);
                return;
            }

            // Error: cannot specify equal sign if in value
            // Example: key=value=
            if (x == EqualSign)
            {
                throw SelectorFormatException.EqualSignWhileInValue(this);
            }
        }
        else
        {
            if (x == LeftBrace)
            {
                // Error: trying to specify braces in keys.
                // Example: key{}=value
                throw SelectorFormatException.BraceInvalidInKeys(this);
            }

            if (x == EqualSign)
            {
                // Enter value part of a pair.
                InValue = true;
            }
        }

        _builder.Append(x);
    }

    private void BreakPair()
    {
        Pairs.Add(_builder.ToString());
        InValue = false;
        _builder.Clear();
    }

    private void RunInsideFirstBrace(char x)
    {
        if (x == RightBrace)
        {
            Brace = BraceState.Outside;
        }

        if (x == LeftBrace)
        {
            Brace = BraceState.Second;
            _braceLevel = 1;
        }

        _builder.Append(x);
    }

    private void RunInsideSecondBrace(char x)
    {
        // Add additional brace.
        if (x == LeftBrace)
        {
            _braceLevel++;
        }

        if (x == RightBrace)
        {
            _braceLevel--;

            // If no more secondary braces, exit the second brace.
            if (_braceLevel == 0)
            {
                Brace = BraceState.First;
            }
        }

        _builder.Append(x);
    }

    private void OnEnd()
    {
        if (Brace != BraceState.Outside)
        {
            throw new SelectorFormatException("Brace never ends", "<unknown>");
        }

        BreakPair();
    }
}