// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.Schema;

using MineJason.Events;
using MineJason.Serialization.IO;
using MineJason.Serialization.Schema.Objects;
using MineJason.Serialization.Schema.Primitive;
using MineJason.Serialization.Utilities.Results;
using MineJason.Text.Behaviour.Click;

/// <summary>
/// Defines the schema that converts <see cref="ClickEvent"/> to or from the given element type.
/// </summary>
public class ClickEventSchema : ValueSchema<ClickEvent>
{
    /// <summary>
    /// The singleton instance for use with text components.
    /// </summary>
    public static readonly ClickEventSchema TextInstance = new();

    /// <summary>
    /// The singleton instance for use with dialogs.
    /// </summary>
    public static readonly ClickEventSchema DialogInstance = new("type");

    /// <summary>
    /// Initializes a new instance of <see cref="ClickEventSchema"/> with the specified
    /// type field name.
    /// </summary>
    /// <param name="typeFieldName">The name of the field indicating the type.</param>
    public ClickEventSchema(string typeFieldName = "action")
    {
        _typeFieldName = typeFieldName;

        _openUrl =
            new ObjectSchemaBuilder<OpenUrlClickEvent>()
                .Constant(typeFieldName, "open_url", StringValueSchema.Instance)
                .Property("url", x => x.Value, UriSchema.Instance)
                .Build();
        _runCommand =
            new ObjectSchemaBuilder<RunCommandClickEvent>()
                .Constant(typeFieldName, "run_command", StringValueSchema.Instance)
                .String("command", x => x.Value)
                .Build();
        _suggestCommand =
            new ObjectSchemaBuilder<SuggestCommandClickEvent>()
                .Constant(typeFieldName, "suggest_command", StringValueSchema.Instance)
                .String("command", x => x.Value)
                .Build();
        _changePage =
            new ObjectSchemaBuilder<ChangePageClickEvent>()
                .Constant(typeFieldName, "change_page", StringValueSchema.Instance)
                .Int32("page", x => x.Value)
                .Build();
        _copyToClipboard =
            new ObjectSchemaBuilder<CopyToClipboardClickEvent>()
                .Constant(typeFieldName, "copy_to_clipboard",
                    StringValueSchema.Instance)
                .String("value", x => x.Value)
                .Build();
        _custom =
            new ObjectSchemaBuilder<CustomClickEvent>()
                .Constant(typeFieldName, "custom", StringValueSchema.Instance)
                .ResourceLocation("id", x => x.Id)
                .OptionalString("payload", x => x.Payload)
                .Build();
    }

    private readonly string _typeFieldName;
    private readonly ObjectSchema<OpenUrlClickEvent> _openUrl;
    private readonly ObjectSchema<RunCommandClickEvent> _runCommand;
    private readonly ObjectSchema<SuggestCommandClickEvent> _suggestCommand;
    private readonly ObjectSchema<ChangePageClickEvent> _changePage;
    private readonly ObjectSchema<CopyToClipboardClickEvent> _copyToClipboard;

    // TODO: show dialog codec once dialog is written

    private readonly ObjectSchema<CustomClickEvent> _custom;

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(ClickEvent value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return value switch
        {
            OpenUrlClickEvent openUrl => _openUrl.Encode(openUrl, encoder, elementName),
            RunCommandClickEvent runCommand => _runCommand.Encode(runCommand, encoder, elementName),
            SuggestCommandClickEvent suggest => _suggestCommand.Encode(suggest, encoder,
                elementName),
            ChangePageClickEvent changePage => _changePage.Encode(changePage, encoder, elementName),
            CopyToClipboardClickEvent clipboard => _copyToClipboard.Encode(clipboard, encoder,
                elementName),
            // TODO show dialog
            CustomClickEvent custom => _custom.Encode(custom, encoder, elementName),
            _ => Errors.Error("Unrecognised click event type")
        };
    }

    /// <inheritdoc />
    public override Result<ClickEvent> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var objResult = decoder.GetObjectLike(value);
        if (!objResult.IsSuccess(out var obj)) return objResult.AsError();

        var keyResult = GetKey(obj, _typeFieldName, decoder);
        if (!keyResult.IsSuccess(out var key)) keyResult.AsError();

        static Result<ClickEvent> Up<TFrom>(Result<TFrom> from) where TFrom : ClickEvent
        {
            if (!from.IsSuccess(out var r))
            {
                return from.AsError();
            }

            return r;
        }

        return key switch
        {
            "open_url" => Up(_openUrl.Decode(value, decoder)),
            "run_command" => Up(_runCommand.Decode(value, decoder)),
            "suggest_command" => Up(_suggestCommand.Decode(value, decoder)),
            "change_page" => Up(_changePage.Decode(value, decoder)),
            "copy_to_clipboard" => Up(_copyToClipboard.Decode(value, decoder)),
            "custom" => Up(_custom.Decode(value, decoder)),
            _ => Errors.Error($"Unknown click action {key}")
        };
    }

    private static Result<string> GetKey<TElement>(IReadOnlyObjectLike<TElement> obj,
        string key,
        IValueDecoder<TElement> decoder)
    {
        return decoder.GetString(obj.Get(key));
    }
}