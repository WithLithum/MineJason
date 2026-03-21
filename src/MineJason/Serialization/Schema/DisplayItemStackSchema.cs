// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.Schema;

using MineJason.Data;
using MineJason.Serialization.IO;
using MineJason.Serialization.Schema.Objects;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Encodes or decodes <see cref="DisplayItemStack"/> to or from the given element type.
/// </summary>
public class DisplayItemStackSchema : ValueSchema<DisplayItemStack>
{
    private readonly ObjectSchema<DisplayItemStack> _parent;

    /// <summary>
    /// Gets the default singleton instance that does not support the encoding or decoding of
    /// data components.
    /// </summary>
    public static readonly DisplayItemStackSchema Default = new(null);

    /// <summary>
    /// Initializes a new instance of the <see cref="DisplayItemStackSchema"/> class.
    /// </summary>
    /// <param name="componentSchema">
    /// The schema that supports the component map. If <see langword="null"/>, the "components"
    /// property won't be serialized or deserialized.
    /// </param>
    public DisplayItemStackSchema(DataComponentMapSchema? componentSchema)
    {
        var builder = new ObjectSchemaBuilder<DisplayItemStack>()
            .ResourceLocation("id", x => x.Id)
            .Int32("count", x => x.Count);

        if (componentSchema != null)
        {
            builder.Property("components", x => x.Components,
                componentSchema,
                optional: true);
        }

        _parent = builder.Build();
    }

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(DisplayItemStack value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return _parent.Encode(value, encoder, elementName);
    }

    /// <inheritdoc />
    public override Result<DisplayItemStack> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        return _parent.Decode(value, decoder);
    }
}