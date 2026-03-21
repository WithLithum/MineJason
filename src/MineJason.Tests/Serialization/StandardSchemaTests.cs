// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Serialization.IO;
using MineJason.Serialization.IO.Json;
using MineJason.Serialization.Schema;
using MineJason.Serialization.Schema.Primitive;
using MineJason.Serialization.Utilities.Results;
using MineJason.Tests.Serialization.Utilities;
using Moq;
using System.Text.Json;
using StringGuidSchema = MineJason.Serialization.Schema.Primitive.StringGuidSchema;

namespace MineJason.Tests.Serialization;

public class StandardSchemaTests
{
    #region Mock tyeps

    private class A;
    private class B : A;

    #endregion

    [Fact]
    public void ValueSchema_InterfaceEncodeWithName_PassNameSuccessfully()
    {
        // Arrange
        var mock = new Mock<ValueSchema<int>>();

        // Act
        ((IValueSchema)mock.Object).Encode(12, Mock.Of<IValueEncoder<string>>(),
            "elementName");

        // Assert
        mock.Verify(x => x.Encode(12,
                It.IsAny<IValueEncoder<string>>(),
                "elementName"),
            Times.Once);
    }

    [Fact]
    public void ValueSchemaNonGenericInterface_PassedValueOtherThanSpecifiedType_Throws()
    {
        // Arrange
        var mock = new Mock<ValueSchema<int>>();

        // Act
        var exception = Record.Exception(() =>
        ((IValueSchema)mock.Object).Encode("NOT AN INT",
            Mock.Of<IValueEncoder<string>>()));

        // Assert
        Assert.IsType<ArgumentException>(exception);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Boolean_Value_EncodesSuccessfully(bool value)
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
    public void Boolean_True_DecodesSuccessfully()
    {
        // Arrange
        var schema = BooleanSchema.Instance;

        var json = JsonDocument.Parse("true");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        Assert.Null(result.Error);
        Assert.True(result.Value);
    }

    [Fact]
    public void Boolean_False_DecodesSuccessfully()
    {
        // Arrange
        var schema = BooleanSchema.Instance;

        var json = JsonDocument.Parse("false");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        Assert.False(ResultAssert.NotEmpty(result));
    }

    [Fact]
    public void Uri_CorrectForming_EncodesSuccessfully()
    {
        // Arrange
        var uri = new Uri("https://contoso.com/");
        var schema = UriSchema.Instance;
        var encoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode(uri, encoder);

        // Assert
        Assert.Equal("\"https://contoso.com/\"", ResultAssert.NotEmpty(result)
            .ToJsonString());
    }

    [Fact]
    public void Uri_CorrectForming_DecodesSuccessfully()
    {
        // Arrange
        var schema = UriSchema.Instance;

        var json = JsonDocument.Parse("\"https://contoso.com/\"");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        Assert.Equal(new Uri("https://contoso.com/"), ResultAssert.NotEmpty(result));
    }

    [Fact]
    public void StringEnum_NoNamingPolicy_EncodesSuccessfully()
    {
        // Arrange
        var schema = new StringEnumValueSchema<AttributeTargets>();
        var encoder = new JsonNodeEncoder();

        const AttributeTargets value = AttributeTargets.Struct;

        // Act
        var result = schema.Encode(value, encoder);

        // Assert
        Assert.Equal("\"Struct\"", ResultAssert.NotEmpty(result).ToJsonString());
    }

    [Fact]
    public void StringEnum_SnakeCaseNamingPolicy_EncodesSuccessfully()
    {
        // Arrange
        var schema = new StringEnumValueSchema<AttributeTargets>(
            JsonNamingPolicy.SnakeCaseLower);
        var encoder = new JsonNodeEncoder();

        const AttributeTargets value = AttributeTargets.ReturnValue;

        // Act
        var result = schema.Encode(value, encoder);

        // Assert
        Assert.Equal("\"return_value\"", ResultAssert.NotEmpty(result)
            .ToJsonString());
    }

    [Fact]
    public void StringEnum_NoNamingPolicy_DecodesSuccessfully()
    {
        // Arrange
        var schema = new StringEnumValueSchema<AttributeTargets>();

        var json = JsonDocument.Parse("\"Property\"");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        Assert.Null(result.Error);
        Assert.Equal(AttributeTargets.Property, result.Value);
    }

    [Fact]
    public void StringEnum_SnakeCaseNamingPolicy_DecodesSuccessfully()
    {
        // Arrange
        var schema = new StringEnumValueSchema<AttributeTargets>(
            JsonNamingPolicy.SnakeCaseLower);

        var json = JsonDocument.Parse("\"generic_parameter\"");
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(json.RootElement, decoder);

        // Assert
        Assert.Null(result.Error);
        Assert.Equal(AttributeTargets.GenericParameter, result.Value);
    }

    [Fact]
    public void Guid_Encode_ConvertsCorrectly()
    {
        // Arrange
        var guid = new Guid("e101bfbe-1621-43e0-b05e-d847cd9ddf21");
        var schema = new StringGuidSchema("D", true);
        var encoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode(guid, encoder);

        // Assert
        Assert.Equal($"\"{guid:D}\"", ResultAssert.NotEmpty(result).ToJsonString());
    }

    [Fact]
    public void GuidParseExact_Conforming_DecodeSucceed()
    {
        // Arrange
        var schema = new StringGuidSchema("D", true);
        var decoder = new JsonElementDecoder();

        var element = JsonDocument.Parse("\"a574a5fb-5a17-49eb-88a4-70dae3f8b8b0\"");

        // Act
        var result = schema.Decode(element.RootElement, decoder);

        // Assert
        Assert.Equal(new Guid("a574a5fb-5a17-49eb-88a4-70dae3f8b8b0"),
            ResultAssert.NotEmpty(result));
    }

    [Fact]
    public void GuidParseExact_NonConforming_DecodeFail()
    {
        // Arrange
        var schema = new StringGuidSchema("D", true);
        var decoder = new JsonElementDecoder();

        var element = JsonDocument.Parse("\"{a574a5fb-5a17-49eb-88a4-70dae3f8b8b0}\"");

        // Act
        var result = schema.Decode(element.RootElement, decoder);

        // Assert
        ResultAssert.Failure(result);
    }

    [Fact]
    public void CollectionSchema_StandardCollection_EncodeSucceed()
    {
        // Arrange
        var schema = new CollectionSchema<string>(StringValueSchema.Instance);
        var encoder = new JsonNodeEncoder();
        var list = new List<string>
        {
            "One",
            "Two"
        };

        // Act
        var result = schema.Encode(list, encoder);

        // Assert
        Assert.Equal("[\"One\",\"Two\"]",
            ResultAssert.NotEmpty(result).ToJsonString());
    }

    [Fact]
    public void CollectionSchema_StandardCollection_DecodeSucceed()
    {
        // Arrange
        var schema = new CollectionSchema<string>(
            StringValueSchema.Instance);
        var decoder = new JsonElementDecoder();

        var element = JsonDocument.Parse("[\"Hello\",\"World\"]");

        // Act
        var result = schema.Decode(element.RootElement, decoder);

        // Assert
        Assert.Collection(ResultAssert.NotEmpty(result),
            x1 => Assert.Equal("Hello", x1),
            x2 => Assert.Equal("World", x2));
    }

    [Fact]
    public void StringSchema_StringValue_EncodeSucceed()
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
    public void StringSchema_StringValue_DecodeSucceed()
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
    public void ExtensionResultEncode_SuccessfulHasValue_EncodeSucceed()
    {
        // Arrange
        var schema = StringValueSchema.Instance;
        var decoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode(Result.Success(value: "Hello World!"), decoder);

        // Assert
        Assert.Equal("\"Hello World!\"", result.Value?.ToJsonString());
    }

    [Fact]
    public void ExtensionResultEncode_Errored_ReturnErroredResult()
    {
        // Arrange
        var schema = StringValueSchema.Instance;
        var decoder = new JsonNodeEncoder();

        // Act
        var result = schema.Encode(Errors.Error("Test"), decoder);

        // Assert
        Assert.NotNull(result.Error);
    }

    [Fact]
    public void ExtensionResultDecode_Success_DecodeSucceed()
    {
        // Arrange
        var schema = StringValueSchema.Instance;
        var decoder = new JsonElementDecoder();

        var element = JsonDocument.Parse("\"Hello World!\"");

        // Act
        var result = schema.Decode(Result.Success(element.RootElement),
            decoder);

        // Assert
        Assert.Equal("Hello World!", result.Value);
    }

    [Fact]
    public void ExtensionResultDecode_Errored_ReturnErrored()
    {
        // Arrange
        var schema = StringValueSchema.Instance;
        var decoder = new JsonElementDecoder();

        // Act
        var result = schema.Decode(Errors.Error("Test"), decoder);

        // Assert
        Assert.NotNull(result.Error);
    }

    [Fact]
    public void ExtensionDecodeObjectTryPattern_ErroredResult_ReturnErrored()
    {
        // Arrange
        var schema = new Mock<IValueSchema<string>>();
        schema.Setup(x => x.Decode(It.IsAny<object>(),
            It.IsAny<IValueDecoder<object>>()))
            .Returns(Errors.Error("Test"));

        // Act
        var result = schema.Object.Decode("foo",
            Mock.Of<IValueDecoder<object>>(),
            out var value);

        // Assert
        Assert.Multiple(() => Assert.False(result),
            () => Assert.Null(value));
    }

    [Fact]
    public void ExtensionDecodeObjectTryPattern_ErroredInput_ReturnErrored()
    {
        // Arrange
        var schema = new Mock<IValueSchema<string>>();

        // Act
        var result = schema.Object.Decode(Errors.Error("Test error"),
            Mock.Of<IValueDecoder<object>>(),
            out var value);

        // Assert
        Assert.Multiple(() => Assert.False(result),
            () => Assert.Null(value));
    }

    [Fact]
    public void ExtensionDecodeObjectTryPattern_SuccessResult_ReturnEmpty()
    {
        // Arrange
        var schema = new Mock<IValueSchema<string>>();
        schema.Setup(x => x.Decode(It.IsAny<object>(),
            It.IsAny<IValueDecoder<object>>()))
            .Returns("OK!");

        // Act
        var result = schema.Object.Decode("foo",
            Mock.Of<IValueDecoder<object>>(),
            out var value);

        // Assert
        Assert.Multiple(() => Assert.True(result),
            () => Assert.Equal("OK!", value));
    }
}