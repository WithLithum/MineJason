// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Compiler;

using MineJason.Utilities;

/// <summary>
/// Writes string NBT data.
/// </summary>
public class NbtDataWriter : IDisposable
{
    /// <summary>
    /// Initialises a new instance of the <see cref="NbtDataWriter"/> class.
    /// </summary>
    /// <param name="writer">The writer to write to.</param>
    public NbtDataWriter(TextWriter writer)
    {
        _writer = writer;
    }
    
    private readonly TextWriter _writer;
    private bool _first;

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes this instance.
    /// </summary>
    /// <param name="disposing">If <see langword="true"/>, disposes held resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _writer.Dispose();
        }
    }

    ~NbtDataWriter()
    {
        Dispose(false);
    }

    /// <summary>
    /// Closes this instance.
    /// </summary>
    public void Close()
    {
        _writer.Close();
    }

    /// <summary>
    /// Writes the beginning of an NBT compound.
    /// </summary>
    public void WriteBeginCompound()
    {
        _writer.Write('{');
        _first = false;
    }

    /// <summary>
    /// Writes the end of an NBT compound.
    /// </summary>
    public void WriteEndCompound()
    {
        _writer.Write('}');
    }

    /// <summary>
    /// Writes a property key.
    /// </summary>
    /// <param name="key">The key to write.</param>
    public void WriteKey(string key)
    {
        _writer.Write($"{key}:");
    }
    
    /// <summary>
    /// Writes an NBT property.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public void WriteRawProperty(string key, string value)
    {
        WriteComma();
        WriteKey(key);
        _writer.Write(value);
    }

    /// <summary>
    /// Writes a single-quoted string NBT property. The value will be escaped.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public void WriteProperty(string key, string value)
    {
        WriteRawProperty(key, $"'{SpecificValueUtil.EscapeNbtString(value)}'");
    }

    /// <summary>
    /// Writes a chat component as a quoted string property.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void WriteProperty(string key, ChatComponent value)
    {
        var strValue = SpecificValueUtil.ToEscapedComponentString(value);
        
        WriteRawProperty(key, strValue);
    }

    /// <summary>
    /// Writes a begin of a compound property.
    /// </summary>
    /// <param name="key">The key of the property.</param>
    public void WritePropertyCompoundBegin(string key)
    {
        WriteComma();
        WriteKey(key);
        WriteBeginCompound();
    }

    /// <summary>
    /// Writes the end of a compound property.
    /// </summary>
    public void WritePropertyCompoundEnd()
    {
        _first = true;
        WriteEndCompound();
    }

    private void WriteComma()
    {
        if (!_first)
        {
            _first = true;
        }
        else
        {
            _writer.Write(',');
        }
    }
}