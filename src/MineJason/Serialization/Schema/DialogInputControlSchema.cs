// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.Schema;

using MineJason.Dialogs.Input;
using MineJason.Serialization.IO;
using MineJason.Serialization.Schema.Objects;
using MineJason.Serialization.Utilities.Results;

/// <summary>
/// Encodes or decodes <see cref="DialogInput"/> inheitors to or from the given element type.
/// </summary>
public class DialogInputControlSchema : ValueSchema<DialogInput>
{
    private DialogInputControlSchema()
    {
    }

    /// <summary>
    /// The singleton instance.
    /// </summary>
    public static readonly DialogInputControlSchema Instance = new();

    private static readonly ObjectSchema<TextDialogInput> TextSchema =
        new ObjectSchemaBuilder<TextDialogInput>()
            .CommonDialogInputSchema(TextDialogInput.Id)
            .OptionalInt32("width", x => x.Width)
            .OptionalBoolean("label_visible", x => x.LabelVisible)
            .OptionalString("initial", x => x.Initial)
            .OptionalInt32("max_length", x => x.MaxLength)
            .OptionalObject("multiline", x => x.Multiline,
                cfg => cfg
                    .OptionalInt32("max_lines", x => x.MaxLines)
                    .OptionalInt32("height", x => x.Height))
            .Build();

    private static readonly ObjectSchema<BooleanDialogInput> BooleanSchema =
        new ObjectSchemaBuilder<BooleanDialogInput>()
            .CommonDialogInputSchema(BooleanDialogInput.Id)
            .OptionalBoolean("initial", x => x.Initial)
            .OptionalString("on_true", x => x.OnTrue)
            .OptionalString("on_false", x => x.OnFalse)
            .Build();

    private static readonly ObjectSchema<SingleOptionDialogInput> SingleOptionSchema =
        new ObjectSchemaBuilder<SingleOptionDialogInput>()
            .CommonDialogInputSchema(SingleOptionDialogInput.Type)
            .OptionalBoolean("label_visible", x => x.LabelVisible)
            .OptionalInt32("width", x => x.Width)
            .Property("options", x => x.Options,
                new CollectionSchema<SingleOptionDialogInput.Item>(MineJasonSchemas
                    .SingleOptionInputItem))
            .Build();

    private static readonly ObjectSchema<NumberRangeDialogInput> NumberRangeSchema =
        new ObjectSchemaBuilder<NumberRangeDialogInput>()
            .CommonDialogInputSchema(NumberRangeDialogInput.Id)
            .OptionalString("label_format", x => x.LabelFormat)
            .OptionalInt32("width", x => x.Width)
            .Single("start", x => x.Start)
            .Single("end", x => x.End)
            .OptionalSingle("step", x => x.Step)
            .OptionalInt32("initial", x => x.Initial)
            .Build();

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(DialogInput value, IValueEncoder<TElement> encoder, string? elementName = null)
    {
        return value switch
        {
            TextDialogInput text => TextSchema.Encode(text, encoder, elementName),
            BooleanDialogInput boolean => BooleanSchema.Encode(boolean, encoder, elementName),
            SingleOptionDialogInput option => SingleOptionSchema.Encode(option, encoder,
                elementName),
            NumberRangeDialogInput slider => NumberRangeSchema.Encode(slider, encoder, elementName),
            _ => Errors.Error("Unsupported dialog input type")
        };
    }

    /// <inheritdoc />
    public override Result<DialogInput> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var objResult = decoder.GetObjectLike(value);
        if (!objResult.IsSuccess(out var obj))
        {
            return objResult.AsError();
        }

        var typeResult = ResourceLocationSchema.Instance.Decode(obj.Get("type"),
            decoder);
        if (!typeResult.IsSuccess(out var type))
        {
            return typeResult.AsError();
        }

        if (type == TextDialogInput.Id)
            return TextSchema.Decode(value, decoder).Cast<TextDialogInput, DialogInput>();

        if (type == BooleanDialogInput.Id)
            return BooleanSchema.Decode(value, decoder).Cast<BooleanDialogInput, DialogInput>();

        if (type == SingleOptionDialogInput.Type)
            return SingleOptionSchema.Decode(value, decoder).Cast<SingleOptionDialogInput,
                DialogInput>();

        if (type == NumberRangeDialogInput.Id)
            return NumberRangeSchema.Decode(value, decoder).Cast<NumberRangeDialogInput,
                DialogInput>();

        return Errors.Error($"Unrecognised dialog input type '{type}'");
    }
}