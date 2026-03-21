// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json;
using MineJason.Serialization.IO.Json;
using MineJason.Serialization.Schema.Objects;
using MineJason.Serialization.Schema.Primitive;
using MineJason.Serialization.Utilities.Results;
using MineJason.Tests.Serialization.Utilities;

namespace MineJason.Tests.Serialization;

public class ObjectSchemaTests
{
    #region Mock types

    public record Foo
    {
        public required string Bar { get; init; }
    }

    public record NullMock
    {
        public required string? Value { get; init; }
    }

    public record Complex
    {
        public required int One { get; init; }
        public required Foo Two { get; init; }
    }

    public readonly struct ReadOnlyStruct;

    #endregion

    #region Optional value type

    private record OptionalMock
    {
        public required int? Value { get; init; }
    }

    [Fact]
    public void ObjectSchemaBuilder_EncodeOptionalValueNotPresent_ConvertsCorrectly()
    {
        // Arrange
        var obj = new OptionalMock
        {
            Value = null
        };
        var encoder = new JsonNodeEncoder();
        var schema = new ObjectSchemaBuilder<OptionalMock>()
            .OptionalProperty("value", x => x.Value,
                Int32ValueSchema.Instance)
            .Build();

        // Act
        var result = schema.Encode(obj, encoder);

        // Assert
        Assert.Equal("{}", ResultAssert.NotEmpty(result)
            .ToJsonString());
    }

    [Fact]
    public void ObjectSchemaBuilder_EncodeOptionalValuePresent_ConvertsCorrectly()
    {
        // Arrange
        var obj = new OptionalMock
        {
            Value = 123
        };
        var encoder = new JsonNodeEncoder();
        var schema = new ObjectSchemaBuilder<OptionalMock>()
            .OptionalProperty("value", x => x.Value,
                Int32ValueSchema.Instance)
            .Build();

        // Act
        var result = schema.Encode(obj, encoder);

        // Assert
        Assert.Equal("{\"value\":123}", ResultAssert.NotEmpty(result)
            .ToJsonString());
    }

    [Fact]
    public void ObjectSchemaBuilder_DecodeOptionalValueNotPresent_ConvertsCorrectly()
    {
        // Arrange
        const string json = "{}";

        var decoder = new JsonElementDecoder();
        var schema = new ObjectSchemaBuilder<OptionalMock>()
            .OptionalProperty("value", x => x.Value,
                Int32ValueSchema.Instance)
            .Build();

        var element = JsonDocument.Parse(json);

        // Act
        var result = schema.Decode(element.RootElement, decoder);

        // Assert
        Assert.Null(result.Error);
        Assert.NotNull(result.Value);
        Assert.False(result.Value.Value.HasValue);
    }

    [Fact]
    public void ObjectSchemaBuilder_DecodeOptionalValuePresent_ConvertsCorrectly()
    {
        // Arrange
        const string json = "{\"value\":456}";

        var decoder = new JsonElementDecoder();
        var schema = new ObjectSchemaBuilder<OptionalMock>()
            .OptionalProperty("value", x => x.Value,
                Int32ValueSchema.Instance)
            .Build();

        var element = JsonDocument.Parse(json);

        // Act
        var result = schema.Decode(element.RootElement, decoder);

        // Assert
        Assert.Null(result.Error);
        Assert.Equal(456, result.Value?.Value);
    }

    #endregion

    [Fact]
    public void ObjectSchemaBuilder_EncodeNullOfOptionalProperty_ConvertsCorrectly()
    {
        // Arrange
        var obj = new NullMock()
        {
            Value = null
        };
        var encoder = new JsonNodeEncoder();
        var schema = new ObjectSchemaBuilder<NullMock>()
            .OptionalString("value", x => x.Value)
            .Build();

        // Act
        var result = schema.Encode(obj, encoder);

        // Assert
        Assert.Equal("{}", ResultAssert.NotEmpty(result)
            .ToJsonString());
    }

    [Fact]
    public void ObjectSchemaBuilder_EncodeIgnoreOptionalProperty_ConvertsCorrectly()
    {
        // Arrange
        var obj = new OptionalMock()
        {
            Value = 123
        };
        var encoder = new JsonNodeEncoder();
        var schema = new ObjectSchemaBuilder<OptionalMock>()
            .OptionalInt32("value", x => x.Value,
                ignoreIf: x => x == 123)
            .Build();

        // Act
        var result = schema.Encode(obj, encoder);

        // Assert
        Assert.Equal("{}", ResultAssert.NotEmpty(result)
            .ToJsonString());
    }

    [Fact]
    public void ObjectSchemaBuilder_EncodeSimpleMissingField_ReturnsFailure()
    {
        // Arrange
        var encoder = new JsonNodeEncoder();
        var schema = new ObjectSchemaBuilder<Foo>()
            .Constant("foo", "My foo", StringValueSchema.Instance)
            .String("bar", x => x.Bar)
            .Build();

        // Act
        var result = schema.Encode(new Foo
        {
            Bar = null! // Intentional so we test field is missing
        }, encoder);

        // Assert
        ResultAssert.Failure(result);
    }

