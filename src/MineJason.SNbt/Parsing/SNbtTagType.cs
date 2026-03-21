// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.SNbt.Parsing;

/// <summary>
/// An enumeration of all types of tag that can be represented in string NBT.
/// </summary>
public enum SNbtTagType
{
    /// <summary>
    /// The tag type is unsupported.
    /// </summary>
    None,
    /// <summary>
    /// A <c>TAG_Compound</c>.
    /// </summary>
    Compound,
    /// <summary>
    /// A <c>TAG_Int</c>.
    /// </summary>
    Int,
    /// <summary>
    /// A <c>TAG_Double</c>.
    /// </summary>
    Double,
    /// <summary>
    /// A <c>TAG_Long</c>.
    /// </summary>
    Long,
    /// <summary>
    /// A <c>TAG_Float</c>.
    /// </summary>
    Float,
    /// <summary>
    /// A <c>TAG_Byte</c>.
    /// </summary>
    Byte,
    /// <summary>
    /// A <c>TAG_Int_Array</c>.
    /// </summary>
    IntArray,
    /// <summary>
    /// A <c>TAG_Long_Array</c>.
    /// </summary>
    LongArray,
    /// <summary>
    /// A <c>TAG_String</c>.
    /// </summary>
    String,
    /// <summary>
    /// A <c>TAG_List</c>.
    /// </summary>
    List,
    /// <summary>
    /// A <c>TAG_Byte_Array</c>.
    /// </summary>
    ByteArray
}