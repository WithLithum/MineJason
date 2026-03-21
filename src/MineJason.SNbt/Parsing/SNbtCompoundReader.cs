// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MineJason.SNbt.Parsing;

/// <summary>
/// Reads the elements of a compound.s
/// </summary>
// ReSharper disable once InconsistentNaming
public class SNbtCompoundReader
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtCompoundReader"/> class.
    /// </summary>
    /// <param name="reader">The reader.</param>
    public SNbtCompoundReader(SNbtTextReader reader)
    {
        Reader = reader;
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtCompoundReader"/> class.
    /// </summary>
    /// <param name="reader">The reader.</param>
    public SNbtCompoundReader(TextReader reader) : this(new SNbtTextReader(reader))
    {
    }
    
    /// <summary>
    /// Gets the underlying text reader.
    /// </summary>
    public SNbtTextReader Reader { get; }

    /// <summary>
    /// Reads the next character from the text reader and the character position by one character.
    /// Throws <see cref="FormatException"/> if unable to.
    /// </summary>
    /// <returns>The character that was read.</returns>
    public char ReadExpectChar()
    {
        var read = Reader.Read();

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
    /// Reads the next character from the text reader and the character position by one character, and
    /// throws <see cref="FormatException"/> if unable to read or the character is not the beginning of a compound.
    /// </summary>
    /// <returns>The character that was read.</returns>
    public void ReadBeginCompound()
    {
        ReadExpectChar(SNbtConstants.LeftCurveBracket);
    }

    public string ReadTagKey()
    {
        return Reader.Peek() is '"' or '\''
            ? ReadQuotedTagKey()
            : ReadUnquotedTagKey();
    }
    
    /// <summary>
    /// Reads the next unquoted tag key from the text reader. The character position will be put after the
    /// colon.
    /// </summary>
    /// <returns>The tag key.</returns>
    public string ReadUnquotedTagKey()
    {
        return ReadUntil(SNbtConstants.Colon, x => char.IsAsciiLetterOrDigit(x)
            || x == '_'
            || x == '-'
            || x == '.'
            || x == '+');
    }

    /// <summary>
    /// Reads the next quoted tag key from the text reader. The character position will be put after the
    /// colon.
    /// </summary>
    /// <returns>The tag key.</returns>
    public string ReadQuotedTagKey()
    {
        var tokenReader = new SNbtBasicTokenReader(Reader);
        var result = tokenReader.ReadQuotedString();
        ReadExpectChar(':');

        return result;
    }
    
    /// <summary>
    /// Read characters from the text reader until the specified character was encountered.
    /// </summary>
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
    /// Determines whether the character position is located just before a comma indicating the next tag.
    /// </summary>
    /// <returns><see langword="true"/> if the character position is located just before a comma indicating the next tag;
    /// otherwise <see langword="false"/>.</returns>
    public bool IsNextTag()
    {
        return Reader.Last == SNbtConstants.Comma;
    }
    
    /// <summary>
    /// Determines whether the character position is located just before the closing brace
    /// of a compound.
    /// </summary>
    /// <returns><see langword="true"/> if the character position is located just before the closing brace of a compound;
    /// otherwise <see langword="false"/>.</returns>
    public bool IsCompoundEnd()
    {
        return Reader.Last == SNbtConstants.RightCurveBracket;
    }
    
    [DoesNotReturn]
    private static void SyntaxError(string message)
    {
        throw new FormatException($"Compound syntax error: {message}");
    }
}