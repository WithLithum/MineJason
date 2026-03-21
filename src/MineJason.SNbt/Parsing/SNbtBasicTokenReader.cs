// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Parsing;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

/// <summary>
/// Reads one or more NBT tokens.
/// </summary>
public class SNbtBasicTokenReader
{
    /// <summary>
    /// The escape character.
    /// </summary>
    public const char Escape = '\\';

    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtBasicTokenReader"/>.
    /// </summary>
    /// <param name="reader">The underlying text reader.</param>
    public SNbtBasicTokenReader(SNbtTextReader reader)
    {
        Reader = reader;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtBasicTokenReader"/>.
    /// </summary>
    /// <param name="reader">The underlying text reader.</param>
    public SNbtBasicTokenReader(TextReader reader) : this(new SNbtTextReader(reader))
    {
    }


    /// <summary>
    /// Gets the underlying text reader.
    /// </summary>
    public SNbtTextReader Reader { get; }

    /// <summary>
    /// Peeks the type of the current token.
    /// </summary>
    /// <returns>The type.</returns>
    public BasicTokenType PeekTokenType()
    {
        char peekChar = PeekExpectedChar();
        return BasicTokenResolver.Resolve(peekChar);
    }
    
    /// <summary>
    /// Reads the next token.
    /// </summary>
    /// <returns>The token.</returns>
    /// <exception cref="EndOfStreamException">Reached the end of stream.</exception>
    public SNbtBasicToken ReadToken()
    {
        char peekChar = PeekExpectedChar();
        var type = BasicTokenResolver.Resolve(peekChar);

        switch (type)
        {
            case BasicTokenType.DoubleQuotedString:
                return ReadDoubleQuotedStringToken();
            case BasicTokenType.SingleQuotedString:
                return ReadSingleQuotedStringToken();
            case BasicTokenType.UnquotedWord:
                return ReadUnquoteWord();
            case BasicTokenType.Compound:
                return new SNbtCompoundParser(new SNbtCompoundReader(Reader)).ReadBasicToken();
            case BasicTokenType.List:
                return ReadBasicList();
            default:
                SyntaxError("Unknown NBT token type");
                break;
        }

        throw new InvalidOperationException("How did we come to this far?");
    }

    private char PeekExpectedChar()
    {
        var peek = Reader.Peek();
        if (peek == -1)
        {
            SyntaxError("Expected token but got end of string");
        }

        var peekChar = (char)peek;
        return peekChar;
    }

    internal static bool IsAllowedInUnquotedWord(char ch)
    {
        return char.IsAsciiLetterOrDigit(ch)
               || ch == '_'
               || ch == '-'
               || ch == '.';
    }

    /// <summary>
    /// Reads a basic list as a token.
    /// </summary>
    /// <returns>The list read.</returns>
    public SNbtBasicToken ReadBasicList()
    {
        var builder = new StringBuilder();
        var nestLevel = 0;

        while (true)
        {
            if (nestLevel > 512)
            {
                SyntaxError("List may not be nested beyond the depth of 512");
            }

            var ch = Reader.ReadExpectChar();

            // Handle quoted strings.
            // Note that quoted string may only ever appear inside of lists, not outside of.
            if (nestLevel <= 0)
            {
                if (ch == SNbtConstants.DoubleQuote)
                {
                    builder.Append(ReadDoubleQuotedString());
                    continue;
                }

                if (ch == SNbtConstants.SingleQuote)
                {
                    builder.Append(ReadSingleQuotedString());
                    continue;
                }
            }

            // Handle end of list.
            if (ch == SNbtConstants.RightSquareBracket)
            {
                if (nestLevel <= 0)
                {
                    SyntaxError("End of list while outside of a list");
                }
                else if (nestLevel == 1)
                {
                    builder.Append(SNbtConstants.RightSquareBracket);
                    break;
                }
                else
                {
                    nestLevel--;
                }
            }

            if (ch == SNbtConstants.LeftSquareBracket)
            {
                nestLevel++;
            }

            builder.Append(ch);
        }

        return new SNbtBasicToken(BasicTokenType.List, builder.ToString());
    }

    /// <summary>
    /// Reads an unquoted word.
    /// </summary>
    /// <returns>The word to read.</returns>
    public SNbtBasicToken ReadUnquoteWord()
    {
        var builder = new StringBuilder();

        while (true)
        {
            var ch = Reader.Read();

            if (ch < 0)
            {
                break;
            }

            var readChar = (char)ch;

            if (!IsAllowedInUnquotedWord(readChar))
            {
                break;
            }

            builder.Append(readChar);
        }

        return new SNbtBasicToken(BasicTokenType.UnquotedWord, builder.ToString());
    }

    /// <summary>
    /// Reads the next quoted string, and advances the character position after the closing quote.
    /// </summary>
    /// <returns>The string that was read.</returns>
    public string ReadQuotedString()
    {
        char peekChar = PeekExpectedChar();
        return peekChar switch
        {
            '"' => ReadDoubleQuotedString(),
            '\'' => ReadSingleQuotedString(),
            _ => throw new FormatException($"NBT syntax error: Expected quoted string but got '{peekChar}'")
        };
    }

    /// <summary>
    /// Reads the next double-quoted string, and advances the character position after the closing quote.
    /// </summary>
    /// <returns>The string that was read.</returns>
    public SNbtBasicToken ReadDoubleQuotedStringToken()
    {
        return new SNbtBasicToken(BasicTokenType.DoubleQuotedString, ReadDoubleQuotedString());
    }

    /// <summary>
    /// Reads the next double-quoted string, and advances the character position after the closing quote.
    /// </summary>
    /// <returns>The string that was read.</returns>
    public string ReadDoubleQuotedString()
    {
        return InternalReadQuotedString('"');
    }

    /// <summary>
    /// Reads the next single-quoted string, and advances the character position after the closing quote.
    /// </summary>
    /// <returns>The string that was read.</returns>
    public SNbtBasicToken ReadSingleQuotedStringToken()
    {
        return new SNbtBasicToken(BasicTokenType.SingleQuotedString, ReadSingleQuotedString());
    }

    /// <summary>
    /// Reads the next double-quoted string, and advances the character position after the closing quote.
    /// </summary>
    /// <returns>The string that was read.</returns>
    public string ReadSingleQuotedString()
    {
        return InternalReadQuotedString('\'');
    }

    private string InternalReadQuotedString(char quoteChar)
    {
        if (Reader.Read() != quoteChar)
        {
            SyntaxError("Expected quoted string");
        }

        var escapeMode = false;
        var builder = new StringBuilder();

        while (true)
        {
            var ch = Reader.Read();

            if (ch < 0)
            {
                SyntaxError("Quoted string never ends");
            }

            var readChar = (char)ch;

            if (readChar == Escape)
            {
                escapeMode = true;
                continue;
            }

            // Escape mode handling logic
            if (escapeMode)
            {
                // Check if the character is allowed to be escaped.
                if (readChar != quoteChar && readChar != Escape)
                {
                    SyntaxError($"Invalid escape character '{quoteChar}'");
                }

                // Ignore the following code and append it as-is.
                builder.Append(readChar);
                continue;
            }

            // Non-escape-mode handling logic
            if (readChar == quoteChar)
            {
                break;
            }

            builder.Append(readChar);
        }

        return builder.ToString();
    }

    [DoesNotReturn]
    private static void SyntaxError(string message)
    {
        throw new FormatException($"NBT syntax error: {message}");
    }
}