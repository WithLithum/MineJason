// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Codecs;
using MineJason.SNbt.Parsing;
using MineJason.SNbt.Codecs.Primitives;
using MineJason.SNbt;

namespace MineJason.Tests.NBT;

public class CodecTests
{
    [Fact]
    public void StringCodec_Explicit_Read_UnquotedWord()
    {
        // Arrange
        var str = new StringReader("unquoted");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = new StringCodec().Read(reader);
        
        // Assert
        Assert.Equal("unquoted", result);
    }
    
    [Fact]
    public void StringCodec_Explicit_Read_SingleQuoted()
    {
        // Arrange
        var str = new StringReader("'single quoted'");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = new StringCodec().Read(reader);
        
        // Assert
        Assert.Equal("single quoted", result);
    }
    
    [Fact]
    public void StringCodec_Explicit_Read_DoubleQuoted()
    {
        // Arrange
        var str = new StringReader("\"double quoted string\"");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = new StringCodec().Read(reader);
        
        // Assert
        Assert.Equal("double quoted string", result);
    }
    
    [Fact]
    public void StringCodec_Default_Write()
    {
        // Arrange
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        // Act
        writer.WriteValue("string test");
        
        // Assert
        Assert.Equal("\"string test\"", str.ToString());
    }
    
    [Fact]
    public void SByteCodec_Explicit_Read_True()
    {
        // Arrange
        var str = new StringReader("true");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = new SByteCodec().Read(reader);
        
        // Assert
        Assert.Equal(1, result);
    }
    
    [Fact]
    public void SByteCodec_Explicit_Read_False()
    {
        // Arrange
        var str = new StringReader("false");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = new SByteCodec().Read(reader);
        
        // Assert
        Assert.Equal(0, result);
    }
    
    [Fact]
    public void SByteCodec_Explicit_Read_Number()
    {
        // Arrange
        var str = new StringReader("127b");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = new SByteCodec().Read(reader);
        
        // Assert
        Assert.Equal(127, result);
    }
    
    [Fact]
    public void SByteCodec_Default_Write()
    {
        // Arrange
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        // Act
        writer.WriteValue((object)(sbyte)12);
        
        // Assert
        Assert.Equal("12b", str.ToString());
    }
    
    [Fact]
    public void Int32Codec_Explicit_Read()
    {
        // Arrange
        var str = new StringReader("12345678");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = new Int32Codec().Read(reader);
        
        // Assert
        Assert.Equal(12345678, result);
    }
    
    [Fact]
    public void Int32Codec_Default_Write()
    {
        // Arrange
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        // Act
        writer.WriteValue((object)123456789);
        
        // Assert
        Assert.Equal("123456789", str.ToString());
    }
    
    [Fact]
    public void Int64Codec_Explicit_Read()
    {
        // Arrange
        var str = new StringReader("1234567891234567L");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = new Int64Codec().Read(reader);
        
        // Assert
        Assert.Equal(1234567891234567L, result);
    }
    
    [Fact]
    public void Int64Codec_Default_Write()
    {
        // Arrange
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        // Act
        writer.WriteValue((object)123456789123456789L);
        
        // Assert
        Assert.Equal("123456789123456789L", str.ToString());
    }
    
    [Fact]
    public void SingleCodec_Explicit_Read()
    {
        // Arrange
        var str = new StringReader("12.3f");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = new SingleCodec().Read(reader);
        
        // Assert
        Assert.Equal(12.3f, result);
    }
    
    [Fact]
    public void SingleCodec_Default_Write()
    {
        // Arrange
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        // Act
        writer.WriteValue((object)12.3f);
        
        // Assert
        Assert.Equal("12.3f", str.ToString());
    }
    
    [Fact]
    public void DoubleCodec_Explicit_Read_Decimal()
    {
        // Arrange
        var str = new StringReader("123.45");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = new DoubleCodec().Read(reader);
        
        // Assert
        Assert.Equal(123.45, result);
    }
    
    [Fact]
    public void DoubleCodec_Explicit_Read_Suffix()
    {
        // Arrange
        var str = new StringReader("12345d");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = new DoubleCodec().Read(reader);
        
        // Assert
        Assert.Equal(12345, result);
    }
    
    [Fact]
    public void DoubleCodec_Default_Write()
    {
        // Arrange
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        // Act
        writer.WriteValue((object)12.34);
        
        // Assert
        Assert.Equal("12.34d", str.ToString());
    }
    
    [Fact]
    public void BooleanCodec_Explicit_Read_True()
    {
        // Arrange
        var str = new StringReader("true");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = new BooleanCodec().Read(reader);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void BooleanCodec_Explicit_Read_False()
    {
        // Arrange
        var str = new StringReader("false");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = new BooleanCodec().Read(reader);
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void BooleanCodec_Default_Write_True()
    {
        // Arrange
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        // Act
        writer.WriteValue((object)true);
        
        // Assert
        Assert.Equal("1b", str.ToString());
    }
    
    [Fact]
    public void BooleanCodec_Default_Write_False()
    {
        // Arrange
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        // Act
        writer.WriteValue((object)false);
        
        // Assert
        Assert.Equal("0b", str.ToString());
    }
    
    [Fact]
    public void GuidCodec_Explicit_Read()
    {
        // Arrange
        var str = new StringReader("[I;9952964,1326009494,-1533578282,361464481]");
        var reader = new SNbtBasicTokenReader(str);
        
        // Act
        var result = new GuidCodec().Read(reader);
        
        // Assert
        Assert.Equal(Guid.Parse("0097dec4-4f09-4c96-a497-73d6158b82a1"), result);
    }

    
    [Fact]
    public void GuidCodec_Default_Write()
    {
        // Arrange
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        // Act
        writer.WriteValue(Guid.Parse("0097dec4-4f09-4c96-a497-73d6158b82a1"));
        
        // Assert
        Assert.Equal("[I;9952964,1326009494,-1533578282,361464481]", str.ToString());
    }
    
    #region Codec Attribute Test

    [SNbtCodec(typeof(MockCodec))]
    private class CodecMockTest(string content)
    {
        internal string Content { get; } = content;
    }

    private class MockCodec : ISNbtCodec<CodecMockTest>
    {
        public CodecMockTest Read(SNbtBasicTokenReader reader)
        {
            throw new NotSupportedException();
        }

        public void Write(CodecMockTest value, SNbtWriter writer)
        {
            writer.WriteStringValue(value.Content);
        }
    }

    [Fact]
    public void SNbtCodecAttribute_Test()
    {
        // Arrange
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        // Act
        writer.WriteValue(new CodecMockTest("Test Codec Content"));
        
        // Assert
        Assert.Equal("\"Test Codec Content\"", str.ToString());
    }
    
    #endregion
}