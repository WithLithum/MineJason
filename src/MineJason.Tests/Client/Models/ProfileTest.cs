// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using MineJason.Data.Profile;
using MineJason.Serialization.IO.Json;
using MineJason.Serialization.Schema;
using MineJason.Tests.Serialization.Utilities;

namespace MineJason.Tests.Client.Models;

public class ProfileTest
{
    [Fact]
    public void StringSchema_Encode_Error()
    {
        // Arrange
        var input = new PlayerProfile
        {
            Name = "Test"
        };
        var encoder = new JsonNodeEncoder();
        var schema = new StringPlayerProfileSchema();

        // Act
        var result = schema.Encode(input, encoder);

        // Assert
        ResultAssert.Failure(result);
    }

    [Fact]
    public void StringSchema_ValidName_Success()
    {
        // Arrange
        const string input = "\"PlayerName\"";
        var element = JsonElement.Parse(input);
        var decoder = new JsonElementDecoder();
        var schema = new StringPlayerProfileSchema();

        // Act
        var result = schema.Decode(element, decoder);

        // Assert
        Assert.Equal("PlayerName", ResultAssert.Success(result).Name);
    }
}
