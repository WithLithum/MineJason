// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json;
using System.Text.Json.Nodes;
using MineJason.Serialization.IO.Json;
using MineJason.Tests.Serialization.Utilities;

namespace MineJason.Tests.Serialization.IO;

public class JsonEncodeTests
{
    [Fact]
    public void Encoder_Boolean_ReturnsCorrect()
    {
        // Arrange
        const bool value = true;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = encoder.CreateBoolean(value);

        // Assert
        Assert.Equal(value, result.GetValue<bool>());
    }

    [Fact]
    public void Encoder_Byte_ReturnsCorrect()
    {
        // Arrange
        const byte value = 0x20;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = encoder.CreateByte(value);

        // Assert
        Assert.Equal(value, result.GetValue<byte>());
    }

    [Fact]
    public void Encoder_SByte_ReturnsCorrect()
    {
        // Arrange
        const sbyte value = -15;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = encoder.CreateSByte(value);

        // Assert
        Assert.Equal(value, result.GetValue<sbyte>());
    }

    [Fact]
    public void Encoder_UInt16_ReturnsCorrect()
    {
        // Arrange
        const ushort value = 60000;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = encoder.CreateUInt16(value);

        // Assert
        Assert.Equal(value, result.GetValue<ushort>());
    }

    [Fact]
    public void Encoder_Int16_ReturnsCorrect()
    {
        // Arrange
        const short value = -10000;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = encoder.CreateInt16(value);

        // Assert
        Assert.Equal(value, result.GetValue<short>());
    }

    [Fact]
    public void Encoder_UInt32_ReturnsCorrect()
    {
        // Arrange
        const uint value = 60000;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = encoder.CreateUInt32(value);

        // Assert
        Assert.Equal(value, result.GetValue<uint>());
    }

    [Fact]
    public void Encoder_Int32_ReturnsCorrect()
    {
        // Arrange
        const int value = -10000;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = encoder.CreateInt32(value);

        // Assert
        Assert.Equal(value, result.GetValue<int>());
    }

    [Fact]
    public void Encoder_UInt64_ReturnsCorrect()
    {
        // Arrange
        const ulong value = 60000;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = encoder.CreateUInt64(value);

        // Assert
        Assert.Equal(value, result.GetValue<ulong>());
    }

    [Fact]
    public void Encoder_Int64_ReturnsCorrect()
    {
        // Arrange
        const long value = -60000L;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = encoder.CreateInt64(value);

        // Assert
        Assert.Equal(value, result.GetValue<long>());
    }

    [Fact]
    public void Encoder_Single_ReturnsCorrect()
    {
        // Arrange
        const float value = 50F;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = encoder.CreateSingle(value);

        // Assert
        Assert.Equal(value, result.GetValue<float>());
    }

    [Fact]
    public void Encoder_Double_ReturnsCorrectly()
    {
        // Arrange
        const double value = 10000.25;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = encoder.CreateDouble(value);

        // Assert
        Assert.Equal(value, result.GetValue<double>());
    }

    [Fact]
    public void Encoder_Object_ReturnsCorrectly()
    {
        // Arrange
        var encoder = new JsonNodeEncoder();
        var obj = encoder.CreateObjectLike();
        
        // Act
        var addFirstResult = obj.Add("First", encoder.CreateInt32(1));
        var addSecondResult = obj.Add("Second", encoder.CreateInt32(2));
        var result = obj.GetContainer();
        
        // Assert
        Assert.Multiple(() => ResultAssert.Success(addFirstResult),
            () => ResultAssert.Success(addSecondResult),
            () => Assert.Equal("{\"First\":1,\"Second\":2}", result.ToJsonString()));
    }
    
    [Fact]
    public void Encoder_Object_EnumeratesCorrectly()
    {
        // Arrange
        var obj = new JsonObject()
        {
            { "First", JsonValue.Create("1") },
            { "Second", JsonValue.Create("2") }
        };
        var adapter = new JsonObjectAdapter(obj);

        // Act
        var enumerable = adapter.EnumerateObject();

        // Assert
        Assert.Collection(enumerable,
            x1 => Assert.Multiple(
                () => Assert.Equal("First", x1.Key),
                () => Assert.Equal("1", x1.Value.GetValue<string>())),
            x2 => Assert.Multiple(
                () => Assert.Equal("Second", x2.Key),
                () => Assert.Equal("2", x2.Value.GetValue<string>()))
        );
    }

