// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MineJason.SNbt.Parsing;

/// <summary>
/// Reads string NBT formats from a specified text reader.
/// </summary>
public class SNbtTextReader
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtTextReader"/> class.
    /// </summary>
    /// <param name="reader">The reader to read from.</param>
    public SNbtTextReader(TextReader reader)
    {
        Reader = reader;
    }
    
    /// <summary>
    /// Gets the reader associated with this instance.
    /// </summary>
    public TextReader Reader { get; }
    
    /// <summary>
    /// Gets the last character that was read.
    /// </summary>
    public char Last { get; private set; }

    /// <summary>
    /// Reads the next character, and advances the character position by 1.
    /// </summary>
    /// <returns>The character that was read.</returns>
    public int Read()
    {
        var result = Reader.Read();
        if (result >= 0)
        {
            Last = (char)result;
        }

        return result;
    }

    /// <summary>
    /// Returns the next available character without altering the character position or
    /// <see cref="Last"/> property.
    /// </summary>
    /// <returns>The next available character, or <c>-1</c> if no more characters are available.</returns>
    /// <seealso cref="PeekExpectChar"/>
    public int Peek()
    {
        return Reader.Peek();
    }

    /// <summary>
    /// Returns the next available character without altering the character position or
    /// <see cref="Last"/> property. Throws <see cref="FormatException"/> if the next character is not
    /// available.
    /// </summary>
    /// <returns>The next available character.</returns>
    /// <exception cref="FormatException">The next character is not available.</exception>
    /// <seealso cref="Peek"/>
    public char PeekExpectChar()
    {
        var read = Peek();

        if (read == -1)
        {
            SyntaxError("Expected character but got nothing");
        }

        return (char)read;
    }
    
    /// <summary>
    /// Reads the next character from the text reader and the character position by one character.
    /// Throws <see cref="FormatException"/> if unable to.
    /// </summary>
    /// <returns>The character that was read.</returns>
    public char ReadExpectChar()
    {
        var read = Read();

        if (read == -1)
        {
            SyntaxError("Expected character but got nothing");
        }

        return (char)read;
    }
    
    /// <summary>
    /// Reads the next character from the text reader and the character position by one character, and
    /// throws <see cref="FormatException"/> if unable to read or the character is different than expected.
    /// </summary>
    /// <returns>The character that was read.</returns>
    public void ReadExpectChar(char expected)
    {
        var ch = ReadExpectChar();

        if (ch != expected)
        {
            SyntaxError($"Expected character '{expected}' but got '{ch}'");
        }
    }
    
    /// <summary>
    /// Read characters from the text reader until the specified character was encountered. The
    /// character position will be right after the encounter.
    /// </summary>
    /// <param name="until">The character to stop reading at.</param>
    /// <param name="validate">A function that returns whether the character specified is valid. If <see langword="null"/>, characters are not validated.</param>
    /// <exception cref="FormatException">A disallowed character was encountered. -or- The reader was read to end without encountering the specified character.</exception>
    public string ReadUntil(char until, Func<char, bool>? validate = null)
    {
        var builder = new StringBuilder();
        
        while (true)
        {
            var ch = ReadExpectChar();

            if (ch == until)
            {
                break;
            }
            
            // User-defined validation.
            if (validate != null && !validate(ch))
            {
                SyntaxError($"Character '{ch}' is not allowed");
                continue;
            }

            builder.Append(ch);
        }

        return builder.ToString();
    }
    
    /// <summary>
    /// Read characters from the text reader until the specified condition is met.
    /// </summary>
    /// <param name="condition">A function returning if the character encountered should be the character to stop reading at.</param>
    /// <param name="validate">A function that returns whether the character specified is valid. If <see langword="null"/>, characters are not validated.</param>
    public string ReadUntil(Func<char, bool> condition, Func<char, bool>? validate = null)
    {
        var builder = new StringBuilder();
        
        while (true)
        {
            var ch = ReadExpectChar();

            if (condition(ch))
            {
                break;
            }
            
            // User-defined validation.
            if (validate != null && !validate(ch))
            {
                SyntaxError($"Character '{ch}' is not allowed");
                continue;
            }

            builder.Append(ch);
        }

        return builder.ToString();
    }
    
    [DoesNotReturn]
    private static void SyntaxError(string message)
    {
        throw new FormatException($"NBT syntax error: {message}");
    }

    /// <summary>
    /// Skips the next character.
    /// </summary>
    public void Skip()
    {
        Read();
    }

}