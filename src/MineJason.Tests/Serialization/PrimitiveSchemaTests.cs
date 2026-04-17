// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json;
using MineJason.Serialization.IO.Json;
using MineJason.Serialization.Schema;
using MineJason.Serialization.Schema.Primitive;
using MineJason.Tests.Serialization.Utilities;

namespace MineJason.Tests.Serialization;

public class PrimitiveSchemaTests
{
    #region Boolean
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Boolean_EncodeValue_ReturnsCorrect(bool value)
    {
        // Arrange
        var schema = BooleanSchema.Instance;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode(value, encoder);

        // Assert
        Assert.Equal(value, ResultAssert.NotEmpty(result).GetValue<bool>());
    }

    [Fact]
    public void Boolean_DecodeTrue_ReturnsCorrect()
    {
        // Arrange
        var schema = BooleanSchema.Instance;

        using var json = JsonDocument.Parse("true");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        Assert.Null(result.Error);
        Assert.True(result.Value);
    }

    [Fact]
    public void Boolean_DecodeFalse_ReturnsCorrect()
    {
        // Arrange
        var schema = BooleanSchema.Instance;

        using var json = JsonDocument.Parse("false");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        Assert.False(ResultAssert.NotEmpty(result));
    }

    [Fact]
    public void Boolean_DecodeNotBoolean_ReturnsErr()
    {
        // Arrange
        var schema = BooleanSchema.Instance;

        using var json = JsonDocument.Parse("\"I AM NOT A BOOLEAN\"");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        ResultAssert.Failure(result);
    }
    #endregion

    #region Floating point numbers

    [Fact]
    public void Single_EncodeValue_ReturnsCorrect()
    {
        // Arrange
        const float value = 1234.56f;
        var schema = SingleValueSchema.Instance;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode(value, encoder);

        // Assert
        Assert.Equal("1234.56", ResultAssert.Success(result).ToJsonString());
    }

    [Fact]
    public void Double_EncodeValue_ReturnsCorrect()
    {
        // Arrange
        const double value = 1234.5678;
        var schema = DoubleValueSchema.Instance;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode(value, encoder);

        // Assert
        Assert.Equal("1234.5678", ResultAssert.Success(result).ToJsonString());
    }

    [Fact]
    public void Single_DecodeValue_ReturnsCorrect()
    {
        // Arrange
        var schema = SingleValueSchema.Instance;

        using var json = JsonDocument.Parse("3.14");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        Assert.Equal(3.14f, ResultAssert.Success(result));
    }

    [Fact]
    public void Double_DecodeValue_ReturnsCorrect()
    {
        // Arrange
        var schema = DoubleValueSchema.Instance;

        using var json = JsonDocument.Parse("3.1415926");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        Assert.Equal(3.1415926, ResultAssert.Success(result));
    }

    [Fact]
    public void Single_DecodeNotNumber_ReturnsErr()
    {
        // Arrange
        var schema = SingleValueSchema.Instance;

        using var json = JsonDocument.Parse("\"I am not a number.\"");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        ResultAssert.Failure(result);
    }

    [Fact]
    public void Double_DecodeNotNumber_ReturnsErr()
    {
        // Arrange
        var schema = DoubleValueSchema.Instance;

        using var json = JsonDocument.Parse("\"I am not a number.\"");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        ResultAssert.Failure(result);
    }

    #endregion

    #region Integers
    [Fact]
    public void Byte_EncodeValue_ReturnsCorrect()
    {
        // Arrange
        const byte value = 200;
        var schema = ByteValueSchema.Instance;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode(value, encoder);

        // Assert
        Assert.Equal($"{value}", ResultAssert.Success(result).ToJsonString());
    }
    
    [Fact]
    public void SByte_EncodeValue_ReturnsCorrect()
    {
        // Arrange
        const sbyte value = 100;
        var schema = SByteValueSchema.Instance;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode(value, encoder);

        // Assert
        Assert.Equal($"{value}", ResultAssert.Success(result).ToJsonString());
    }

    [Fact]
    public void Int32_EncodeValue_ReturnsCorrect()
    {
        // Arrange
        const int value = 12345678;
        var schema = Int32ValueSchema.Instance;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode(value, encoder);

        // Assert
        Assert.Equal($"{value}", ResultAssert.Success(result).ToJsonString());
    }

    [Fact]
    public void Int32_DecodeValue_ReturnsCorrect()
    {
        // Arrange
        const int value = 87654321;
        var schema = Int32ValueSchema.Instance;

        using var json = JsonDocument.Parse($"{value}");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        Assert.Equal(value, ResultAssert.Success(result));
    }

    [Fact]
    public void Int64_EncodeValue_ReturnsCorrect()
    {
        // Arrange
        const long value = 12345678;
        var schema = Int64ValueSchema.Instance;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode(value, encoder);

        // Assert
        Assert.Equal($"{value}", ResultAssert.Success(result).ToJsonString());
    }

    [Fact]
    public void Int64_DecodeValue_ReturnsCorrect()
    {
        // Arrange
        const long value = 87654321;
        var schema = Int64ValueSchema.Instance;

        using var json = JsonDocument.Parse($"{value}");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        Assert.Equal(value, ResultAssert.Success(result));
    }

