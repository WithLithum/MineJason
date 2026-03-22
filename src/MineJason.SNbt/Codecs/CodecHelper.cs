// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Reflection;
using MineJason.SNbt.Codecs.Primitives;

namespace MineJason.SNbt.Codecs;

internal static class CodecHelper
{
    internal static void WriteViaCodec(object value, object codec, SNbtWriter writer)
    {
        var codecType = codec.GetType();
        var writeMethod = codecType.GetMethod("Write", [value.GetType(), typeof(SNbtWriter)])
                          ?? throw new ArgumentException("Codec is invalid.", nameof(codec));

        writeMethod.Invoke(codec, [value, writer]);
    }

    internal static bool IsValidCodec(Type codecType, Type valueType)
    {
        var interfaceType = GetCodecInterfaceOfType(valueType);
        return codecType.GetInterfaces().Contains(interfaceType);
    }

    internal static object CreateCodec(Type codecType, Type valueType)
    {
        var interfaceType = GetCodecInterfaceOfType(valueType);

        if (!codecType.GetInterfaces().Contains(interfaceType))
        {
            throw new ArgumentException("Codec type is not a codec or not a codec for the specified type.",
                nameof(codecType));
        }

        var instance = Activator.CreateInstance(codecType);
        if (instance == null)
        {
            throw new InvalidOperationException("Failed to create Codec.");
        }
        return instance;
    }

    internal static Type GetAssociatedCodecType(Type type)
    {
        var attribute = type.GetCustomAttribute<SNbtCodecAttribute>();
        if (attribute is SNbtCodecAttribute codecAttribute)
        {
            return codecAttribute.CodecType;
        }

        if (!DefaultCodecs.TryGetCodec(type, out var codecType))
        {
            throw new ArgumentException("The specified type does not have am associated codec.", nameof(type));
        }

        return codecType!;
    }

    internal static Type GetCodecInterfaceOfType(Type valueType)
    {
        return typeof(ISNbtCodec<>).MakeGenericType(valueType);
    }
}