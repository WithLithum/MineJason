// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json;
using MineJason.Extras.Selectors;
using MineJason.Extras.Serialization.Schema;
using MineJason.Serialization.IO.Json;
using MineJason.Tests.Serialization.Utilities;

namespace MineJason.Tests.Extras.Selectors;

public class SelectorSchemaTest
{
    [Fact]
    public void SelectorSchema_Encode_SameResult()
    {
        // Arrange
        const string selectorString = "@a[x=100,y=50,z=-100,dx=15,dy=2,dz=12]";
        var selector = EntitySelectorStringFormatter
            .ParseSelector(selectorString);

        // Act
        var parseResult = EntitySelectorSchema.Instance.Encode(selector,
            new JsonNodeEncoder());

        // Assert
        Assert.Equal(selectorString,
            ResultAssert.Success(parseResult).AsValue().GetValue<string>());
    }

    [Fact]
    public void SelectorSchemaDecode_NotString_Error()
    {
        // Arrange
        var json = JsonElement.Parse("{\"obviously\":\"not a string\"}");

        // Act
        var result = EntitySelectorSchema.Instance.Decode(json,
            new JsonElementDecoder());

        // Assert
        ResultAssert.Failure(result);
    }

    [Fact]
    public void SelectorSchemaDecode_Malformed_Error()
    {
        // Arrange
        var json = JsonElement.Parse("\"@a[\"");

        // Act
        var result = EntitySelectorSchema.Instance.Decode(json,
            new JsonElementDecoder());

        // Assert
        ResultAssert.Failure(result);
    }

    [Fact]
    public void SelectorSchema_Decode_SameResult()
    {
        // Arrange
        const string selector = "@a[x=100,y=50,z=-100,dx=15,dy=2,dz=12]";
        var json = JsonElement.Parse($"\"{selector}\"");

        // Act
        var parseResult = EntitySelectorSchema.Instance.Decode(json,
            new JsonElementDecoder());

        // Assert
        Assert.Equal(selector, ResultAssert.Success(parseResult).ToString());
    }
}
