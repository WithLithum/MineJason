// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt;
using MineJason.SNbt.Values;

namespace MineJason.Tests.NBT;

public class CollectionTests
{
    [Fact]
    public void ByteArray_ToSNbtString()
    {
        var array = new SNbtByteArray { 110, 101, 11 };

        Assert.Equal("[B;110b,101b,11b]", array.ToSNbtString());
    }
    
    [Fact]
    public void IntArray_ToSNbtString()
    {
        var array = new SNbtIntArray { 225, 221, 115 };

        Assert.Equal("[I;225,221,115]", array.ToSNbtString());
    }
    
    [Fact]
    public void LongArray_ToSNbtString()
    {
        var array = new SNbtLongArray { 222, 888, 555 };

        Assert.Equal("[L;222L,888L,555L]", array.ToSNbtString());
    }
    
    [Fact]
    public void List_ToSNbtString()
    {
        var array = new SNbtListValue<SNbtStringValue> { new("Grass"), new("Field"), new("Okay") };

        Assert.Equal("['Grass','Field','Okay']", array.ToSNbtString());
    }
}