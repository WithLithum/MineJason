// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Parsing;

using MineJason.NBT.Values;
using MineJason.SNbt.Values;
using System.Collections.Generic;

/// <summary>
/// Parses a <c>TAG_List</c>.
/// </summary>
public class SNbtListParser
{
    private ref struct ParseState
    {
        internal ParseState(SNbtBasicTokenReader tokenReader)
        {
            TokenReader = tokenReader;
        }

        internal int NestDepth;
        internal bool ExpectingComma;
        internal SNbtTagType CurrentTagType;
        internal ISNbtWritable? CurrentTag;

        internal readonly SNbtBasicTokenReader TokenReader;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtListParser"/> class.
    /// </summary>
    /// <param name="reader">The reader to read from.</param>
    public SNbtListParser(TextReader reader) : this(new SNbtTextReader(reader))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtListParser"/> class.
    /// </summary>
    /// <param name="reader">The reader to read from.</param>
    public SNbtListParser(SNbtTextReader reader)
    {
        Reader = reader;
    }

    /// <summary>
    /// Gets the underlying reader of this instance.
    /// </summary>
    public SNbtTextReader Reader { get; }

    #region Helpers & Common logic

    private void ReadTagInternal(ref ParseState s)
    {
        var type = BasicTokenResolver.Resolve(Reader.PeekExpectChar());

        var basicToken = type switch
        {
            BasicTokenType.DoubleQuotedString => ReadDoubleQuotedStringToken(s.TokenReader),
            BasicTokenType.SingleQuotedString => ReadSingleQuotedStringToken(s.TokenReader),
            BasicTokenType.Compound => new SNbtCompoundParser(new SNbtCompoundReader(Reader)).ReadBasicToken(),
            BasicTokenType.List => ReadNestedBasicList(ref s),
            BasicTokenType.UnquotedWord => s.TokenReader.ReadToken(),
            _ => throw new NotSupportedException(),
        };

        s.CurrentTagType = SNbtTagResolver.ResolvePrimitive(basicToken);
        s.CurrentTag = SNbtTagParser.Parse(s.CurrentTagType, basicToken);
    }

    private void ReadBeginList()
    {
        Reader.ReadExpectChar(SNbtConstants.LeftSquareBracket);
    }

    private static SNbtBasicToken ReadNestedBasicList(ref ParseState s)
    {
        s.NestDepth++;
        return s.TokenReader.ReadBasicList();
    }

    private static ListTag ToHeterogeneous(ListTag? original)
    {
        if (original == null)
        {
            return new ListTag(SNbtTagType.Compound);
        }

        var newList = new ListTag(SNbtTagType.Compound, new List<ISNbtWritable>(original.Count));
        foreach (var t in original)
        {
            newList.Add(new HListValue(t));
        }

        return newList;
    }

    private static SNbtBasicToken ReadSingleQuotedStringToken(SNbtBasicTokenReader reader)
    {
        var result = reader.ReadSingleQuotedStringToken();
        reader.Reader.Skip();
        return result;
    }

    private static SNbtBasicToken ReadDoubleQuotedStringToken(SNbtBasicTokenReader reader)
    {
        var result = reader.ReadDoubleQuotedStringToken();
        reader.Reader.Skip();
        return result;
    }

    #endregion

    /// <summary>
    /// Reads the next list value as a heterogeneous list, regardless of if it were actually 
    /// heterogeneous or not.
    /// </summary>
    /// <returns>The list tag.</returns>
    public ListTag ReadHeterogeneous()
    {
        ReadBeginList();

        var s = new ParseState(new SNbtBasicTokenReader(Reader));
        var list = new ListTag(SNbtTagType.Compound);

        while (true)
        {
            if (Reader.Last == SNbtConstants.RightSquareBracket)
            {
                if (s.NestDepth <= 0)
                {
                    break;
                }
                else
                {
                    s.NestDepth--;
                    Reader.Skip();
                    continue;
                }
            }

            ReadTagInternal(ref s);
            list.Add(new HListValue(s.CurrentTag!));
        }

        return list;
    }

    /// <summary>
    /// Reads a primitive value.
    /// </summary>
    /// <returns>The read value.</returns>
    /// <exception cref="FormatException">The format of the value is invalid.</exception>
    /// <exception cref="NotSupportedException">The kind of the value is not supported.</exception>
    public ISNbtList? ReadPrimitive()
    {
        ReadBeginList();

        // heterogeneous and homogeneous lists.
        ListTag? list = null;

        var homoType = SNbtTagType.None;

        var s = new ParseState(new SNbtBasicTokenReader(Reader));

        // This variable indicates whether we are returning a heterogeneous list.
        var heterogeneous = false;

        while (true)
        {
            // End of list, abort
            if (Reader.Last == SNbtConstants.RightSquareBracket)
            {
                if (s.NestDepth <= 0)
                {
                    break;
                }
                else
                {
                    s.NestDepth--;
                    Reader.Skip();
                    continue;
                }
            }

            if (s.ExpectingComma && Reader.Last != SNbtConstants.Comma)
            {
                throw new FormatException("Expected comma or end of list");
            }

            s.ExpectingComma = true;

            // Evaluate basic token type.
            ReadTagInternal(ref s);

            // Process heterogeneous tags
            if (heterogeneous)
            {
                if (list?.ItemType != SNbtTagType.Compound)
                {
                    list = ToHeterogeneous(list);
                }

                list.Add(new HListValue(s.CurrentTag!));
            }
            else
            {
                // Evaluate tag type.
                // If tag type is not set yet, set tag type
                // If tag type is set, check if tag type matches
                if (homoType == SNbtTagType.None)
                {
                    homoType = s.CurrentTagType;

                    // Construct the generic list type
                    list = new ListTag(s.CurrentTagType);
                }
                else if (!heterogeneous && s.CurrentTagType != homoType)
                {
                    heterogeneous = true;
                    list = ToHeterogeneous(list);
                    list.Add(new HListValue(s.CurrentTag!));

                    continue;
                }

                list ??= new ListTag(s.CurrentTagType);
                list.Add(s.CurrentTag!);
            }
        }

        return list;
    }
}
