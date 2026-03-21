// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Values;
using MineJason.SNbt;

namespace MineJason.Tests.NBT;

public class ValueTests
{
    [Fact]
    public void ByteValue_WriteTo()
    {
        var stream = new StringWriter();
        using (var writer = new SNbtWriter(stream))
        {
            new SNbtByteValue(127).WriteTo(writer);
        }
        
        Assert.Equal("127b", stream.ToString());
    }

    [Fact]
    public void ByteValue_BooleanValue_True()
    {
        Assert.True(new SNbtByteValue(true).BooleanValue());
    }
    
    [Fact]
    public void ByteValue_BooleanValue_False()
    {
        Assert.False(new SNbtByteValue(false).BooleanValue());
    }
    
    [Fact]
    public void ByteValue_Boolean_True()
    {
        Assert.Equal((sbyte)1, new SNbtByteValue(true).Value);
    }
    
    [Fact]
    public void ByteValue_Boolean_False()
    {
        Assert.Equal((sbyte)0, new SNbtByteValue(false).Value);
    }
    
    [Fact]
    public void ShortValue_WriteTo()
    {
        var stream = new StringWriter();
        using (var writer = new SNbtWriter(stream))
        {
            new SNbtShortValue(12345).WriteTo(writer);
        }
        
        Assert.Equal("12345s", stream.ToString());
    }
    
    [Fact]
    public void IntValue_WriteTo()
    {
        var stream = new StringWriter();
        using (var writer = new SNbtWriter(stream))
        {
            new SNbtIntValue(67891011).WriteTo(writer);
        }
        
        Assert.Equal("67891011", stream.ToString());
    }
    
    [Fact]
    public void LongValue_WriteTo()
    {
        var stream = new StringWriter();
        using (var writer = new SNbtWriter(stream))
        {
            new SNbtLongValue(121314151617).WriteTo(writer);
        }
        
        Assert.Equal("121314151617L", stream.ToString());
    }
    
    [Fact]
    public void FloatValue_WriteTo()
    {
        var stream = new StringWriter();
        using (var writer = new SNbtWriter(stream))
        {
            new SNbtFloatValue(12.34f).WriteTo(writer);
        }
        
        Assert.Equal("12.34f", stream.ToString());
    }
    
    [Fact]
    public void DoubleValue_WriteTo()
    {
        var stream = new StringWriter();
        using (var writer = new SNbtWriter(stream))
        {
            new SNbtDoubleValue(56.789101112).WriteTo(writer);
        }
        
        Assert.Equal("56.789101112d", stream.ToString());
    }
    
    [Fact]
    public void Compound_ToSNbtString_NoSubCompounds()
    {
        var value = new SNbtCompound()
        {
            { "StringValue", new SNbtStringValue("MyCompound") },
            { "IntValue", new SNbtIntValue(233) }
        };
        Assert.Equal("{StringValue:'MyCompound',IntValue:233}", value.ToSNbtString());
    }
    
    [Fact]
    public void Compound_ToSNbtString_HaveSubCompounds()
    {
        var value = new SNbtCompound()
        {
            { "StringValue", new SNbtStringValue("MyCompound") },
            { "IntValue", new SNbtIntValue(23325) },
            { "CompoundValue", new SNbtCompound
            {
                { "SubValue", new SNbtByteValue(111) }
            } }
        };
        Assert.Equal("{StringValue:'MyCompound',IntValue:23325,CompoundValue:{SubValue:111b}}", value.ToSNbtString());
    }

    [Fact]
    public void GuidValue_IntArray()
    {
        var guid = Guid.Parse("0097dec4-4f09-4c96-a497-73d6158b82a1");

        var value = new SNbtGuidValue(guid).ToSNbtString();
        Assert.Equal("[I;9952964,1326009494,-1533578282,361464481]", value);
    }
}