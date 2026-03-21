// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using JetBrains.Annotations;
using MineJason.SNbt.Values;

namespace MineJason.SNbt.Parsing;

/// <summary>
/// Parses a string NBT array.
/// </summary>
public class SNbtArrayParser
{
    /// <summary>
    /// Left square bracket.
    /// </summary>
    [PublicAPI]
    public const char LeftSquareBracket = '[';
    
    /// <summary>
    /// Right square bracket.
    /// </summary>
    [PublicAPI]
    public const char RightSquareBracket = ']';
    
    /// <summary>
    /// Semicolon.
    /// </summary>
    [PublicAPI]
    public const char Semicolon = ';';
    
    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtArrayParser"/> class.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="type">The type of the array.</param>
    public SNbtArrayParser(TextReader reader, SNbtTagType type) : this(new SNbtTextReader(reader), type)
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtArrayParser"/> class.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="type">The type of the array.</param>
    /// <exception cref="ArgumentException">The specified type is not an array type.</exception>
    [PublicAPI]
    public SNbtArrayParser(SNbtTextReader reader, SNbtTagType type)
    {
        Reader = reader;

        if (type != SNbtTagType.IntArray
            && type != SNbtTagType.LongArray
            && type != SNbtTagType.ByteArray)
        {
            throw new ArgumentException("Only int or long array can be parsed.",
                nameof(type));
        }

        Type = type;
    }
    
    /// <summary>
    /// Gets the underlying reader associated with this instance.
    /// </summary>
    [PublicAPI]
    public SNbtTextReader Reader { get; }
    
    /// <summary>
    /// Gets the type of the array.
    /// </summary>
    [PublicAPI]
    public SNbtTagType Type { get; }

    /// <summary>
    /// Gets the identifying character for the associated array type.
    /// </summary>
    /// <returns>The identifying character.</returns>
    /// <exception cref="InvalidOperationException">The tag type specified in property <see cref="Type"/> is not an array type.</exception>
    [PublicAPI]
    public char GetSignChar()
    {
        // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
        // Justification: All other arms are explicitly prohibited.
        return Type switch
        {
            SNbtTagType.IntArray => 'I',
            SNbtTagType.LongArray => 'L',
            SNbtTagType.ByteArray => 'B',
            _ => throw new InvalidOperationException($"Tag type {Type} is not an array type.")
        };
    }
    
    /// <summary>
    /// Reads and validates the beginning of an array.
    /// </summary>
    [PublicAPI]
    public void ReadBeginning()
    {
        Reader.ReadExpectChar(LeftSquareBracket);
        Reader.ReadExpectChar(GetSignChar());
        Reader.ReadExpectChar(Semicolon);
    }
    
    /// <summary>
    /// Parses a <c>TAG_Int_Array</c> or <c>TAG_Long_Array</c>.
    /// </summary>
    /// <returns>The parsed value.</returns>
    /// <exception cref="InvalidOperationException">The tag type specified in property <see cref="Type"/> is not an array type.</exception>
    public ISNbtWritable ParseArray()
    {
        ReadBeginning();

        // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
        // Justification: All other arms are explicitly prohibited.
        return Type switch
        {
            SNbtTagType.IntArray => ParseIntArray(),
            SNbtTagType.LongArray => ParseLongArray(),
            SNbtTagType.ByteArray => ParseByteArray(),
            _ => throw new InvalidOperationException($"Tag type {Type} is not an array type.")
        };
    }

    private SNbtByteArray ParseByteArray()
    {
        var first = false;
        var array = new SNbtByteArray();

        while (true)
        {
            // If second or later elements
            if (first && Reader.Last != ',')
            {
                throw new FormatException("Expected end of array or comma");
            }

            first = true;
            var value = Reader.ReadUntil(x => x is ',' or RightSquareBracket,
                x => char.IsAsciiLetterOrDigit(x) || x == '-');

            // Literal boolean values.
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (value == "true")
            {
                array.Add(1);
                continue;
            }
            
            if (value == "false")
            {
                array.Add(0);
                continue;
            }
            
            // Byte values.
            // Get the suffix and validate
            var suffix = value[^1];
            if (suffix != 'b' && suffix != 'B')
            {
                throw new FormatException($"Invalid suffix for TAG_Byte; found {suffix}");
            }
            
            // Parse everything except the suffix
            var intValue = sbyte.Parse(value[..^1]);
            
            array.Add(intValue);
            
            if (Reader.Last == ']')
            {
                break;
            }
        }

        return array;
    }

    private SNbtIntArray ParseIntArray()
    {
        var first = false;
        var array = new SNbtIntArray();

        while (true)
        {
            // If second or later elements
            if (first)
            {
                if (Reader.Last == ']')
                {
                    break;
                }

                if (Reader.Last != ',')
                {
                    throw new FormatException($"Expected end of array or comma, but is {Reader.Last}");   
                }
            }

            first = true;
            var value = Reader.ReadUntil(x => x is ',' or RightSquareBracket,
                x => char.IsAsciiDigit(x) || x == '-');
            var intValue = int.Parse(value);
            
            array.Add(intValue);
        }

        return array;
    }
    
    private SNbtLongArray ParseLongArray()
    {
        var first = false;
        var array = new SNbtLongArray();

        while (true)
        {
            // If second or later elements
            if (first && Reader.Last != ',')
            {
                throw new FormatException("Expected end of array or comma");
            }

            first = true;
            var value = Reader.ReadUntil(x => x is ',' or RightSquareBracket,
                x => char.IsAsciiDigit(x)
                || x == 'l'
                || x == 'L'
                || x == '-');

            // Get the suffix and validate
            var suffix = value[^1];
            if (suffix != 'l' && suffix != 'L')
            {
                throw new FormatException($"Invalid suffix for TAG_Long; found {suffix}");
            }
            
            // Parse everything except the suffix
            var intValue = long.Parse(value[..^1]);
            
            array.Add(intValue);
            
            if (Reader.Last == ']')
            {
                break;
            }
        }

        return array;
    }
}