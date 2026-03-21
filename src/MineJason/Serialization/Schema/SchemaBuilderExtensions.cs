// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.Schema;

using System.Linq.Expressions;
using MineJason.Dialogs;
using MineJason.Serialization.Schema.Objects;

/// <summary>
/// Provides common extensions for object schema builders.
/// </summary>
public static class SchemaBuilderExtensions
{
    /// <summary>
    /// Configures the schema to read a <see cref="ResourceLocation"/> from the specified property,
    /// and optionally from the specified name.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The custom name of the property. If <see langword="null"/>, use the name of the property,
    /// with the specified naming policy for the builder.
    /// </param>
    /// <param name="expression">The expression to the property.</param>
    /// <typeparam name="TValue">The type of the object.</typeparam>
    /// <returns>The builder instance, passed in <paramref name="builder"/>.</returns>
    public static ObjectSchemaBuilder<TValue> ResourceLocation<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, ResourceLocation>> expression
    )
    {
        return builder.Property(name,
            expression,
            ResourceLocationSchema.Instance);
    }

    /// <summary>
    /// Configures the schema to read a <see cref="ResourceLocation"/> from the specified property,
    /// and optionally from the specified name.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The custom name of the property. If <see langword="null"/>, use the name of the property,
    /// with the specified naming policy for the builder.
    /// </param>
    /// <param name="expression">The expression to the property.</param>
    /// <typeparam name="TValue">The type of the object.</typeparam>
    /// <returns>The builder instance, passed in <paramref name="builder"/>.</returns>
    public static ObjectSchemaBuilder<TValue> OptionalResourceLocation<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, ResourceLocation?>> expression
    )
    {
        return builder.OptionalProperty(name,
            expression,
            ResourceLocationSchema.Instance);
    }

    /// <summary>
    /// Configures the schema to read a <see cref="ChatComponent"/> from the specified property,
    /// and optionally from the specified name.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The custom name of the property. If <see langword="null"/>, use the name of the property,
    /// with the specified naming policy for the builder.
    /// </param>
    /// <param name="expression">The expression to the property.</param>
    /// <typeparam name="TValue">The type of the object.</typeparam>
    /// <returns>The builder instance, passed in <paramref name="builder"/>.</returns>
    public static ObjectSchemaBuilder<TValue> TextComponent<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, ChatComponent?>> expression)
    {
        return builder.Property(name,
            expression,
            TextComponentSchema.Instance);
    }

    /// <summary>
    /// Configures the schema to read a <see cref="ChatComponent"/> from the specified property,
    /// and optionally from the specified name.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The custom name of the property. If <see langword="null"/>, use the name of the property,
    /// with the specified naming policy for the builder.
    /// </param>
    /// <param name="expression">The expression to the property.</param>
    /// <typeparam name="TValue">The type of the object.</typeparam>
    /// <returns>The builder instance, passed in <paramref name="builder"/>.</returns>
    public static ObjectSchemaBuilder<TValue> OptionalTextComponent<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, ChatComponent?>> expression)
    {
        return builder.Property(name,
            expression,
            TextComponentSchema.Instance,
            optional: true);
    }

    /// <summary>
    /// Configures the schema to read a <see cref="Dialogs.DialogButton"/> from the specified
    /// property, and optionally from the specified name.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The custom name of the property. If <see langword="null"/>, use the name of the property,
    /// with the specified naming policy for the builder.
    /// </param>
    /// <param name="expression">The expression to the property.</param>
    /// <typeparam name="TValue">The type of the object.</typeparam>
    /// <returns>The builder instance, passed in <paramref name="builder"/>.</returns>
    public static ObjectSchemaBuilder<TValue> DialogButton<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, DialogButton>> expression)
    {
        return builder.Property(name, expression, MineJasonSchemas.DialogButton);
    }

    /// <summary>
    /// Configures the schema to read a <see cref="Dialogs.DialogButton"/> from the specified
    /// property, and optionally from the specified name.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="name">
    /// The custom name of the property. If <see langword="null"/>, use the name of the property,
    /// with the specified naming policy for the builder.
    /// </param>
    /// <param name="expression">The expression to the property.</param>
    /// <typeparam name="TValue">The type of the object.</typeparam>
    /// <returns>The builder instance, passed in <paramref name="builder"/>.</returns>
    public static ObjectSchemaBuilder<TValue> OptionalDialogButton<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string? name,
        Expression<Func<TValue, DialogButton?>> expression)
    {
        return builder.Property(name, expression, MineJasonSchemas.DialogButton,
            optional: true);
    }
}