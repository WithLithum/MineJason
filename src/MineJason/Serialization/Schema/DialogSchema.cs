// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using MineJason.Dialogs;
using MineJason.Serialization.IO;
using MineJason.Serialization.Schema.Objects;
using MineJason.Serialization.Utilities.Results;
using MineJason.Utilities;

/// <summary>
/// Encodes or decodes <see cref="Dialog"/> descendants.
/// </summary>
public sealed class DialogSchema : ValueSchema<Dialog>
{
    private static readonly ObjectSchema<NoticeDialog> NoticeSchema =
        new ObjectSchemaBuilder<NoticeDialog>()
            .CommonDialogSchema(NoticeDialog.Id)
            .OptionalDialogButton("action", x => x.Action)
            .Build();

    private static readonly ObjectSchema<ConfirmationDialog> ConfirmationSchema =
        new ObjectSchemaBuilder<ConfirmationDialog>()
            .CommonDialogSchema(ConfirmationDialog.Id)
            .DialogButton("yes", x => x.Yes)
            .DialogButton("no", x => x.No)
            .Build();

    private static readonly ObjectSchema<MultiActionDialog> MultiActionSchema =
        new ObjectSchemaBuilder<MultiActionDialog>()
            .CommonDialogSchema(MultiActionDialog.Id)
            .Property("actions", x => x.Actions,
                new CollectionSchema<DialogButton>(MineJasonSchemas.DialogButton))
            .OptionalInt32("columns", x => x.Columns)
            .OptionalDialogButton("exit_action", x => x.ExitAction)
            .Build();

    private static readonly ObjectSchema<ServerLinksDialog> ServerLinksSchema =
        new ObjectSchemaBuilder<ServerLinksDialog>()
            .CommonDialogSchema(ServerLinksDialog.Id)
            .OptionalDialogButton("exit_action", x => x.ExitAction)
            .OptionalInt32("columns", x => x.Columns)
            .OptionalInt32("button_width", x => x.ButtonWidth)
            .Build();

    // TODO dialog list dialog

    /// <summary>
    /// The shared instance.
    /// </summary>
    public static readonly DialogSchema Instance = new();

    private DialogSchema()
    {
    }

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(Dialog value, IValueEncoder<TElement> encoder, string? elementName = null)
    {
        return value switch
        {
            NoticeDialog notice => NoticeSchema.Encode(notice, encoder, elementName),
            ConfirmationDialog confirm => ConfirmationSchema.Encode(confirm, encoder, elementName),
            MultiActionDialog multi => MultiActionSchema.Encode(multi, encoder, elementName),
            ServerLinksDialog links => ServerLinksSchema.Encode(links, encoder, elementName),
            _ => MyResults.DoesNotKnowTypeEncode(value.GetType().ToString())
        };
    }

    /// <inheritdoc />
    public override Result<Dialog> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var idResult = ResourceLocationSchema.Instance.Decode(decoder
                .GetObjectLike(value)
                .Get("type"),
            decoder);
        if (!idResult)
        {
            return idResult.AsError();
        }

        var id = idResult.Value;
        if (id.NameSpace != "minecraft")
        {
            return MyResults.NotMinecraftNamespace;
        }

        return id.Path switch
        {
            "notice" => NoticeSchema.Decode(value, decoder).Cast<NoticeDialog, Dialog>(),
            "confirmation" => ConfirmationSchema.Decode(value, decoder)
                .Cast<ConfirmationDialog, Dialog>(),
            "multi_action" => MultiActionSchema.Decode(value, decoder)
                .Cast<MultiActionDialog, Dialog>(),
            "server_links" => ServerLinksSchema.Decode(value, decoder)
                .Cast<ServerLinksDialog, Dialog>(),
            _ => MyResults.DoesNotKnowTypeDecode(id.ToString())
        };
    }
}