// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using fNbt;
using MineJason.Serialization.fNbt;
using MineJason.Tests.Serialization.Utilities;

namespace MineJason.Tests.Serialization;

public class NbtIoTests
{
    #region Encoder tests

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
    public void Encoder_Single_ProducesFloatTag()
    {
        // Arrange
        var encoder = new NbtTagEncoder();

        // Act
        var result = encoder.CreateSingle(12.3f);

        // Assert
        Assert.Equal(12.3f, Assert.IsType<NbtFloat>(result).Value);
    }

    [Fact]
    public void Encoder_Double_ProducesDoubleTag()
    {
        // Arrange
        var encoder = new NbtTagEncoder();

        // Act
        var result = encoder.CreateDouble(12.345d);

        // Assert
        Assert.Equal(12.345d, Assert.IsType<NbtDouble>(result).Value);
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

    #endregion Encoder tests

    #region Decoder tests

    [Fact]
    public void Decoder_DecodeBooleanByte_ReturnsBoolean()
    {
        // Arrange
        var tag = new NbtByte(null, 1);
        var decoder = new NbtTagDecoder();

        // Act
        var result = decoder.GetBoolean(tag);

        // Assert
        Assert.True(ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_DecodeNumberByte_ReturnsByte()
    {
        // Arrange
        var tag = new NbtByte(null, 100);
        var decoder = new NbtTagDecoder();

        // Act
        var result = decoder.GetByte(tag);

        // Assert
        Assert.Equal(100, ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_DecodeShort_ReturnsInt16()
    {
        // Arrange
        var tag = new NbtShort(null, 25565);
        var decoder = new NbtTagDecoder();

        // Act
        var result = decoder.GetInt16(tag);

        // Assert
        Assert.Equal(25565, ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_DecodeInt_ReturnsInt32()
    {
        // Arrange
        var tag = new NbtInt(null, 1000000);
        var decoder = new NbtTagDecoder();

        // Act
        var result = decoder.GetInt32(tag);

        // Assert
        Assert.Equal(1000000, ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_DecodeLong_ReturnsInt64()
    {
        // Arrange
        var tag = new NbtLong(null, 168000000L);
        var decoder = new NbtTagDecoder();

        // Act
        var result = decoder.GetInt64(tag);

        // Assert
        Assert.Equal(168000000L, ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_DecodeFloat_ReturnsSingle()
    {
        // Arrange
        var tag = new NbtFloat(null, 127.0f);
        var decoder = new NbtTagDecoder();

        // Act
        var result = decoder.GetSingle(tag);

        // Assert
        Assert.Equal(127.0f, ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_DecodeDouble_ReturnsDouble()
    {
        // Arrange
        var tag = new NbtDouble(null, 127.122d);
        var decoder = new NbtTagDecoder();

        // Act
        var result = decoder.GetDouble(tag);

        // Assert
        Assert.Equal(127.122d, ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_DecodeString_ReturnsString()
    {
        // Arrange
        var tag = new NbtString(null, "Hello World!");
        var decoder = new NbtTagDecoder();

        // Act
        var result = decoder.GetString(tag);

        // Assert
        Assert.Equal("Hello World!", ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_DecodeByteArray_ReturnsInt16Array()
    {
        // Arrange
        var tag = new NbtByteArray(null, [1, 2, 3]);
        var decoder = new NbtTagDecoder();

        // Act
        var result = decoder.GetByteArray(tag);

        // Assert
        Assert.Collection(ResultAssert.Success(result),
            a1 => Assert.Equal(1, a1),
            a2 => Assert.Equal(2, a2),
            a3 => Assert.Equal(3, a3));
    }

    [Fact]
    public void Decoder_DecodeIntArray_ReturnsInt32Array()
    {
        // Arrange
        var tag = new NbtIntArray(null, [12, 34, 56]);
        var decoder = new NbtTagDecoder();

        // Act
        var result = decoder.GetInt32Array(tag);

        // Assert
        Assert.Collection(ResultAssert.Success(result),
            a1 => Assert.Equal(12, a1),
            a2 => Assert.Equal(34, a2),
            a3 => Assert.Equal(56, a3));
    }

    [Fact]
    public void Decoder_DecodeLongArray_ReturnsInt64Array()
    {
        // Arrange
        var tag = new NbtLongArray(null, [1234L, 5678L]);
        var decoder = new NbtTagDecoder();

        // Act
        var result = decoder.GetInt64Array(tag);

        // Assert
        Assert.Collection(ResultAssert.Success(result),
            a1 => Assert.Equal(1234L, a1),
            a2 => Assert.Equal(5678L, a2));
    }

    [Fact]
    public void Decoder_DecodeList_ReturnsCollection()
    {
        // Arrange
        var tag = new NbtList(null, [
            new NbtString("One"),
            new NbtString("Two")
        ], NbtTagType.String);
        var decoder = new NbtTagDecoder();

        // Act
        var result = decoder.GetCollection(tag);

        // Assert
        Assert.Collection(ResultAssert.Success(result),
            a1 => Assert.Equal("One", Assert.IsType<NbtString>(a1).Value),
            a2 => Assert.Equal("Two", Assert.IsType<NbtString>(a2).Value));
    }

    [Fact]
    public void Decoder_DecodeCompound_ReturnsObject()
    {
        // Arrange
        var tag = new NbtCompound(null,
        [
            new NbtString("One", "1"),
            new NbtString("Two", "2")
        ]);
        var decoder = new NbtTagDecoder();

        // Act
        var result = decoder.GetObjectLike(tag);

        // Assert
        var obj = ResultAssert.Success(result);
        Assert.Multiple(
            () => Assert.Equal("1", Assert.IsType<NbtString>(ResultAssert.Success(
                obj.Get("One")))
            .Value),
            () => Assert.Equal("2", Assert.IsType<NbtString>(ResultAssert.Success(
                obj.Get("Two")))
            .Value));
    }

    #endregion Decoder tests

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
    public void CompoundContainsKey_Existing_ReturnTrue()
    {
        // Arrange
        var compound = new NbtCompound
        {
            new NbtString("First", "1")
        };
        var adapter = new NbtCompoundAdapter(compound);

        // Act
        var result = adapter.ContainsKey("First");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CompoundContainsKey_NonExisting_ReturnFalse()
    {
        // Arrange
        var compound = new NbtCompound();
        var adapter = new NbtCompoundAdapter(compound);

        // Act
        var result = adapter.ContainsKey("Third");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CompoundAdd_NewKey_Succeed()
    {
        // Arrange
        var compound = new NbtCompound();
        var adapter = new NbtCompoundAdapter(compound);

        // Act
        var result = adapter.Add("Value", new NbtString("Value", "3"));

        // Assert
        ResultAssert.Success(result);
    }

    [Fact]
    public void CompoundAdd_ExistingKey_Error()
    {
        // Arrange
        var compound = new NbtCompound
        {
            new NbtString("First", "1"),
        };
        var adapter = new NbtCompoundAdapter(compound);

        // Act
        var result = adapter.Add("First", new NbtString("First", ""));

        // Assert
        ResultAssert.Failure(result);
    }

    [Fact]
    public void CompoundAdd_MismatchedKey_Error()
    {
        // Arrange
        var compound = new NbtCompound();
        var adapter = new NbtCompoundAdapter(compound);

        // Act
        var result = adapter.Add("First", new NbtString("Second", ""));

        // Assert
        ResultAssert.Failure(result);
    }

    [Fact]
    public void CompoundGet_ValidKey_Succeed()
    {
        // Arrange
        var compound = new NbtCompound
        {
            new NbtString("First", "1"),
        };
        var adapter = new NbtCompoundAdapter(compound);

        // Act
        var result = adapter.Get("First");

        // Assert
        ResultAssert.Success(result);
    }

    [Fact]
    public void CompoundGet_InvalidKey_Error()
    {
        // Arrange
        var compound = new NbtCompound();
        var adapter = new NbtCompoundAdapter(compound);

        // Act
        var result = adapter.Get("Second");

        // Assert
        ResultAssert.Failure(result);
    }

    [Fact]
    public void Compound_GetContainer_ReturnSameInstance()
    {
        // Arrange
        var compound = new NbtCompound();
        var adapter = new NbtCompoundAdapter(compound);

        // Act
        var result = adapter.GetContainer();

        // Assert
        Assert.Same(result, compound);
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

    [Fact]
    public void ListAdapter_InsertIncorrectType_ReturnError()
    {
        // Arrange
        var adapter = new NbtListAdapter(new NbtList(NbtTagType.String));

        // Act
        var result = adapter.Add(new NbtFloat(null, 1f));

        // Assert
        ResultAssert.Failure(result);
    }
}