    public static TheoryData<IValueSchema> GetIntegerSchemas()
    {
        return [
            ByteValueSchema.Instance,
            SByteValueSchema.Instance,
            Int16ValueSchema.Instance,
            Int32ValueSchema.Instance,
            Int64ValueSchema.Instance,
            UInt16ValueSchema.Instance,
            UInt32ValueSchema.Instance,
            UInt64ValueSchema.Instance,
        ];
    }

    [Theory]
    [MemberData(nameof(GetIntegerSchemas))]
    public void IntegerSchema_DecodeNotNumber_ReturnsErr(IValueSchema schema)
    {
        // Arrange
        using var json = JsonDocument.Parse("\"I am not a number.\"");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        ResultAssert.Failure(result);
    }
    #endregion

    #region Unsigned Integers

    [Fact]
    public void UInt16_EncodeValue_ReturnsCorrect()
    {
        // Arrange
        const ushort value = 54321;
        var schema = UInt16ValueSchema.Instance;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode(value, encoder);

        // Assert
        Assert.Equal($"{value}", ResultAssert.Success(result).ToJsonString());
    }

    [Fact]
    public void UInt16_DecodeValue_ReturnsCorrect()
    {
        // Arrange
        const ushort value = 54321;
        var schema = UInt16ValueSchema.Instance;

        using var json = JsonDocument.Parse($"{value}");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        Assert.Equal(value, ResultAssert.Success(result));
    }

    [Fact]
    public void UInt32_EncodeValue_ReturnsCorrect()
    {
        // Arrange
        const uint value = 4000000000;
        var schema = UInt32ValueSchema.Instance;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode(value, encoder);

        // Assert
        Assert.Equal($"{value}", ResultAssert.Success(result).ToJsonString());
    }

    [Fact]
    public void UInt32_DecodeValue_ReturnsCorrect()
    {
        // Arrange
        const uint value = 4000000000;
        var schema = UInt32ValueSchema.Instance;

        using var json = JsonDocument.Parse($"{value}");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        Assert.Equal(value, ResultAssert.Success(result));
    }

    [Fact]
    public void UInt64_EncodeValue_ReturnsCorrect()
    {
        // Arrange
        const ulong value = 18000000000000000000;
        var schema = UInt64ValueSchema.Instance;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode(value, encoder);

        // Assert
        Assert.Equal($"{value}", ResultAssert.Success(result).ToJsonString());
    }

    [Fact]
    public void UInt64_DecodeValue_ReturnsCorrect()
    {
        // Arrange
        const ulong value = 18000000000000000000;
        var schema = UInt64ValueSchema.Instance;

        using var json = JsonDocument.Parse($"{value}");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        Assert.Equal(value, ResultAssert.Success(result));
    }
    
    #endregion
    
    #region String
    [Fact]
    public void String_EncodeValue_ReturnsCorrect()
    {
        // Arrange
        var schema = StringValueSchema.Instance;
        var decoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode("Hello World!", decoder);

        // Assert
        Assert.Equal("\"Hello World!\"", result.Value?.ToJsonString());
    }

    [Fact]
    public void String_DecodeValue_ReturnsCorrect()
    {
        // Arrange
        var schema = StringValueSchema.Instance;
        var decoder = new JsonElementDecoder();

        var element = JsonDocument.Parse("\"Hello World!\"");

        // Act
        var result = schema.Decode(element.RootElement, decoder);

        // Assert
        Assert.Equal("Hello World!", result.Value);
    }

    [Fact]
    public void String_DecodeNotString_ReturnsErr()
    {
        // Arrange
        var schema = StringValueSchema.Instance;
        var decoder = new JsonElementDecoder();

        var element = JsonDocument.Parse("123");

        // Act
        var result = schema.Decode(element.RootElement, decoder);

        // Assert
        ResultAssert.Failure(result);
    }
    #endregion

    #region URI

    [Fact]
    public void Uri_EncodeValue_ReturnsCorrect()
    {
        // Arrange
        var uri = new Uri("https://contoso.com/");
        var schema = new UriSchema(UriKind.Absolute);
        var encoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode(uri, encoder);

        // Assert
        Assert.Equal("\"https://contoso.com/\"", ResultAssert.NotEmpty(result)
            .ToJsonString());
    }

    [Fact]
    public void Uri_DecodeValue_ReturnsCorrect()
    {
        // Arrange
        var schema = new UriSchema(UriKind.Absolute);

        using var json = JsonDocument.Parse("\"https://contoso.com/\"");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        Assert.Equal(new Uri("https://contoso.com/"), ResultAssert.NotEmpty(result));
    }

    [Fact]
    public void Uri_DecodeNotString_ReturnsErr()
    {
        // Arrange
        var schema = new UriSchema(UriKind.Absolute);

        using var json = JsonDocument.Parse("321");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        ResultAssert.Failure(result);
    }

    [Fact]
    public void Uri_DecodeInvalidUri_ReturnsErr()
    {
        // Arrange
        var schema = new UriSchema(UriKind.Absolute);

        using var json = JsonDocument.Parse("\"I_HAVE_NO_GREETING://////////\"");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        ResultAssert.Failure(result);
    }

    #endregion
}
