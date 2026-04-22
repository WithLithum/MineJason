// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using MineJason.Dialogs;
using MineJason.Serialization.IO;
using MineJason.Serialization.Schema.Objects;
using MineJason.Serialization.Schema.Primitive;
using MineJason.Serialization.Utilities.Results;
using MineJason.Utilities;

/// <summary>
/// Encodes or decodes <see cref="DialogAction"/> inheritors to or from the given element type.
/// </summary>
public class DialogActionSchema : ValueSchema<DialogAction>
{
    /// <summary>
    /// The shared instance.
    /// </summary>
    public static readonly DialogActionSchema Instance = new();

    private DialogActionSchema()
    {
    }

    private static readonly ObjectSchema<DynamicCommandDialogAction> CommandSchema =
        new ObjectSchemaBuilder<DynamicCommandDialogAction>()
            .Constant("type", "minecraft:dynamic/run_command", StringValueSchema.Instance)
            .String("template", x => x.Template)
            .Build();

    private static readonly ObjectSchema<DynamicCustomDialogAction> CustomSchema =
        new ObjectSchemaBuilder<DynamicCustomDialogAction>()
            .Constant("type", "minecraft:dynamic/custom", StringValueSchema.Instance)
            .ResourceLocation("id", x => x.Id)
            .Build();

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(DialogAction value, IValueEncoder<TElement> encoder, string? elementName = null)
    {
        return value switch
        {
            DynamicCommandDialogAction command => CommandSchema.Encode(command, encoder,
                elementName),
            DynamicCustomDialogAction custom => CustomSchema.Encode(custom, encoder, elementName),
            StaticDialogAction st => ClickEventSchema.DialogInstance.Encode(st.Value,
                encoder,
                elementName),
            _ => Errors.Error("Unsupported dialog action")
        };
    }

    /// <inheritdoc />
    public override Result<DialogAction> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var obj = decoder.GetObjectLike(value);
        var typeKeyResult = obj.Get("type");
        if (!typeKeyResult.IsSuccess(out var typeKey))
        {
            return typeKeyResult.AsError();
        }

        var typeResult = ResourceLocationSchema.Instance.Decode(typeKey,
            decoder);
        if (!typeResult) return typeResult.AsError();
        var type = typeResult.Value;

        if (type.NameSpace != "minecraft")
        {
            return Errors.Error("Non-minecraft dialog actions are not supported");
        }

        return type.Path switch
        {
            "dynamic/run_command" => CommandSchema.Decode(value, decoder)
                .Cast<DynamicCommandDialogAction, DialogAction>(),
            "dynamic/custom" => CustomSchema.Decode(value, decoder)
                .Cast<DynamicCustomDialogAction, DialogAction>(),
            _ => DecodeStatic(value, decoder)
        };
    }

    private static Result<DialogAction> DecodeStatic<TElement>(TElement value,
        IValueDecoder<TElement> decoder)
    {
        var actionResult = ClickEventSchema.DialogInstance.Decode<TElement>(value, decoder);
        if (!actionResult) return actionResult.AsError();

        return new StaticDialogAction(actionResult.Value!);
    }
}