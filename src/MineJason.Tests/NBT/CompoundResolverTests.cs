// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;
using MineJason.SNbt.Parsing.Complex;
using MineJason.SNbt.Values;
using Moq;

namespace MineJason.Tests.NBT;
public class CompoundResolverTests
{
    [Fact]
    public void Resolve_DisallowAlreadyRead()
    {
        // Arrange
        var reader = new StringReader("{property:\"value\",property:123}");
        var resolver = new CompoundResolver(new SNbtCompoundReader(reader),
            null,
            null)
        {
            DisallowAlreadyRead = true
        };

        // Act
        var exception = Record.Exception(resolver.ResolveCompound);
        
        // Assert
        Assert.IsType<FormatException>(exception);
    }
    
    [Fact]
    public void Resolve_FlatType()
    {
        // Arrange
        var mock = new Mock<CompoundResolver.ProcessPropertyDelegate>();
        
        var reader = new StringReader("{property:\"value\"}");
        var resolver = new CompoundResolver(new SNbtCompoundReader(reader),
            mock.Object,
            null);
        
        // Act
        resolver.ResolveCompound();
        
        // Assert
        mock.Verify(x => x("property", new SNbtStringValue("value")),
            Times.Once);
    }
    
    [Fact]
    public void Resolve_FlatType_MultipleProperties()
    {
        // Arrange
        var mock = new Mock<CompoundResolver.ProcessPropertyDelegate>();
        
        var reader = new StringReader("{property:\"value\",number:123}");
        var resolver = new CompoundResolver(new SNbtCompoundReader(reader),
            mock.Object,
            null);
        
        // Act
        resolver.ResolveCompound();
        
        // Assert
        mock.Verify(x => x("property", new SNbtStringValue("value")),
            Times.Once);
        mock.Verify(x => x("number", new SNbtIntValue(123)),
            Times.Once);
    }
    
    [Fact]
    public void Resolve_NestedCompounds()
    {
        // Arrange
        var mock = new Mock<CompoundResolver.ProcessCompoundPropertyDelegate>();
        
        var reader = new StringReader("{nested:{property:123}}");
        var resolver = new CompoundResolver(new SNbtCompoundReader(reader),
            null,
            mock.Object);
        
        // Act
        resolver.ResolveCompound();
        
        // Assert
        mock.Verify(x => x("nested",
                new SNbtBasicToken(BasicTokenType.Compound, "{property:123}")),
            Times.Once);
    }
}