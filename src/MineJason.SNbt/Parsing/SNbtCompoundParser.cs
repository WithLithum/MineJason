// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Parsing;

using System.Diagnostics;
using System.Text;
using MineJason.NBT.Values;
using MineJason.SNbt.Values;

/// <summary>
/// Parses a compound of primitive values.
/// </summary>
public class SNbtCompoundParser
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtCompoundParser"/> class.
    /// </summary>
    /// <param name="reader">The reader to read compound from.</param>
    public SNbtCompoundParser(SNbtCompoundReader reader)
    {
        Reader = reader;
    }
    
    /// <summary>
    /// Gets the underlying reader of this instance.
    /// </summary>
    public SNbtCompoundReader Reader { get; }

    /// <summary>
    /// Parses the compound tag at current position either as a full compound or a heterogeneous
    /// list value.
    /// </summary>
    /// <returns>
    /// A tuple containing either a <see cref="SNbtCompound"/> or a <see cref="HListValue"/>. If
    /// <see cref="SNbtCompound"/> is not <see langword="null"/>, then <see cref="HListValue"/> is
    /// <see langword="default"/>, and vice versa.
    /// </returns>
    /// <exception cref="FormatException"></exception>
    public (SNbtCompound?, HListValue) ReadCompoundOrHListValue()
    {
        Reader.ReadBeginCompound();

        // Possess a heterogeneous value and a full compound. The full compound will only be
        // created only when one is being read.
        HListValue hval = default;
        SNbtCompound? compound = null;
        bool isHetero = false;

        var firstRead = false;
        var readHetero = false;
        var valueReader = new SNbtBasicTokenReader(Reader.Reader);

        while (true)
        {
            if (Reader.IsCompoundEnd()) break;

            if (firstRead)
            {
                // Dunno what happened but caret misses the comma if the first value was with an
                // empty key, so skip one character here.
                if (compound == null) Reader.Reader.Skip();

                if (!Reader.IsNextTag())
                {
                    throw new FormatException($"Syntax error: Invalid following sequence: found {(char)Reader.Reader.Peek()}");
                }
            }

            // Read tag.
            var key = Reader.ReadTagKey();

            // If the current value is first value and has an empty key, then parse one single
            // heterogeneous value.
            //
            // Otherwise, if one heterogeneous value already exists, creates a new full compound
            // and copy existing value into it.
            if (key == string.Empty && !firstRead)
            {
                readHetero = true;
                isHetero = true;
            }
            else if (compound == null)
            {
                compound = [];

                if (isHetero)
                {
                    isHetero = false;
                    compound.Add(string.Empty, hval.Value);
                }
            }

            firstRead = true;

            var valueToken = valueReader.ReadToken();
            var tagType = SNbtTagResolver.ResolvePrimitive(valueToken);

            var tag = SNbtTagParser.Parse(tagType, valueToken);

            if (readHetero)
            {
                hval = new HListValue(tag);
                readHetero = false;
            }
            else
            {
                compound ??= new SNbtCompound();
                compound.Add(key, tag);
            }
        }

        // Ensure that if compound is not null, then the HListValue is always default.
        return compound == null ? (null, hval) : (compound, default);
    }

    /// <summary>
    /// Parses a complex compound tag.
    /// </summary>
    public SNbtCompound ReadCompound()
    {
        Reader.ReadBeginCompound();

        var compound = new SNbtCompound();
        var firstRead = false;
        var valueReader = new SNbtBasicTokenReader(Reader.Reader);

        while (true)
        {
            if (Reader.IsCompoundEnd()) break;

            if (firstRead)
            {
                if (!Reader.IsNextTag())
                {
                    throw new FormatException($"Syntax error: Invalid following sequence: found {(char)Reader.Reader.Peek()}");
                }
            }

            firstRead = true;

            // Read tag.
            var key = Reader.ReadTagKey();

            var valueToken = valueReader.ReadToken();
            var tagType = SNbtTagResolver.ResolvePrimitive(valueToken);

            var tag = SNbtTagParser.Parse(tagType, valueToken);
            compound.Add(key, tag);
        }
        
        return compound;
    }

    /// <summary>
    /// Parses a compound tag to <see cref="SNbtBasicToken"/>.
    /// </summary>
    public SNbtBasicToken ReadBasicToken()
    {
        Reader.ReadBeginCompound();
        
        var firstRead = false;
        var valueReader = new SNbtBasicTokenReader(Reader.Reader);
        var builder = new StringBuilder();
        builder.Append(SNbtConstants.LeftCurveBracket);
        
        while (true)
        {
            if (Reader.IsCompoundEnd()) break;

            if (firstRead)
            {
                if (!Reader.IsNextTag())
                {
                    Debug.WriteLine(builder.ToString());
                    throw new FormatException($"Syntax error: Invalid following sequence: found {(char)Reader.Reader.Peek()}");
                }

                builder.Append(SNbtConstants.Comma);
            }

            firstRead = true;
            
            // Read tag.
            var key = Reader.ReadTagKey();
            var value = valueReader.ReadToken();

            builder.Append(key);
            builder.Append(SNbtConstants.Colon);
            builder.Append(value.Value);
        }

        builder.Append(SNbtConstants.RightCurveBracket);
        return new SNbtBasicToken(BasicTokenType.Compound, builder.ToString());
    }
}