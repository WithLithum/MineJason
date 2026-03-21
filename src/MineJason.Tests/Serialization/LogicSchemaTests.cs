// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json;
using MineJason.Serialization.IO;
using MineJason.Serialization.IO.Json;
using MineJason.Serialization.Schema;
using MineJason.Serialization.Schema.Primitive;
using MineJason.Serialization.Utilities.Results;
using Moq;

namespace MineJason.Tests.Serialization;

public class LogicSchemaTests
{
    [Fact]
    public void OneOf_TwoConditionsLastPass_Succeeds()
    {
        // Arrange
        const string json = "\"Test\"";
        var codecMock = new Mock<ValueSchema<string>>();

        codecMock.Setup(x => x.Decode(It.IsAny<JsonElement>(),
                It.IsAny<IValueDecoder<JsonElement>>()))
            .Returns(Errors.Error("Error"));

        var decoder = new JsonElementDecoder();
        var oneOf = new OneOfValueSchema<string>([
            codecMock.Object,
            StringValueSchema.Instance
        ]);
        var element = JsonDocument.Parse(json);

        // Act
        var result = oneOf.Decode(element.RootElement, decoder);

        // Assert
        Assert.Null(result.Error);
        Assert.Equal("Test", result.Value);
    }
}