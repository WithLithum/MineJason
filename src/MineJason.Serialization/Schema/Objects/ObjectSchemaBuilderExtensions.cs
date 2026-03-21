// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Objects;

using System.Linq.Expressions;
using JetBrains.Annotations;
using MineJason.Serialization.Schema.Primitive;

/// <summary>
/// Provides methods to configure properties of commonly found types.
/// </summary>
[PublicAPI]
public static class ObjectSchemaBuilderExtensions
{
    /// <summary>
    /// Configures the schema to convert the specified boolean property, and fail in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> Boolean<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, bool>> property,
        Predicate<bool>? ignoreIf = null)
    {
        return builder.Property(name, property, BooleanSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified boolean property, and disregard the property
    /// in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> OptionalBoolean<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, bool?>> property,
        Predicate<bool?>? ignoreIf = null)
    {
        return builder.OptionalProperty(name, property, BooleanSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="byte"/>
    /// value, and fail in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> Byte<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, byte>> property,
        Predicate<byte>? ignoreIf = null)
    {
        return builder.Property(name, property, ByteValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="byte"/>
    /// value, and disregard the property in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> OptionalByte<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, byte?>> property,
        Predicate<byte?>? ignoreIf = null)
    {
        return builder.OptionalProperty(name, property, ByteValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="sbyte"/>
    /// value, and fail in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> SByte<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, sbyte>> property,
        Predicate<sbyte>? ignoreIf = null)
    {
        return builder.Property(name, property, SByteValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="sbyte"/>
    /// value, and disregard the property in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> OptionalSByte<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, sbyte?>> property,
        Predicate<sbyte?>? ignoreIf = null)
    {
        return builder.OptionalProperty(name, property, SByteValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="ushort"/>
    /// value, and fail in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> UInt16<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, ushort>> property,
        Predicate<ushort>? ignoreIf = null)
    {
        return builder.Property(name, property, UInt16ValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="ushort"/>
    /// value, and disregard the property in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> OptionalUInt16<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, ushort?>> property,
        Predicate<ushort?>? ignoreIf = null)
    {
        return builder.OptionalProperty(name, property, UInt16ValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="short"/>
    /// value, and fail in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> Int16<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, short>> property,
        Predicate<short>? ignoreIf = null)
    {
        return builder.Property(name, property, Int16ValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="short"/>
    /// value, and disregard the property in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> OptionalInt16<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, short?>> property,
        Predicate<short?>? ignoreIf = null)
    {
        return builder.OptionalProperty(name, property, Int16ValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="uint"/>
    /// value, and fail in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> UInt32<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, uint>> property,
        Predicate<uint>? ignoreIf = null)
    {
        return builder.Property(name, property, UInt32ValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="uint"/>
    /// value, and disregard the property in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> OptionalUInt32<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, uint?>> property,
        Predicate<uint?>? ignoreIf = null)
    {
        return builder.OptionalProperty(name, property, UInt32ValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="int"/>
    /// value, and fail in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> Int32<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, int>> property,
        Predicate<int>? ignoreIf = null)
    {
        return builder.Property(name, property, Int32ValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="int"/>
    /// value, and disregard the property in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> OptionalInt32<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, int?>> property,
        Predicate<int?>? ignoreIf = null)
    {
        return builder.OptionalProperty(name, property, Int32ValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="ulong"/>
    /// value, and fail in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> UInt64<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, ulong>> property,
        Predicate<ulong>? ignoreIf = null)
    {
        return builder.Property(name, property, UInt64ValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="ulong"/>
    /// value, and disregard the property in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> OptionalUInt64<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, ulong?>> property,
        Predicate<ulong?>? ignoreIf = null)
    {
        return builder.OptionalProperty(name, property, UInt64ValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="long"/>
    /// value, and fail in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> Int64<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, long>> property,
        Predicate<long>? ignoreIf = null)
    {
        return builder.Property(name, property, Int64ValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="long"/>
    /// value, and disregard the property in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> OptionalInt64<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, long?>> property,
        Predicate<long?>? ignoreIf = null)
    {
        return builder.OptionalProperty(name, property, Int64ValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="float"/>
    /// value, and fail in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> Single<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, float>> property,
        Predicate<float>? ignoreIf = null)
    {
        return builder.Property(name, property, SingleValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="float"/>
    /// value, and disregard the property in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> OptionalSingle<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, float?>> property,
        Predicate<float?>? ignoreIf = null)
    {
        return builder.OptionalProperty(name, property, SingleValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="double"/>
    /// value, and fail in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> Double<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, double>> property,
        Predicate<double>? ignoreIf = null)
    {
        return builder.Property(name, property, DoubleValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified property containing <see cref="double"/>
    /// value, and disregard the property in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> OptionalDouble<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, double?>> property,
        Predicate<double?>? ignoreIf = null)
    {
        return builder.OptionalProperty(name, property, DoubleValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified string property, and fail if it is absent.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> String<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, string>> property,
        Predicate<string>? ignoreIf = null)
    {
        return builder.Property(name, property, StringValueSchema.Instance,
            ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified string property, and disregard the property
    /// in its absence.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public static ObjectSchemaBuilder<TValue> OptionalString<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, string?>> property,
        Predicate<string?>? ignoreIf = null)
    {
        return builder.Property(name, property, StringValueSchema.Instance, true,
            ignoreIf: ignoreIf);
    }
}