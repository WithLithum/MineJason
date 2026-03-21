// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Tests.Selectors;

using MineJason.Data.Selectors;

public class SelectorPairSetResolverTests
{
    [Fact]
    public void Resolver_Success_FourBraces()
    {
        // Arrange
        var resolver = new EntitySelectorPairSetResolver("key={{{{Value}}}}");
        
        // Act
        var ex = Record.Exception(resolver.RunToEnd);

        // Assert
        Assert.Null(ex);
    }
}
