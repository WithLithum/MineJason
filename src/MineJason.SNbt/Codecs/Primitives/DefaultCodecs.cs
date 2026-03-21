// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Codecs.Primitives;

internal static class DefaultCodecs
{
    private static readonly Dictionary<Type, Type> Codecs = new()
    {
        { typeof(int), typeof(Int32Codec) },
        { typeof(long), typeof(Int64Codec) },
        { typeof(bool), typeof(BooleanCodec) },
        { typeof(sbyte), typeof(SByteCodec) },
        { typeof(string), typeof(StringCodec) },
        { typeof(double), typeof(DoubleCodec) },
        { typeof(float), typeof(SingleCodec) },
        { typeof(Guid), typeof(GuidCodec) }
    };

    public static bool TryGetCodec(Type valueType, out Type? codecType)
    {
        return Codecs.TryGetValue(valueType, out codecType);
    }
}