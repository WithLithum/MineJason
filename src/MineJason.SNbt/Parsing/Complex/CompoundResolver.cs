// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Parsing.Complex;

using JetBrains.Annotations;
using MineJason.SNbt.Values;

/// <summary>
/// Resolves a complex compound property.
/// </summary>
[PublicAPI]
public class CompoundResolver
{
    private readonly List<string> _alreadyRead = [];
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CompoundResolver"/> class.
    /// </summary>
    /// <param name="reader">The reader to read from.</param>
    /// <param name="processProperty">The delegate that processes properties.</param>
    /// <param name="processCompound">The delegate that processes nested compounds.</param>
    public CompoundResolver(SNbtCompoundReader reader,
        ProcessPropertyDelegate? processProperty,
        ProcessCompoundPropertyDelegate? processCompound)
    {
        Reader = reader;

        ProcessProperty = processProperty;
        ProcessCompound = processCompound;
    }
    
    /// <summary>
    /// Gets the reader to read from.
    /// </summary>
    public SNbtCompoundReader Reader { get; }

    /// <summary>
    /// Defines a method that processes a property in compound.
    /// </summary>
    public delegate void ProcessPropertyDelegate(string propertyName, ISNbtWritable token);

    /// <summary>
    /// Defines a method that processes a nested compound property value in the compound.
    /// </summary>
    public delegate ISNbtWritable ProcessCompoundPropertyDelegate(string propertyName, SNbtBasicToken token);
    
    /// <summary>
    /// Gets or sets the delegate that processes properties.
    /// </summary>
    public ProcessPropertyDelegate? ProcessProperty { get; set; }
    
    /// <summary>
    /// Gets or sets the delegate that processes compounds.
    /// </summary>
    public ProcessCompoundPropertyDelegate? ProcessCompound { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to disallow properties being read twice or more times.
    /// </summary>
    public bool DisallowAlreadyRead { get; set; } = true;
    
    /// <summary>
    /// Parses a complex compound tag.
    /// </summary>
    public void ResolveCompound()
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

            if (DisallowAlreadyRead)
            {
                if (_alreadyRead.Contains(key))
                {
                    throw new FormatException($"Key {key} already exists.");
                }

                _alreadyRead.Add(key);
            }

            var valueToken = valueReader.ReadToken();
            var tagType = SNbtTagResolver.ResolvePrimitive(valueToken);
            ISNbtWritable? tag;
            
            // Process compounds if defined.
            if (tagType == SNbtTagType.Compound
                && ProcessCompound != null)
            {
                tag = ProcessCompound(key, valueToken);
            }

            tag = SNbtTagParser.Parse(tagType, valueToken);
            
            // Process properties.
            ProcessProperty?.Invoke(key, tag);
            Reader.Reader.Skip();
        }
    }
}