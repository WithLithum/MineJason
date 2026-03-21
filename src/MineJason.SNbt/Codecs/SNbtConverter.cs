// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Codecs;

/// <summary>
/// Converts objects and values to string NBT.
/// </summary>
public class SNbtConverter : ISNbtConverter
{
    private readonly Dictionary<Type, Type> _customCodecs = new();

    /// <summary>
    /// Registers a codec for the specified type.
    /// </summary>
    /// <param name="valueType">The type of the value to convert with the codec.</param>
    /// <param name="codecType">The type of the codec to convert with.</param>
    /// <exception cref="ArgumentException">The codec type does not match the type of the value to convert with.</exception>
    public void AddCodec(Type valueType, Type codecType)
    {
        if (!CodecHelper.IsValidCodec(codecType, valueType))
        {
            throw new ArgumentException("The specified codec is not a valid codec for the specified type.",
                nameof(valueType));
        }
        
        _customCodecs.Add(valueType, codecType);
    }

    /// <inheritdoc />
    public void WriteTo<T>(T value, SNbtWriter writer)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        Type? codecType = null;
        codecType = _customCodecs.TryGetValue(typeof(T), out var resultCodec)
            ? resultCodec
            : CodecHelper.GetAssociatedCodecType(typeof(T));

        var codec = CodecHelper.CreateCodec(codecType, typeof(T));
        CodecHelper.WriteViaCodec(value, codec, writer);
    }

    /// <inheritdoc />
    public void WriteTo(object value, SNbtWriter writer)
    {
        ArgumentNullException.ThrowIfNull(value);
        
        Type? codecType = null;
        var valueType = value.GetType();
        codecType = _customCodecs.TryGetValue(valueType, out var resultCodec)
            ? resultCodec
            : CodecHelper.GetAssociatedCodecType(valueType);

        var codec = CodecHelper.CreateCodec(codecType, valueType);
        CodecHelper.WriteViaCodec(value, codec, writer);
    }
}