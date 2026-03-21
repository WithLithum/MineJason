// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Serialization.Utilities;

namespace MineJason.Tests.Serialization;

public class LambdaHelperTests
{
    [Fact]
    public void GetPropertyInfo_Expression_CorrectProperty()
    {
        // Nothing to arrange

        // Act
        var result = LambdaHelper.GetPropertyInfo<string, int>(x => x.Length);

        // Assert
        Assert.Equal("Length", result.Name);
    }
}