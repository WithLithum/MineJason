// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Codecs;

/// <summary>
/// Specifies a codec for the specified type.
/// </summary>
[AttributeUsage(AttributeTargets.Class
                | AttributeTargets.Struct
                | AttributeTargets.Delegate
                | AttributeTargets.Enum 
                | AttributeTargets.Interface, AllowMultiple = false)]
public class SNbtCodecAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SNbtCodecAttribute"/> class.
    /// </summary>
    /// <param name="codecType">The codec to associate.</param>
    public SNbtCodecAttribute(Type codecType)
    {
        CodecType = codecType;
    }
    
    /// <summary>
    /// Gets or sets the associated codec.
    /// </summary>
    public Type CodecType { get; }
}