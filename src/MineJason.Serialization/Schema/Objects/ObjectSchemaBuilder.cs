// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Objects;

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text.Json;
using JetBrains.Annotations;
using MineJason.Serialization.Utilities;

/// <summary>
/// Provides fluent syntax building for object schema.
/// </summary>
/// <typeparam name="TValue">The type of the object.</typeparam>
/// <seealso cref="ObjectSchema{TValue}"/>
[PublicAPI]
public class ObjectSchemaBuilder<[MeansImplicitUse] TValue>
{
    private readonly Dictionary<PropertySchema, IValueSchema> _meta = new();

    private readonly Dictionary<string, ValueWithSchema> _constants = new();

    private readonly JsonNamingPolicy _namingPolicy;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectSchemaBuilder{TValue}"/> class,
    /// optionally with the specified naming policy.
    /// </summary>
    /// <param name="namingPolicy">
    /// The naming policy to use. If <see langword="null"/>, uses the name of the property.
    /// </param>
    public ObjectSchemaBuilder(JsonNamingPolicy? namingPolicy = null)
    {
        _namingPolicy = namingPolicy ?? JsonNamingPolicy.SnakeCaseLower;
    }

    /// <summary>
    /// Configures the schema to write the specified constant when encoding the value. 
    /// </summary>
    /// <param name="key">The key of the constant.</param>
    /// <param name="value">The value.</param>
    /// <param name="schema">The schema.</param>
    /// <typeparam name="T">The type of the constant value.</typeparam>
    /// <returns>The current instance.</returns>
    public ObjectSchemaBuilder<TValue> Constant<T>(string key,
        [DisallowNull] T value,
        IValueSchema<T> schema)
    {
        ArgumentNullException.ThrowIfNull(value);

        _constants.Add(key, new ValueWithSchema
        {
            Value = value,
            Schema = schema
        });
        return this;
    }

    /// <summary>
    /// Configures the schema to convert the specified property, with the specified name and the
    /// specified value schema.
    /// </summary>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="schema">The schema used to convert the value.</param>
    /// <param name="optional">
    /// If <see langword="true"/>, the property will be ignored if it is absent in decoding or is
    /// <see langword="null"/> in encoding.
    /// </param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <typeparam name="TProperty">The type of the property value.</typeparam>
    /// <returns>The current instance.</returns>
    public ObjectSchemaBuilder<TValue> Property<TProperty>(string? name,
        Expression<Func<TValue, TProperty>> property,
        IValueSchema schema,
        bool optional = false,
        Predicate<TProperty>? ignoreIf = null)
    {
        ArgumentNullException.ThrowIfNull(property);
        ArgumentNullException.ThrowIfNull(schema);

        var info = property.GetPropertyInfo();
        if (info.PropertyType != typeof(TProperty))
        {
            throw new ArgumentException($"The property expression points to a property whose type" +
                                        $"({info.PropertyType}) is not the type specified in the" +
                                        $"type argument ({typeof(TProperty)}).",
                nameof(property));
        }

        var targetName = name ?? _namingPolicy.ConvertName(info.Name);

        Predicate<object>? ignoreIfReal = ignoreIf != null
            ? x => ignoreIf((TProperty)x)
            : null;
        _meta.Add(new PropertySchema(targetName, info, optional, ignoreIfReal),
            schema);

        return this;
    }

    /// <summary>
    /// Configures the schema to convert the specified property, with the specified name and the
    /// specified value schema, and containing the given value type. The property is disregarded if
    /// the property value is <see langword="null"/> or is absent.
    /// </summary>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="schema">The schema used to convert the value.</param>
    /// <typeparam name="TProperty">The type of the property value.</typeparam>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <returns>The current instance.</returns>
    public ObjectSchemaBuilder<TValue> OptionalProperty<TProperty>(string? name,
        Expression<Func<TValue, TProperty?>> property,
        IValueSchema schema,
        Predicate<TProperty?>? ignoreIf = null)
        where TProperty : struct
    {
        ArgumentNullException.ThrowIfNull(property);
        ArgumentNullException.ThrowIfNull(schema);

        var info = property.GetPropertyInfo();
        var targetName = name ?? _namingPolicy.ConvertName(info.Name);

        Predicate<object>? ignoreIfReal = ignoreIf != null
            ? x => ignoreIf((TProperty)x)
            : null;

        _meta.Add(new PropertySchema(targetName, info, true, ignoreIfReal),
            schema);
        return this;
    }

    /// <summary>
    /// Configures the schema to convert the specified object property with the schema configured
    /// accordingly, and fail in its absence.
    /// </summary>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="configure">A function that configures the property.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <returns>The current instance.</returns>
    public ObjectSchemaBuilder<TValue> Object<T>(
        string? name,
        Expression<Func<TValue, T>> property,
        Action<ObjectSchemaBuilder<T>> configure,
        Predicate<T?>? ignoreIf = null)
    {
        var builder = new ObjectSchemaBuilder<T>(_namingPolicy);
        configure(builder);

        return Property(name, property, builder.Build(), ignoreIf: ignoreIf);
    }

    /// <summary>
    /// Configures the schema to convert the specified object property with the schema configured
    /// accordingly, and ignore the property in its absence during decode.
    /// </summary>
    /// <param name="name">
    /// The name of the property. If <see langword="null"/>, uses the name of the property,
    /// conforming to the naming policy (if applicable).
    /// </param>
    /// <param name="property">An expression to the property.</param>
    /// <param name="configure">A function that configures the property.</param>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <returns>The current instance.</returns>
    public ObjectSchemaBuilder<TValue> OptionalObject<T>(
        string? name,
        Expression<Func<TValue, T?>> property,
        Action<ObjectSchemaBuilder<T>> configure)
    {
        var builder = new ObjectSchemaBuilder<T>(_namingPolicy);
        configure(builder);

        return Property(name, property, builder.Build(), optional: true);
    }

    /// <summary>
    /// Returns a new instance of <see cref="ObjectSchema{TValue}"/> containing the
    /// properties configured.
    /// </summary>
    /// <returns>The resulting schema.</returns>
    public ObjectSchema<TValue> Build()
    {
        var constMeta = _constants.Count > 0 ? _constants : null;
        return new ObjectSchema<TValue>(_meta, constMeta);
    }
}