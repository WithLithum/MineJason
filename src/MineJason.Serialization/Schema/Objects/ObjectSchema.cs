// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Objects;

using System.Reflection;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities;
using MineJason.Serialization.Utilities.Results;
using BindingFlags = System.Reflection.BindingFlags;

/// <summary>
/// Defines a schema that can be used to convert an object from or to a compound element type.
/// </summary>
/// <typeparam name="TValue">The value to convert from or to.</typeparam>
/// <typeparam name="TElement">
/// The element type that the compound type contains and is based upon.
/// </typeparam>
/// <seealso cref="ObjectSchemaBuilder{TValue,TElement}"/>
public class ObjectSchema<TValue> : IValueSchema<TValue>
{
    private readonly IReadOnlyDictionary<PropertySchema, IValueSchema> _meta;

    private readonly IReadOnlyDictionary<string, ValueWithSchema>? _constants;

    /// <summary>
    /// Initializes a new instance of <see cref="ObjectSchema{TValue,TElement}"/> with the
    /// specified properties configured.
    /// </summary>
    /// <param name="meta">The dictionary containing the configuration.</param>
    /// <param name="constants">The constants to insert when encoding.</param>
    public ObjectSchema(IReadOnlyDictionary<PropertySchema, IValueSchema> meta,
        IReadOnlyDictionary<string, ValueWithSchema>? constants = null)
    {
        _meta = meta;
        _constants = constants;
    }

    #region Encoder

    /// <summary>
    /// Converts the specified value to the specified element type by evaluating the configured
    /// property schemas.
    /// </summary>
    /// <param name="value">The value to encode.</param>
    /// <param name="encoder">The encoder to use.</param>
    /// <param name="elementName">The name of the element to output.</param>
    /// <returns>A data result containing either an error, or the conversion result.</returns>
    public Result<TElement> Encode<TElement>(TValue value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        var container = encoder.CreateObjectLike(elementName);

        // Encode constants.
        var constResult = EncodeConstantsInternal(container, encoder);
        if (!constResult.IsSuccess())
        {
            return constResult.AsError();
        }

        foreach (var (prop, schema) in _meta)
        {
            var (itemResult, hasValue) = EncodePropertyInternal(value, prop, schema, encoder,
                out var item);
            if (!itemResult)
            {
                return itemResult.AsError();
            }

            // Empty but successful result indicates optional property can be ignored.
            if (!hasValue)
            {
                continue;
            }

            var addResult = container.Add(prop.Name, item!);
            if (!addResult.IsSuccess())
            {
                return addResult.AsError();
            }
        }

        var cont = container.GetContainer();
        var res = cont;
        return res;
    }

    Result<TElement> IValueSchema.Encode<TElement>(object value, IValueEncoder<TElement> encoder,
        string? elementName)
    {
        if (value is not TValue obj)
        {
            throw new ArgumentException("Value must be of type TValue.",
                nameof(value));
        }

        return Encode(obj, encoder, elementName);
    }

    private Result EncodeConstantsInternal<TElement>(IWriteOnlyObjectLike<TElement> obj,
        IValueEncoder<TElement> encoder)
    {
        if (_constants == null)
        {
            return Result.Success();
        }

        foreach (var (key, value) in _constants)
        {
            var dataResult = value.Schema.Encode(value.Value, encoder, key);
            if (!dataResult.IsSuccess(out var v))
            {
                return dataResult.AsError();
            }

            var result = obj.Add(key, v);
            if (!result.IsSuccess())
            {
                return result;
            }
        }

        return Result.Success();
    }

    private static (Result, bool) EncodePropertyInternal<TElement>(TValue value,
        PropertySchema prop,
        IValueSchema schema,
        IValueEncoder<TElement> encoder,
        out TElement? element)
    {
        var accessor = prop.Property.GetMethod;
        if (accessor == null)
        {
            throw new ArgumentException($"The property '{prop.Property.Name}' is not get-able.");
        }

        // Transform!
        var getValue = accessor.Invoke(value, null);

        if (getValue == null)
        {
            element = default;

            if (prop.Optional)
            {
                return (Result.Success(), false);
            }

            return (Errors.MissingProperty(prop.Name), false);
        }

        // Evaluate the predicate. If predicate matches, ignore the value.
        if (prop.IgnoreIf != null && prop.IgnoreIf.Invoke(getValue))
        {
            element = default;
            return (Result.Success(), false);
        }

        var encodeResult = schema.Encode(getValue, encoder, prop.Name);
        if (!encodeResult.IsSuccess(out element))
        {
            element = default;
            return (encodeResult.AsError(), false);
        }

        return (Result.Success(), true);
    }