    [Fact]
    public void ObjectSchemaBuilder_EncodeSimpleWithConstant_ConvertsCorrectly()
    {
        // Arrange
        var encoder = new JsonNodeEncoder();
        var schema = new ObjectSchemaBuilder<Foo>()
            .Constant("foo", "My foo", StringValueSchema.Instance)
            .String("bar", x => x.Bar)
            .Build();

        // Act
        var result = schema.Encode(new Foo
        {
            Bar = "bar"
        }, encoder);

        // Assert
        Assert.Equal("{\"foo\":\"My foo\",\"bar\":\"bar\"}", ResultAssert.NotEmpty(result).ToJsonString());
    }

    [Fact]
    public void ObjectSchemaBuilder_EncodeSimpleWithIgnore_ConvertsCorrectly()
    {
        // Arrange
        var encoder = new JsonNodeEncoder();
        var schema = new ObjectSchemaBuilder<Foo>()
            .String("bar", x => x.Bar, ignoreIf: x => x == "bar")
            .Build();

        // Act
        var result = schema.Encode(new Foo
        {
            Bar = "bar"
        }, encoder);

        // Assert
        Assert.Equal("{}", ResultAssert.NotEmpty(result).ToJsonString());
    }

    [Fact]
    public void ObjectSchemaBuilder_DecodeOnReadOnlyStructType_Throws()
    {
        // Arrange
        var decoder = new JsonElementDecoder();
        var schema = new ObjectSchemaBuilder<ReadOnlyStruct>()
            .Build();

        // Act
        var exception = Record.Exception(() => _ = schema.Decode(new JsonElement(),
            decoder));

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
    }

    [Fact]
    public void ObjectSchemaBuilder_DecodeSimple_ConvertsCorrectly()
    {
        // Arrange
        const string json = "{\"bar\":\"Hello World!\"}";

        var decoder = new JsonElementDecoder();
        var schema = new ObjectSchemaBuilder<Foo>()
            .Constant("foo", "My foo", StringValueSchema.Instance)
            .String(null, x => x.Bar)
            .Build();

        var element = JsonDocument.Parse(json);

        // Act
        var result = schema.Decode(element.RootElement, decoder);

        // Assert
        Assert.Null(result.Error);
        Assert.Equal("Hello World!", result.Value?.Bar);
    }

    [Fact]
    public void ObjectSchemaBuilder_DecodeSimpleButMissingField_ReturnsFailure()
    {
        // Arrange
        const string json = "{}";

        var decoder = new JsonElementDecoder();
        var schema = new ObjectSchemaBuilder<Foo>()
            .Constant("foo", "My foo", StringValueSchema.Instance)
            .String(null, x => x.Bar)
            .Build();

        var element = JsonDocument.Parse(json);

        // Act
        var result = schema.Decode(element.RootElement, decoder);

        // Assert
        ResultAssert.Failure(result);
    }

    [Fact]
    public void ObjectSchemaBuilder_DecodeSimpleButMissingField_ReturnsCorrectErrorMessage()
    {
        // Arrange
        const string json = "{}";

        var decoder = new JsonElementDecoder();
        var schema = new ObjectSchemaBuilder<Foo>()
            .String(null, x => x.Bar)
            .Build();

        var element = JsonDocument.Parse(json);

        // Act
        var result = schema.Decode(element.RootElement, decoder);

        // Assert
        Assert.Equal(Errors.MissingProperty("bar").Message!, result.Error);
    }

    [Fact]
    public void ObjectSchemaBuilder_DecodeComplex_ConvertsCorrectly()
    {
        // Arrange
        const string json = "{\"one\":456,\"two\":{\"bar\":\"foo\"}}";

        var decoder = new JsonElementDecoder();
        var schema = new ObjectSchemaBuilder<Complex>()
            .Int32("one", x => x.One)
            .Object("two", x => x.Two, s =>
                s.String("bar", x => x.Bar))
            .Build();

        var element = JsonDocument.Parse(json);

        // Act
        var result = schema.Decode(element.RootElement, decoder);

        // Assert
        Assert.Null(result.Error);
        var value = result.Value;

        Assert.NotNull(value);
        Assert.Multiple(() => Assert.Equal(456, value.One),
            () => Assert.Equal("foo", value.Two.Bar));
    }

    [Fact]
    public void SchemaBuilderAddProperty_TypeMismatch_Throw()
    {
        // Arrange
        var schemaBuilder = new ObjectSchemaBuilder<Complex>();

        // Act
        var exception = Record.Exception(() => schemaBuilder.Int64(null, x => x.One));

        // Assert
        Assert.IsType<ArgumentException>(exception);
    }
}