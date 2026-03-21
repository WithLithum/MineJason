// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.Schema;

using MineJason.Dialogs;
using MineJason.Serialization.IO;
using MineJason.Serialization.Schema.Objects;
using MineJason.Serialization.Schema.Primitive;
using MineJason.Serialization.Utilities.Results;
using MineJason.Utilities;

/// <summary>
/// Encodes or decodes <see cref="DialogMessage"/> inheritors to or from the given element type.
/// </summary>
public class DialogMessageSchema : ValueSchema<DialogMessage>
{
    private DialogMessageSchema()
    {
    }

    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static readonly DialogMessageSchema Instance = new();

    private static readonly ObjectSchema<PlainDialogMessage> PlainSchema =
        new ObjectSchemaBuilder<PlainDialogMessage>()
            .Constant("type", "minecraft:plain_message", StringValueSchema.Instance)
            .TextComponent("contents", x => x.Contents)
            .OptionalInt32("width", x => x.Width, ignoreIf: x => x == 0)
            .Build();

    private static readonly ObjectSchema<ItemDialogMessage> ItemSchema =
        new ObjectSchemaBuilder<ItemDialogMessage>()
            .Constant("type", "minecraft:item", StringValueSchema.Instance)
            .Property("item", x => x.Item,
                DisplayItemStackSchema.Default)
            .Property("description", x => x.Description,
                MineJasonSchemas.ItemDialogDescription, optional: true)
            .Build();

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(DialogMessage value, IValueEncoder<TElement> encoder, string? elementName = null)
    {
        return value switch
        {
            PlainDialogMessage plain => PlainSchema.Encode(plain, encoder, elementName),
            ItemDialogMessage item => ItemSchema.Encode(item, encoder, elementName),
            _ => Errors.Error("Unsupported dialog message type")
        };
    }

    /// <inheritdoc />
    public override Result<DialogMessage> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var obj = decoder.GetObjectLike(value);
        var typeResult = ResourceLocationSchema.Instance.Decode(obj.Get("type"),
            decoder);
        if (!typeResult.IsSuccess(out var type)) return typeResult.AsError();

        if (type == PlainDialogMessage.Type)
        {
            return PlainSchema.Decode(value, decoder).Cast<PlainDialogMessage, DialogMessage>();
        }

        if (type == ItemDialogMessage.Type)
        {
            return ItemSchema.Decode(value, decoder).Cast<ItemDialogMessage, DialogMessage>();
        }

        return Errors.Error($"Unrecognised dialog message type '{type}'");
    }
}