// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Serialization.Utilities;

namespace MineJason.Tests.Serialization;

public class ReflectionTests
{
    private struct NormalStruct;
    private readonly struct ReadOnlyStruct;

    [Fact]
    public void IsReadOnlyStruct_NormalStruct_ReturnsFalse()
    {
        // Act
        var result = ReflectionHelper.IsReadOnlyStruct(typeof(NormalStruct));

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsReadOnlyStruct_ReadOnlyStruct_ReturnsTrue()
    {
        // Act
        var result = ReflectionHelper.IsReadOnlyStruct(typeof(ReadOnlyStruct));

        // Assert
        Assert.True(result);
    }
}