// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Codecs.Primitives;

[Obsolete("Use Serialization instead.")]
internal class PrimitiveSNbtConverter : ISNbtConverter
{
    internal static readonly PrimitiveSNbtConverter Default = new();

    public void WriteTo<T>(T value, SNbtWriter writer)
    {
        ArgumentNullException.ThrowIfNull(value);

        var valueType = typeof(T);
        var codecType = CodecHelper.GetAssociatedCodecType(valueType);

        var codec = CodecHelper.CreateCodec(codecType, valueType);
        CodecHelper.WriteViaCodec(value, codec, writer);
    }

    public void WriteTo(object value, SNbtWriter writer)
    {
        ArgumentNullException.ThrowIfNull(value);

        var valueType = value.GetType();
        var codecType = CodecHelper.GetAssociatedCodecType(valueType);

        var codec = CodecHelper.CreateCodec(codecType, valueType);
        CodecHelper.WriteViaCodec(value, codec, writer);
    }
}