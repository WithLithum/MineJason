// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Data.Nbt;

using MineJason.SNbt;
using MineJason.SNbt.Values;

internal static class SNbtExtensions
{
    internal static SNbtObjectBuilder Property(this SNbtObjectBuilder builder, string key, bool value)
    {
        return builder.Property(key, new SNbtByteValue(value));
    }
    
    internal static SNbtObjectBuilder Property(this SNbtObjectBuilder builder, string key, bool? value)
    {
        return !value.HasValue ? builder : builder.Property(key, new SNbtByteValue(value.Value));
    }
    
    internal static SNbtObjectBuilder Property(this SNbtObjectBuilder builder, string key, int value)
    {
        return builder.Property(key, new SNbtIntValue(value));
    }
    
    internal static SNbtObjectBuilder Property(this SNbtObjectBuilder builder, string key, int? value)
    {
        return !value.HasValue ? builder : builder.Property(key, new SNbtIntValue(value.Value));
    }
    
    internal static SNbtObjectBuilder Property(this SNbtObjectBuilder builder, string key, long value)
    {
        return builder.Property(key, new SNbtLongValue(value));
    }
    
    internal static SNbtObjectBuilder Property(this SNbtObjectBuilder builder, string key, long? value)
    {
        return !value.HasValue ? builder : builder.Property(key, new SNbtLongValue(value.Value));
    }
    
    internal static SNbtObjectBuilder Property(this SNbtObjectBuilder builder, string key, float value)
    {
        return builder.Property(key, new SNbtFloatValue(value));
    }
    
    internal static SNbtObjectBuilder PropertyNullable<T>(this SNbtObjectBuilder builder, string key, T? value)
        where T : ISNbtValue
    {
        if (value != null)
        {
            builder.Property(key, value);
        }
        
        return builder;
    }
    
    internal static SNbtObjectBuilder PropertyNotEmpty<TCollection, TItem>(this SNbtObjectBuilder builder, string key, TCollection? value)
        where TCollection : ICollection<TItem>, ISNbtValue
    {
        if (value != null && value.Count != 0)
        {
            builder.Property(key, value);
        }
        
        return builder;
    }
    
    internal static SNbtObjectBuilder PropertyNotEmpty<TKey, TValue>(this SNbtObjectBuilder builder, string key, IDictionary<TKey, TValue>? value)
    {
        if (value != null
            && value.Count != 0
            && value is ISNbtValue val)
        {
            builder.Property(key, val);
        }
        
        return builder;
    }
}