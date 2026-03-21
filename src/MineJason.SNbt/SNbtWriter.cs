// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Globalization;
using JetBrains.Annotations;
using MineJason.SNbt.Codecs;
using MineJason.SNbt.Codecs.Primitives;
using MineJason.SNbt.Values;

namespace MineJason.SNbt;

/// <summary>
/// Writes string NBT values.
/// </summary>
public sealed class SNbtWriter : IDisposable
{
    private const char DoubleQuote = '"';
    private const char SingleQuote = '\'';
    private const char Colon = ':';
    private const char Comma = ',';
    private const char Semicolon = ';';

    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtWriter"/> class, with the default converter.
    /// </summary>
    /// <param name="target">The text writer to write to.</param>
    // ReSharper disable once IntroduceOptionalParameters.Global
    public SNbtWriter(TextWriter target) : this(target, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtWriter"/> class, with a custom converter.
    /// </summary>
    /// <param name="target">The text writer to write to.</param>
    /// <param name="converter">The converter to convert value with.</param>
    public SNbtWriter(TextWriter target, ISNbtConverter? converter)
    {
        _target = target;
        Converter = converter ?? PrimitiveSNbtConverter.Default;
    }
    
    private readonly TextWriter _target;
    private bool _writeComma;
    
    /// <summary>
    /// Gets the converter.
    /// </summary>
    public ISNbtConverter Converter { get; }

    /// <summary>
    /// Writes the specified character.
    /// </summary>
    /// <param name="value">The character to write.</param>
    internal void WriteChar(char value)
    {
        _target.Write(value);
    }

    private void WriteRawValue(string v)
    {
        _target.Write(v);
    }

    /// <summary>
    /// Writes a comma. The first call to this method is ignored.
    /// </summary>
    [PublicAPI]
    public void WriteComma()
    {
        if (!_writeComma)
        {
            _writeComma = true;
            return;
        }
        
        _target.Write(Comma);
    }

    /// <summary>
    /// Writes the beginning of a compound and resets the comma status.
    /// </summary>
    [PublicAPI]
    public void WriteBeginCompound()
    {
        _target.Write('{');
        _writeComma = false;
    }

    /// <summary>
    /// Writes the end of a compound. Subsequent calls to <see cref="WriteComma"/> writes a comma.
    /// </summary>
    [PublicAPI]
    public void WriteEndCompound()
    {
        _target.Write('}');
        _writeComma = true;
    }

    /// <summary>
    /// Writes the beginning of an array.
    /// </summary>
    /// <param name="specifier">The specifier.</param>
    public void WriteBeginArray(char specifier)
    {
        _target.Write('[');
        _target.Write(specifier);
        _target.Write(Semicolon);
        _writeComma = false;
    }

    /// <summary>
    /// Writes the beginning of a list and resets the comma status.
    /// </summary>
    public void WriteBeginList()
    {
        _target.Write('[');
        _writeComma = false;
    }

    /// <summary>
    /// Writes the end of a compound. Subsequent calls to <see cref="WriteComma"/> writes a comma.
    /// </summary>
    public void WriteEndList()
    {
        _target.Write(']');
        _writeComma = true;
    }

    /// <summary>
    /// Writes a key name and a semicolon as the beginning of a property. A comma is automatically added
    /// before the key name if required.
    /// </summary>
    /// <param name="key">The key name.</param>
    public void WriteKeyNameUnquoted(string key)
    {
        WriteComma();
        _target.Write(key);
        _target.Write(Colon);
    }

    /// <summary>
    /// Writes a quoted key name and a semicolon as the beginning of a property. A comma is automatically added
    /// before the key name if required.
    /// </summary>
    /// <param name="key">The key name.</param>
    public void WriteKeyNameQuoted(string key)
    {
        WriteComma();
        _target.Write(DoubleQuote);
        _target.Write(key);
        _target.Write(DoubleQuote);
        _target.Write(':');
    }

    /// <summary>
    /// Writes the invariable culture format value.
    /// </summary>
    /// <param name="value">The value to write.</param>
    internal void WriteFormattable(IFormattable value)
    {
        WriteRawValue(value.ToString(null, CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// Writes the invariable culture format value.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="suffix">The suffix of the value.</param>
    internal void WriteFormattable(IFormattable value, char suffix)
    {
        WriteFormattable(value);
        _target.Write(suffix);
    }

    #region Value Write Methods

    /// <summary>
    /// Writes the specified value via its associated codec.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <exception cref="ArgumentException">There were no codec associated with the specified value.</exception>
    public void WriteValue(object value)
    {
        Converter.WriteTo(value, this);
    }

    /// <summary>
    /// Writes a string value.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="singleQuote">If <see langword="true"/>, uses single quote.</param>
    [PublicAPI]
    [Obsolete("Use WriteStringValue instead.")]
    public void WriteValue(ReadOnlySpan<char> value, bool singleQuote)
        => WriteStringValue(value, singleQuote);
    
    /// <summary>
    /// Writes a string value.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="singleQuote">If <see langword="true"/>, uses single quote.</param>
    [PublicAPI]
    public void WriteStringValue(ReadOnlySpan<char> value, bool singleQuote = false)
    {
        var quote = singleQuote ? '\'' : '"';
        var escapedForm = singleQuote ? "\\'" : "\\\"";
        
        _target.Write(quote);
        foreach (var x in value)
        {
            if (x == '\\')
            {
                _target.Write(@"\\");
                continue;
            }

            if (x == quote)
            {
                _target.Write(escapedForm);
                continue;
            }
            
            _target.Write(x);
        }
        _target.Write(quote);
    }

    /// <summary>
    /// Writes a NBT byte value.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteValue(sbyte value)
    {
        WriteFormattable(value, SNbtByteValue.ValueSuffix);
    }

    /// <summary>
    /// Writes a NBT byte value representing the specified <see cref="bool"/> value.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteValue(bool value)
    {
        WriteValue(SNbtByteValue.GetValueFromBoolean(value));
    }

    /// <summary>
    /// Writes a NBT Integer value.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteValue(int value)
    {
        WriteFormattable(value);
    }
    
    /// <summary>
    /// Writes a NBT Long value.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteValue(long value)
    {
        WriteFormattable(value, SNbtLongValue.ValueSuffix);
    }
    
    /// <summary>
    /// Writes a NBT Float value.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteValue(float value)
    {
        WriteFormattable(value, SNbtFloatValue.ValueSuffix);
    }
    
    /// <summary>
    /// Writes a NBT Double value.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteValue(double value)
    {
        WriteFormattable(value, SNbtDoubleValue.ValueSuffix);
    }

    /// <summary>
    /// Writes a SNBT value.
    /// </summary>
    /// <param name="value">The value to write.</param>
    public void WriteValue(ISNbtWritable value)
    {
        value.WriteTo(this);
    }

    #endregion

    /// <summary>
    /// Writes a string property.
    /// </summary>
    /// <param name="key">The name of the key.</param>
    /// <param name="value">The value of the key.</param>
    /// <param name="singleQuote">If <see langword="true"/>, uses single quote.</param>
    [PublicAPI]
    public void WriteProperty(string key, ReadOnlySpan<char> value, bool singleQuote = false)
    {
        WriteKeyNameUnquoted(key);
        WriteStringValue(value, singleQuote);
    }
    
    /// <summary>
    /// Writes the specified property.
    /// </summary>
    /// <param name="key">The name of the key.</param>
    /// <param name="value">The value of the key.</param>
    [PublicAPI]
    public void WriteProperty(string key, ISNbtWritable value)
    {
        WriteKeyNameUnquoted(key);
        WriteValue(value);
    }
    
    /// <summary>
    /// Writes the specified property.
    /// </summary>
    /// <param name="key">The name of the key.</param>
    /// <param name="value">The value of the key.</param>
    [PublicAPI]
    public void WriteProperty(string key, object value)
    {
        WriteKeyNameUnquoted(key);
        WriteValue(value);
    }

    /// <summary>
    /// Writes the specified property value.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    internal void WritePropertyFormattable(string key, IFormattable value)
    {
        WriteKeyNameUnquoted(key);
        WriteFormattable(value);
    }

    #region Primitives Property Write
    /// <summary>
    /// Writes the specified property, with a NBT byte value.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public void WriteProperty(string key, sbyte value)
    {
        WriteKeyNameUnquoted(key);
        WriteFormattable(value, SNbtByteValue.ValueSuffix);
    }

    /// <summary>
    /// Writes the specified property, with a NBT byte value representing the specified
    /// <see cref="bool"/> value.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public void WriteProperty(string key, bool value)
    {
        WriteKeyNameUnquoted(key);
        WriteValue(value);
    }

    /// <summary>
    /// Writes the specified property, with a NBT integer value.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public void WriteProperty(string key, int value)
    {
        WriteKeyNameUnquoted(key);
        WriteValue(value);
    }
    
    /// <summary>
    /// Writes the specified property, with a NBT long value.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public void WriteProperty(string key, long value)
    {
        WriteKeyNameUnquoted(key);
        WriteValue(value);
    }
    
    /// <summary>
    /// Writes the specified property, with a NBT Float value.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public void WriteProperty(string key, float value)
    {
        WriteKeyNameUnquoted(key);
        WriteValue(value);
    }
    
    /// <summary>
    /// Writes the specified property, with a NBT Double value.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public void WriteProperty(string key, double value)
    {
        WriteKeyNameUnquoted(key);
        WriteValue(value);
    }

    #endregion

    /// <summary>
    /// Writes a property key followed by the beginning of a compound.
    /// </summary>
    /// <param name="key">The name of the property to write.</param>
    [PublicAPI]
    public void WritePropertyCompoundBegin(string key)
    {
        WriteKeyNameUnquoted(key);
        WriteBeginCompound();
    }
    
    /// <summary>
    /// Writes the specified property, with a NBT Double value, and with its key name quoted.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public void WritePropertyQuoted(string key, object value)
    {
        WriteKeyNameQuoted(key);
        WriteValue(value);
    }
    
    /// <summary>
    /// Writes the specified property, with a NBT Double value, and with its key name quoted.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public void WritePropertyQuoted(string key, ISNbtWritable value)
    {
        WriteKeyNameQuoted(key);
        WriteValue(value);
    }

    #region Standard Quoted Properties Write

    /// <summary>
    /// Writes a string property, and with its key name quoted.
    /// </summary>
    /// <param name="key">The name of the key.</param>
    /// <param name="value">The value of the key.</param>
    /// <param name="singleQuote">If <see langword="true"/>, uses single quote.</param>
    [PublicAPI]
    public void WritePropertyQuoted(string key, ReadOnlySpan<char> value, bool singleQuote = false)
    {
        WriteKeyNameQuoted(key);
        WriteStringValue(value, singleQuote);
    }

    /// <summary>
    /// Writes the specified property, with a NBT byte value, and with its key name quoted.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public void WritePropertyQuoted(string key, sbyte value)
    {
        WriteKeyNameQuoted(key);
        WriteFormattable(value, SNbtByteValue.ValueSuffix);
    }

    /// <summary>
    /// Writes the specified property, with a NBT byte value representing the specified
    /// <see cref="bool"/> value, and with its key name quoted.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public void WritePropertyQuoted(string key, bool value)
    {
        WriteKeyNameQuoted(key);
        WriteValue(value);
    }

    /// <summary>
    /// Writes the specified property, with a NBT integer value, and with its key name quoted.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public void WritePropertyQuoted(string key, int value)
    {
        WriteKeyNameQuoted(key);
        WriteValue(value);
    }
    
    /// <summary>
    /// Writes the specified property, with a NBT long value, and with its key name quoted.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public void WritePropertyQuoted(string key, long value)
    {
        WriteKeyNameQuoted(key);
        WriteValue(value);
    }
    
    /// <summary>
    /// Writes the specified property, with a NBT Float value, and with its key name quoted.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public void WritePropertyQuoted(string key, float value)
    {
        WriteKeyNameQuoted(key);
        WriteValue(value);
    }

    /// <summary>
    /// Writes the specified property, with a NBT Double value, and with its key name quoted.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [PublicAPI]
    public void WritePropertyQuoted(string key, double value)
    {
        WriteKeyNameQuoted(key);
        WriteValue(value);
    }
    #endregion

    #region Disposing

    /// <summary>
    /// Disposes the writer held by this instance.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
    }
    
    /// <summary>
    /// Disposes the writer held by this instance.
    /// </summary>
    /// <param name="disposing">If <see langword="true"/>, disposes the writer.</param>
    [PublicAPI]
    public void Dispose(bool disposing)
    {
        if (disposing)
        {
            _target.Dispose();
        }
    }

    #endregion
}