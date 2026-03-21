// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.Schema;

using System.Text.Json;
using MineJason.Data.Profile;
using MineJason.Dialogs;
using MineJason.Dialogs.Input;
using MineJason.Serialization.Schema.Objects;

/// <summary>
/// Contains reusable schema instances of the given element type.
/// </summary>
public static class MineJasonSchemas
{
    /// <summary>
    /// The schema for <see cref="ProfileProperty"/>.
    /// </summary>
    public static readonly ObjectSchema<ProfileProperty> ProfileProperty =
        new ObjectSchemaBuilder<ProfileProperty>()
            .String("name", x => x.Name)
            .String("value", x => x.Value)
            .OptionalString("signature", x => x.Signature)
            .Build();

    /// <summary>
    /// A collective schema for <see cref="PlayerProfile"/>.
    /// </summary>
    public static readonly OneOfValueSchema<PlayerProfile> PlayerProfile =
    new(
    [
        new ObjectSchemaBuilder<PlayerProfile>()
            .OptionalString("name", x => x.Name)
            .OptionalProperty("id", x => x.Id,
                MinecraftUuidSchema.Instance)
            .OptionalResourceLocation("texture", x => x.Texture)
            .OptionalResourceLocation("cape", x => x.Cape)
            .OptionalProperty("model", x => x.Model,
                new StringEnumValueSchema<PlayerModel>(JsonNamingPolicy.SnakeCaseLower))
            .Property("properties", x => x.Properties,
                new CollectionSchema<ProfileProperty>(ProfileProperty),
                optional: true)
            .Build(),
        new StringPlayerProfileSchema()
    ]);

    /// <summary>
    /// The schema implementation for <see cref="ItemDialogMessage.DescriptionLabel"/>.
    /// </summary>
    public static readonly OneOfValueSchema<ItemDialogMessage.DescriptionLabel>
        ItemDialogDescription = new(
            [
                new ObjectSchemaBuilder<ItemDialogMessage.DescriptionLabel>()
                    .TextComponent("contents", x => x.Contents)
                    .Int32("width", x => x.Width, x => x == 0)
                    .Build(),
                new DialogSimpleItemDescriptionSchema()
            ]
        );

    /// <summary>
    /// The schema for <see cref="SingleOptionDialogInput.Item"/>.
    /// </summary>
    public static readonly ObjectSchema<SingleOptionDialogInput.Item>
        SingleOptionInputItem = new ObjectSchemaBuilder<SingleOptionDialogInput.Item>()
            .String("id", x => x.Id)
            .OptionalTextComponent("display", x => x.Display)
            .OptionalBoolean("initial", x => x.Initial)
            .Build();

    /// <summary>
    /// The schema for <see cref="DialogMessage"/> collection.
    /// </summary>
    public static readonly OneOfValueSchema<IReadOnlyCollection<DialogMessage>>
        DialogMessageList = new([
            new CollectionSchema<DialogMessage>(DialogMessageSchema.Instance),
            new ListOfOneSchema<DialogMessage>(DialogMessageSchema.Instance)
        ]);

    /// <summary>
    /// The schema for <see cref="DialogButton"/>.
    /// </summary>
    public static readonly ObjectSchema<DialogButton> DialogButton =
        new ObjectSchemaBuilder<DialogButton>()
            .TextComponent("label", x => x.Label)
            .OptionalTextComponent("tooltip", x => x.Tooltip)
            .OptionalInt32("width", x => x.Width)
            .Property("action", x => x.Action, DialogActionSchema.Instance,
                optional: true)
            .Build();
}