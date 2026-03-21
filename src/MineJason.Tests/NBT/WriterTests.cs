// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt;

namespace MineJason.Tests.NBT;

public class WriterTests
{
    [Fact]
    public void WriteKeyNameUnquoted()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WriteKeyNameUnquoted("unquoted_key_name");
        writer.Dispose();

        Assert.Equal("unquoted_key_name:",
            str.ToString());
    }

    [Fact]
    public void WriteKeyNameQuoted()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WriteKeyNameQuoted("test_key_name");
        writer.Dispose();

        Assert.Equal("\"test_key_name\":",
            str.ToString());
    }

    [Fact]
    public void WriteValue_Byte()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WriteValue((sbyte)127);
        writer.Dispose();

        Assert.Equal("127b", str.ToString());
    }

    [Fact]
    public void WriteValue_Boolean_True()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WriteValue(true);
        writer.Dispose();

        Assert.Equal("1b", str.ToString());
    }

    [Fact]
    public void WriteValue_Boolean_False()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WriteValue(false);
        writer.Dispose();

        Assert.Equal("0b", str.ToString());
    }

    [Fact]
    public void WriteProperty_Byte()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WriteProperty("byte_property", (sbyte)100);
        writer.Dispose();

        Assert.Equal("byte_property:100b", str.ToString());
    }

    [Fact]
    public void WriteProperty_Boolean_True()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WriteProperty("boolean_property", true);
        writer.Dispose();

        Assert.Equal("boolean_property:1b", str.ToString());
    }

    [Fact]
    public void WriteProperty_Boolean_False()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WriteProperty("boolean_property", false);
        writer.Dispose();

        Assert.Equal("boolean_property:0b", str.ToString());
    }

    [Fact]
    public void WritePropertyQuoted_Byte()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WritePropertyQuoted("byte_quoted", (sbyte)100);
        writer.Dispose();

        Assert.Equal("\"byte_quoted\":100b", str.ToString());
    }

    [Fact]
    public void WritePropertyQuoted_Boolean_True()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WritePropertyQuoted("boolean_quoted", true);
        writer.Dispose();

        Assert.Equal("\"boolean_quoted\":1b", str.ToString());
    }

    [Fact]
    public void WritePropertyQuoted_Boolean_False()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WritePropertyQuoted("boolean_quoted", false);
        writer.Dispose();

        Assert.Equal("\"boolean_quoted\":0b", str.ToString());
    }


    [Fact]
    public void WriteProperty_Int()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WriteProperty("int_property", 32771);
        writer.Dispose();

        Assert.Equal("int_property:32771", str.ToString());
    }

    [Fact]
    public void WritePropertyQuoted_Int()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WritePropertyQuoted("int_quoted", 32772);
        writer.Dispose();

        Assert.Equal("\"int_quoted\":32772", str.ToString());
    }

    [Fact]
    public void WriteProperty_Long()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WriteProperty("long_property", 1234567L);
        writer.Dispose();

        Assert.Equal("long_property:1234567L", str.ToString());
    }

    [Fact]
    public void WritePropertyQuoted_Long()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WritePropertyQuoted("long_quoted", 1234567L);
        writer.Dispose();

        Assert.Equal("\"long_quoted\":1234567L", str.ToString());
    }

    [Fact]
    public void WriteProperty_Float()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WriteProperty("double_property", 123.45f);
        writer.Dispose();

        Assert.Equal("double_property:123.45f", str.ToString());
    }

    [Fact]
    public void WritePropertyQuoted_Float()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WritePropertyQuoted("double_quoted", 678.9f);
        writer.Dispose();

        Assert.Equal("\"double_quoted\":678.9f", str.ToString());
    }

    [Fact]
    public void WriteProperty_Double()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WriteProperty("double_property", 123.45);
        writer.Dispose();

        Assert.Equal("double_property:123.45d", str.ToString());
    }

    [Fact]
    public void WritePropertyQuoted_Double()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);

        writer.WritePropertyQuoted("double_quoted", 678.9);
        writer.Dispose();

        Assert.Equal("\"double_quoted\":678.9d", str.ToString());
    }
    
    [Fact]
    public void WriteComma()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        writer.WriteComma();
        writer.WriteComma();
        
        Assert.Equal(",",
            str.ToString());
    }
    
    [Fact]
    public void WriteStringProperty_SingleQuote_NoEscape()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        writer.WriteProperty("TestProperty", "AString", true);
        writer.Dispose();

        Assert.Equal("TestProperty:'AString'",
            str.ToString());
    }
    
    [Fact]
    public void WriteStringProperty_SingleQuote_DoEscape()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        writer.WriteProperty("TestProperty", "AStringEscapeMe'Me", true);
        writer.Dispose();

        Assert.Equal("TestProperty:'AStringEscapeMe\\'Me'",
            str.ToString());
    }
    
    [Fact]
    public void WriteStringProperty_SingleQuote_EscapeSlash()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        writer.WriteProperty("TestString", "IAmSlash\\EscapeMe", true);
        writer.Dispose();

        Assert.Equal(@"TestString:'IAmSlash\\EscapeMe'",
            str.ToString());
    }
    
    [Fact]
    public void WriteStringProperty_DoubleQuote_NoEscape()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        writer.WriteProperty("TestString", "AnotherStringHere");
        writer.Dispose();

        Assert.Equal("TestString:\"AnotherStringHere\"",
            str.ToString());
    }
    
    [Fact]
    public void WriteStringProperty_DoubleQuote_DoEscape()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        writer.WriteProperty("TestString", "AnotherEscaped\"String\"Here");
        writer.Dispose();

        Assert.Equal("TestString:\"AnotherEscaped\\\"String\\\"Here\"",
            str.ToString());
    }
    
    [Fact]
    public void WriteStringProperty_DoubleQuote_EscapeSlash()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        writer.WriteProperty("TestString", "IAmSlash\\EscapeMe");
        writer.Dispose();

        Assert.Equal("TestString:\"IAmSlash\\\\EscapeMe\"",
            str.ToString());
    }
    
    [Fact]
    public void NestedCompound()
    {
        var str = new StringWriter();
        var writer = new SNbtWriter(str);
        
        writer.WriteBeginCompound();
        writer.WriteProperty("Test", 123);
        writer.WritePropertyCompoundBegin("Compound");
        writer.WriteProperty("Number", 456);
        writer.WriteProperty("String", "My String");
        writer.WriteEndCompound();
        writer.WriteEndCompound();
        writer.Dispose();

        Assert.Equal("{Test:123,Compound:{Number:456,String:\"My String\"}}",
            str.ToString());
    }
}