    #endregion

    #region Decoder

    private static TValue InstantiateValue()
    {
        var type = typeof(TValue);
        var constructor = type.GetConstructor(BindingFlags.Instance
                                              | BindingFlags.Public,
            null,
            CallingConventions.HasThis,
            [],
            null);

        if (constructor == null)
        {
            throw new InvalidOperationException($"Target type {type.Name} lacks a public parameterless constructor.");
        }

        return (TValue)constructor.Invoke(null);
    }

    /// <summary>
    /// Converts the specified element to the instance representation of the given type.
    /// </summary>
    /// <param name="value">The element to decode.</param>
    /// <param name="decoder">The decoder to use.</param>
    /// <returns>A result containing the decoder value or an error.</returns>
    /// <exception cref="InvalidOperationException">
    /// One of the properties being evaluated has no setter. -or-
    /// <typeparamref name="TValue"/> is a readonly value type.
    /// </exception>
    public Result<TValue> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        if (ReflectionHelper.IsReadOnlyStruct(typeof(TValue)))
        {
            throw new InvalidOperationException("Decoding is not support for readonly " +
                                                "value types.");
        }

        var valueResult = decoder.GetObjectLike(value);
        if (valueResult.Error != null)
        {
            return Errors.Error($"Failed to acquire container: {valueResult.Error}");
        }

        var container = valueResult.Value!;
        var retVal = InstantiateValue();

        foreach (var (property, schema) in _meta)
        {
            var setter = property.Property.SetMethod
                ?? throw new InvalidOperationException($"CLR property '{property.Property.Name}' has no setter.");

            // There existed corner cases where schema may become null for whatever reason.
            // Check here to report this correctly.
            if (schema == null)
            {
                throw new InvalidOperationException($"Property '{property.Name}' is assigned to a null schema.");
            }

            // Check if that value exists.
            var (itemResult, hasValue) = DecodePropertyInternal(property,
                schema,
                decoder,
                container,
                out var propValue);
            if (!itemResult)
            {
                return itemResult.AsError();
            }

            if (!hasValue)
            {
                continue;
            }

            // Invoke setter
            setter.Invoke(retVal, [propValue]);
        }

        return retVal;
    }

    private static (Result, bool) DecodePropertyInternal<TElement>(PropertySchema property,
        IValueSchema schema,
        IValueDecoder<TElement> decoder,
        IReadOnlyObjectLike<TElement> container,
        out object? value)
    {
        ArgumentNullException.ThrowIfNull(property);
        ArgumentNullException.ThrowIfNull(schema);
        ArgumentNullException.ThrowIfNull(decoder);
        ArgumentNullException.ThrowIfNull(container);

        // Check if that value exists.
        if (!container.ContainsKey(property.Name))
        {
            value = null;

            // Check if property is required, if it is, error!
            if (!property.Optional)
            {
                return (Errors.MissingProperty(property.Name), false);
            }

            return (Result.Success(), false);
        }

        var itemResult = container.Get(property.Name);
        if (itemResult.Error != null)
        {
            value = null;
            return (Errors.Error($"Read property failed: {itemResult.Error}"), false);
        }

        var itemValue = schema.Decode(itemResult.Value!, decoder);
        if (itemValue.Error != null)
        {
            value = null;
            return (Errors.Error($"Property '{property.Name}' value convert failed: " +
                                 $"{itemValue.Error}"), false);
        }

        value = itemValue.Value;
        return ((Result)itemValue, true);
    }

    Result<object> IValueSchema.Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var result = Decode(value, decoder);
        if (!result)
        {
            return result.AsError();
        }
        else
        {
            return result.Value!;
        }
    }

    #endregion
}