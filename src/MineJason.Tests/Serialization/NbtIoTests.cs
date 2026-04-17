// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using fNbt;
using MineJason.Serialization.fNbt;
using MineJason.Tests.Serialization.Utilities;

namespace MineJason.Tests.Serialization;

public class NbtIoTests
{
    [Fact]
    public void Encoder_Boolean_ProducesByteTag()
    {
        // Arrange
        var encoder = new NbtTagEncoder();

        // Act
        var result = encoder.CreateBoolean(true);

        // Assert
        Assert.IsType<NbtByte>(result);
        Assert.Equal(1, result.ByteValue);
    }

    [Fact]
    public void Encoder_Byte_ProducesByteTag()
    {
        // Arrange
        var encoder = new NbtTagEncoder();

        // Act
        var result = encoder.CreateByte(0x20);

        // Assert
        Assert.IsType<NbtByte>(result);
        Assert.Equal(0x20, result.ByteValue);
    }

    [Fact]
    public void Encoder_Int16_ProducesShortTag()
    {
        // Arrange
        var encoder = new NbtTagEncoder();

        // Act
        var result = encoder.CreateInt16(10000);

        // Assert
        Assert.IsType<NbtShort>(result);
        Assert.Equal(10000, result.ShortValue);
    }

    [Fact]
    public void Encoder_Int32_ProducesIntTag()
    {
        // Arrange
        var encoder = new NbtTagEncoder();

        // Act
        var result = encoder.CreateInt32(100);

        // Assert
        Assert.IsType<NbtInt>(result);
        Assert.Equal(100, result.IntValue);
    }

    [Fact]
    public void Encoder_Int64_ProducesLongTag()
    {
        // Arrange
        var encoder = new NbtTagEncoder();

        // Act
        var result = encoder.CreateInt64(30000L);

        // Assert
        Assert.IsType<NbtLong>(result);
        Assert.Equal(30000L, result.LongValue);
    }

    [Fact]
    public void Encoder_ByteArray_ProducesByteArrayTag()
    {
        // Arrange
        var encoder = new NbtTagEncoder();

        // Act
        var array = encoder.CreateByteArray();
        _ = array.Add(63);
        _ = array.Add(127);
        _ = array.Add(255);
        var result = array.GetContainer();

        // Assert
        var tag = Assert.IsType<NbtByteArray>(result);
        Assert.Collection(tag.Value,
            x => Assert.Equal(63, x),
            x => Assert.Equal(127, x),
            x => Assert.Equal(255, x));
    }

    [Fact]
    public void Encoder_Int32Array_ProducesIntArrayTag()
    {
        // Arrange
        var encoder = new NbtTagEncoder();

        // Act
        var array = encoder.CreateInt32Array();
        _ = array.Add(6000);
        _ = array.Add(7000);
        _ = array.Add(8000);
        var result = array.GetContainer();

        // Assert
        var tag = Assert.IsType<NbtIntArray>(result);
        Assert.Collection(tag.Value,
            x => Assert.Equal(6000, x),
            x => Assert.Equal(7000, x),
            x => Assert.Equal(8000, x));
    }

    [Fact]
    public void Encoder_Int64Array_ProducesLongArrayTag()
    {
        // Arrange
        var encoder = new NbtTagEncoder();

        // Act
        var array = encoder.CreateInt64Array();
        _ = array.Add(1000L);
        _ = array.Add(2000L);
        _ = array.Add(3000L);
        var result = array.GetContainer();

        // Assert
        var tag = Assert.IsType<NbtLongArray>(result);
        Assert.Collection(tag.Value,
            x => Assert.Equal(1000L, x),
            x => Assert.Equal(2000L, x),
            x => Assert.Equal(3000L, x));
    }

    [Fact]
    public void Encoder_String_ProducesStringTag()
    {
        // Arrange
        var encoder = new NbtTagEncoder();

        // Act
        var result = encoder.CreateString("Hello World!");

        // Assert
        Assert.IsType<NbtString>(result);
        Assert.Equal("Hello World!", result.StringValue);
    }

    [Fact]
    public void CompoundAdapter_Enumerate_ReturnCorrectly()
    {
        // Arrange
        var compound = new NbtCompound
        {
            new NbtString("First", "1"),
            new NbtString("Second", "2")
        };
        var adapter = new NbtCompoundAdapter(compound);

        // Act
        var enumerable = adapter.EnumerateObject();

        // Assert
        Assert.Collection(enumerable,
            x1 => Assert.Multiple(
                () => Assert.Equal("First", x1.Key),
                () => Assert.Equal("1", x1.Value.StringValue)),
            x2 => Assert.Multiple(
                () => Assert.Equal("Second", x2.Key),
                () => Assert.Equal("2", x2.Value.StringValue)));
    }

    [Fact]
    public void ListAdapter_InsertItems_ReturnCorrect()
    {
        // Arrange
        var list = new NbtList(NbtTagType.String);
        var adapter = new NbtListAdapter(list);
        
        // Act
        _ = adapter.Add(new NbtString(null, "1"));
        var result = adapter.GetContainer();

        // Assert
        var nbtList = Assert.IsType<NbtList>(result);
        Assert.Equal("1", Assert.IsType<NbtString>(Assert.Single(nbtList)).Value);
    }
    
    [Fact]
    public void ListAdapter_InsertNamedItem_ReturnError()
    {
        // Arrange
        var adapter = new NbtListAdapter(new NbtList(NbtTagType.String));
        
        // Act
        var result = adapter.Add(new NbtString("Named", "1"));

        // Assert
        ResultAssert.Failure(result);
    }
}