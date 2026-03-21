// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0


using System.Text.Json.Nodes;
using MineJason.Serialization.IO.Json;

namespace MineJason.Tests.Serialization;

public class IoTests
{
    [Fact]
    public void ValueEncoder_Boolean_EncodesSuccessfully()
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
    public void ValueInput_Byte_EncodesSuccessfully()
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
    public void ValueInput_SByte_EncodesSuccessfully()
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
    public void ValueInput_UInt16_EncodesSuccessfully()
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
    public void ValueInput_Int16_EncodesSuccessfully()
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
    public void ValueInput_UInt32_EncodesSuccessfully()
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
    public void ValueInput_Int32_EncodesSuccessfully()
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
    public void ValueInput_UInt64_EncodesSuccessfully()
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
    public void ValueInput_Int64_EncodesSuccessfully()
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
    public void ValueInput_Single_EncodesSuccessfully()
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
    public void ValueInput_Double_EncodesSuccessfully()
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
    public void ObjectAdapter_Enumerate_ReturnCorrectly()
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
}