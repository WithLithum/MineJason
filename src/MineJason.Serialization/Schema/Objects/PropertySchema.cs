// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Objects;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

public record PropertySchema
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertySchema"/> class.
    /// </summary>
    public PropertySchema()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="PropertySchema"/> with the specified property and
    /// configures whether the property is required.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="property">The property to bind.</param>
    /// <param name="optional">If <see langword="true"/>, the property is optional.</param>
    /// <param name="ignoreIf">A predicate that causes the value to be omitted if met.</param>
    [SetsRequiredMembers]
    public PropertySchema(string name, PropertyInfo property, bool optional,
        Predicate<object>? ignoreIf)
    {
        Name = name;
        Property = property;
        Optional = optional;
        IgnoreIf = ignoreIf;
    }

    /// <summary>
    /// Gets a value indicating whether the property is optional.
    /// </summary>
    public bool Optional { get; init; }

    public required string Name { get; init; }

    /// <summary>
    /// Gets the property associated with this schema.
    /// </summary>
    public required PropertyInfo Property { get; init; }

    public Predicate<object>? IgnoreIf { get; init; }
}