    [Fact]
    public void Decoder_Boolean_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("true");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetBoolean(document.RootElement);
        
        // Assert
        Assert.True(ResultAssert.Success(result));
    }
    
    [Fact]
    public void Decoder_SByte_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("123");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetSByte(document.RootElement);
        
        // Assert
        Assert.Equal(123, ResultAssert.Success(result));
    }
    
    [Fact]
    public void Decoder_Byte_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("234");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetByte(document.RootElement);
        
        // Assert
        Assert.Equal(234, ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_UInt16_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("60000");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetUInt16(document.RootElement);
        
        // Assert
        Assert.Equal((ushort)60000, ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_Int16_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("-10000");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetInt16(document.RootElement);
        
        // Assert
        Assert.Equal((short)-10000, ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_UInt32_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("60000");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetUInt32(document.RootElement);
        
        // Assert
        Assert.Equal((uint)60000, ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_Int32_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("-10000");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetInt32(document.RootElement);
        
        // Assert
        Assert.Equal(-10000, ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_UInt64_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("60000");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetUInt64(document.RootElement);
        
        // Assert
        Assert.Equal((ulong)60000, ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_Int64_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("-60000");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetInt64(document.RootElement);
        
        // Assert
        Assert.Equal(-60000L, ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_Single_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("50.5");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetSingle(document.RootElement);
        
        // Assert
        Assert.Equal(50.5F, ResultAssert.Success(result));
    }

    [Fact]
    public void Decoder_Double_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("10000.25");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetDouble(document.RootElement);
        
        // Assert
        Assert.Equal(10000.25, ResultAssert.Success(result));
    }
    
    [Fact]
    public void Decoder_String_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("\"I am a string\"");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetString(document.RootElement);
        
        // Assert
        Assert.Equal("I am a string", ResultAssert.Success(result));
    }
    
    [Fact]
    public void Decoder_ByteArray_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("[100,101,102]");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetByteArray(document.RootElement);
        
        // Assert
        Assert.Collection(ResultAssert.Success(result),
            x1 => Assert.Equal(100, x1),
            x2 => Assert.Equal(101, x2),
            x3 => Assert.Equal(102, x3)
        );
    }
    
    [Fact]
    public void Decoder_Int32Array_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("[12,34,56]");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetInt32Array(document.RootElement);
        
        // Assert
        Assert.Collection(ResultAssert.Success(result),
            x1 => Assert.Equal(12, x1),
            x2 => Assert.Equal(34, x2),
            x3 => Assert.Equal(56, x3)
        );
    }
    
    [Fact]
    public void Decoder_Int64Array_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("[1234,5678,9101]");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetInt64Array(document.RootElement);
        
        // Assert
        Assert.Collection(ResultAssert.Success(result),
            x1 => Assert.Equal(1234L, x1),
            x2 => Assert.Equal(5678L, x2),
            x3 => Assert.Equal(9101L, x3)
        );
    }
    
    [Fact]
    public void Decoder_Collection_ReturnsCorrect()
    {
        // Arrange
        using var document = JsonDocument.Parse("[\"One\",\"Two\",\"Three\"]");
        var decoder = new JsonElementDecoder();
        
        // Act
        var result = decoder.GetCollection(document.RootElement);
        
        // Assert
        Assert.Collection(ResultAssert.Success(result).Select(x => x.GetString()),
            x1 => Assert.Equal("One", x1),
            x2 => Assert.Equal("Two", x2),
            x3 => Assert.Equal("Three", x3)
        );
    }
    
    [Fact]
    public void Decoder_Object_EnumeratesCorrectly()
    {
        // Arrange
        var obj = JsonDocument.Parse("{\"first\":1,\"second\":2}");
        var adapter = new JsonElementObjectAdapter(obj.RootElement);

        // Act
        var enumerable = adapter.EnumerateObject();

        // Assert
        Assert.Collection(enumerable,
            x1 => Assert.Multiple(
                () => Assert.Equal("first", x1.Key),
                () => Assert.Equal(1, x1.Value.GetInt32())),
            x2 => Assert.Multiple(
                () => Assert.Equal("second", x2.Key),
                () => Assert.Equal(2, x2.Value.GetInt32()))
        );
    }
}