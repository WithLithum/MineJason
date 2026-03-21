// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using System.Text.Json;
using MineJason.Components;
using MineJason.Dialogs;
using MineJason.Dialogs.Input;
using MineJason.Serialization.IO;
using MineJason.Serialization.IO.Json;
using MineJason.Serialization.Schema.Objects;
using MineJason.Serialization.Schema.Primitive;
using MineJason.Serialization.Utilities.Results;
using MineJason.Text;

namespace MineJason.Serialization.Schema;
internal static class SchemaCommons
{
    internal static readonly JsonNodeEncoder JsonEncoder = new();
    internal static readonly JsonElementDecoder JsonDecoder = new();

    internal static Result<T2> Cast<T1, T2>(this Result<T1> from)
        where T1 : T2
    {
        if (!from.IsSuccess(out var value))
        {
            return from.AsError();
        }

        return value;
    }

    internal static ObjectSchemaBuilder<TValue> CommonTextComponentSchema<TValue>(
        this ObjectSchemaBuilder<TValue> builder)
    where TValue : ChatComponent
    {
        return builder.Property("color", x => x.Color,
                TextColorSchema.Instance,
                optional: true)
            .OptionalProperty("shadow_color", x => x.ShadowColor,
                ArgbColorSchema.Instance)
            .OptionalBoolean("bold", x => x.Bold)
            .OptionalBoolean("italic", x => x.Italic)
            .OptionalBoolean("underlined", x => x.Underline)
            .OptionalBoolean("strikethrough", x => x.Strikethrough)
            .OptionalBoolean("obfuscated", x => x.Obfuscated)
            .OptionalResourceLocation("font", x => x.Font)
            .OptionalString("insertion", x => x.Insertion)
            .Property("click_event", x => x.ClickEvent,
                ClickEventSchema.TextInstance,
                optional: true)
            .Property("hover_event", x => x.HoverEvent,
                new HoverEventSchema(),
                optional: true)
            .Property("extra", x => x.Extra,
                TextComponentListSchema.Instance,
                optional: true);
    }

    internal static ObjectSchemaBuilder<TValue> CommonNbtTextComponentSchema<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        string sourceType)
        where TValue : BaseNbtChatComponent
    {
        return builder.Constant("type", "nbt", StringValueSchema.Instance)
            .Constant("source", sourceType, StringValueSchema.Instance)
            .String("nbt", x => x.Path)
            .Property("interpret", x => x.Interpret, BooleanSchema.Instance,
                optional: true,
                ignoreIf: x => !x)
            .OptionalBoolean("plain", x => x.Plain, ignoreIf: x => x != true)
            .Property("separator",
                x => x.Separator,
                TextComponentSchema.Instance,
                optional: true);
    }

    internal static ObjectSchemaBuilder<T> CommonObjectTextComponentSchema<T>(
        this ObjectSchemaBuilder<T> schema,
        string type)
        where T : ObjectTextComponent
    {
        return schema
            .Constant("type", "object", StringValueSchema.Instance)
            .Constant("object", type, StringValueSchema.Instance)
            .CommonTextComponentSchema()
            .OptionalTextComponent("fallback", x => x.Fallback);
    }

    internal static ObjectSchemaBuilder<TValue> CommonDialogInputSchema<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        ResourceLocation sourceType)
        where TValue : DialogInput
    {
        return builder.Constant("type", sourceType, ResourceLocationSchema.Instance)
            .String("key", x => x.Key)
            .TextComponent("label", x => x.Label);
    }

    internal static Result<string> RequireStringValue<TElement>(this Result<TElement> data,
        IValueDecoder<TElement> decoder)
    {
        if (!data.IsSuccess(out var str)) return data.AsError();

        return decoder.GetString(str);
    }

    internal static ObjectSchemaBuilder<TValue> CommonDialogSchema<TValue>(
        this ObjectSchemaBuilder<TValue> builder,
        ResourceLocation sourceType)
        where TValue : Dialog
    {
        return builder.Constant("type", sourceType, ResourceLocationSchema.Instance)
            .TextComponent("title", x => x.Title)
            .OptionalTextComponent("external_title", x => x.ExternalTitle)
            .Property("body", x => x.Body, MineJasonSchemas.DialogMessageList,
                optional: true)
            .Property("inputs", x => x.Inputs,
                new CollectionSchema<DialogInput>(
                    DialogInputControlSchema.Instance),
                optional: true)
            .OptionalBoolean("can_close_with_escape", x => x.CanCloseWithEscape)
            .OptionalBoolean("pause", x => x.Pause)
            .OptionalProperty("after_action", x => x.AfterAction,
                new StringEnumValueSchema<DialogAfterAction>(
                    JsonNamingPolicy.SnakeCaseLower));
    }
}