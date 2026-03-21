// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.Schema;

using MineJason.Events.Hover;
using MineJason.Serialization.IO;
using MineJason.Serialization.Schema.Objects;
using MineJason.Serialization.Schema.Primitive;
using MineJason.Serialization.Utilities.Results;
using ShowItemHoverEvent = MineJason.Text.Behaviour.Hover.ShowItemHoverEvent;

/// <summary>
/// Defines the schema that encodes or decodes <see cref="HoverEvent"/> to or from the given
/// element type.
/// </summary>
public class HoverEventSchema : ValueSchema<HoverEvent>
{
    private const string ActionKey = "action";

    private static readonly ObjectSchema<ShowTextHoverEvent> ShowTextSchema =
        new ObjectSchemaBuilder<ShowTextHoverEvent>()
            .Constant(ActionKey, "show_text", StringValueSchema.Instance)
            .TextComponent("value", x => x.Contents)
            .Build();

    private static readonly ObjectSchema<ShowEntityHoverEvent> ShowEntitySchema =
        new ObjectSchemaBuilder<ShowEntityHoverEvent>()
            .Constant(ActionKey, "show_entity", StringValueSchema.Instance)
            .OptionalTextComponent("name", x => x.Name)
            .ResourceLocation("id", x => x.Type)
            .Property("uuid", x => x.Id,
                new OneOfValueSchema<Guid>([
                    MinecraftUuidSchema.Instance,
                    StringGuidSchema.Instance
                ]))
            .Build();

    private static readonly ObjectSchema<ShowItemHoverEvent> ShowItemSchema =
        new ObjectSchemaBuilder<ShowItemHoverEvent>()
        .Constant(ActionKey, "show_item", StringValueSchema.Instance)
        .ResourceLocation("id", x => x.Id)
        .OptionalInt32("count", x => x.Count)
        .Property("components", x => x.Components,
            new DataComponentMapSchema())
        .Build();

    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static readonly HoverEventSchema Instance = new();

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(HoverEvent value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return value switch
        {
            ShowTextHoverEvent showText => ShowTextSchema.Encode(showText, encoder, elementName),
            ShowEntityHoverEvent showEntity => ShowEntitySchema.Encode(showEntity,
                encoder,
                elementName),
            ShowItemHoverEvent showItem => ShowItemSchema.Encode(showItem, encoder, elementName),
            _ => Errors.Error("Unsupported hover event type")
        };
    }

    /// <inheritdoc />
    public override Result<HoverEvent> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var objResult = decoder.GetObjectLike(value);
        if (!objResult.IsSuccess(out var obj))
        {
            return objResult.AsError();
        }

        var keyResult = decoder.GetString(obj.Get(ActionKey));
        if (!keyResult) return keyResult.AsError();

        var key = keyResult.Value!;

        return key switch
        {
            "show_text" => ShowTextSchema.Decode(value, decoder)
                .Cast<ShowTextHoverEvent, HoverEvent>(),
            "show_entity" => ShowEntitySchema.Decode(value, decoder)
                .Cast<ShowEntityHoverEvent, HoverEvent>(),
            "show_item" => ShowItemSchema.Decode(value, decoder)
                .Cast<ShowItemHoverEvent, HoverEvent>(),
            _ => Errors.Error($"Unsupported action type '{key}'")
        };
    }
}