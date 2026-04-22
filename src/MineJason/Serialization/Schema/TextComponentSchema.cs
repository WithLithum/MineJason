// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

using MineJason.Serialization.IO;
using MineJason.Serialization.Schema.Objects;
using MineJason.Serialization.Schema.Primitive;
using MineJason.Serialization.Utilities.Results;
using MineJason.Text;
using MineJason.Utilities;

namespace MineJason.Serialization.Schema;

/// <summary>
/// Defines a schema that encodes or decodes text components to or from the given element type. 
/// </summary>
public partial class TextComponentSchema : ValueSchema<TextComponent>
{
    /// <summary>
    /// The singleton instance with default settings that do not support items.
    /// </summary>
    public static readonly TextComponentSchema Instance = new();

    private TextComponentSchema()
    {
    }

    private static readonly ObjectSchema<LiteralTextComponent> LiteralSchema
        = new ObjectSchemaBuilder<LiteralTextComponent>()
            .Constant("type", "text", StringValueSchema.Instance)
            .String("text", x => x.Text)
            .CommonTextComponentSchema()
            .Build();

    private static readonly ObjectSchema<TranslateTextComponent> TranslatableSchema
        = new ObjectSchemaBuilder<TranslateTextComponent>()
            .Constant("type", "translatable", StringValueSchema.Instance)
            .String("translate", x => x.Translate)
            .OptionalString("fallback", x => x.Fallback)
            .Property("with", x => x.With!,
                TextComponentListSchema.Instance, optional: true)
            .CommonTextComponentSchema()
            .Build();

    private static readonly ObjectSchema<ScoreTextComponent> ScoreSchema
        = new ObjectSchemaBuilder<ScoreTextComponent>()
            .Constant("type", "score", StringValueSchema.Instance)
            .Object("score", x => x.Score,
                cfg =>
                    cfg.String("name", x => x.Name)
                        .String("objective", x => x.Objective))
            .Build();

    private static readonly ObjectSchema<EntityTextComponent> SelectorSchema
        = new ObjectSchemaBuilder<EntityTextComponent>()
            .Constant("type", "selector", StringValueSchema.Instance)
            .Property("selector", x => x.Selector,
                StringValueSchema.Instance)
            .Property("separator", x => x.Separator,
                Instance, optional: true)
            .Build();

    private static readonly ObjectSchema<KeybindTextComponent> KeybindSchema
        = new ObjectSchemaBuilder<KeybindTextComponent>()
            .Constant("type", "keybind", StringValueSchema.Instance)
            .String("keybind", x => x.Keybind)
            .CommonTextComponentSchema()
            .Build();

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(TextComponent value,
        IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        return value switch
        {
            LiteralTextComponent literal => LiteralSchema.Encode(literal, encoder, elementName),
            TranslateTextComponent translate => TranslatableSchema.Encode(translate,
                encoder,
                elementName),
            ScoreTextComponent score => ScoreSchema.Encode(score, encoder, elementName),
            EntityTextComponent selector => SelectorSchema.Encode(selector, encoder, elementName),
            KeybindTextComponent keybind => KeybindSchema.Encode(keybind, encoder, elementName),
            BlockNbtTextComponent blockNbt => NbtBlockSchema.Encode(blockNbt, encoder, elementName),
            EntityNbtTextComponent entityNbt =>
                NbtEntitySchema.Encode(entityNbt, encoder, elementName),
            StorageNbtTextComponent storageNbt =>
                NbtStorageSchema.Encode(storageNbt, encoder, elementName),
            AtlasObjectTextComponent atlasObject =>
                AtlasSpriteSchema.Encode(atlasObject, encoder, elementName),
            PlayerObjectTextComponent playerObject =>
                PlayerSpriteSchema.Encode(playerObject, encoder, elementName),
            _ => Errors.Error("Unsupported component type")
        };
    }

    /// <inheritdoc />
    public override Result<TextComponent> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        ArgumentNullException.ThrowIfNull(value);

        // Attempts to parse a string-based literal text component.
        var strResult = decoder.GetString(value);
        if (strResult.IsSuccess(out var str))
        {
            return new LiteralTextComponent(str);
        }

        // If fails, attempts to parse collection based components.
        var colResult = decoder.GetCollection(value);
        if (colResult.IsSuccess(out var col))
        {
            return DecodeCollection(col, decoder);
        }

        var objResult = decoder.GetObjectLike(value);

        var keyParse = decoder.GetString(objResult.Get("type"));

        if (!objResult.IsSuccess(out var o))
        {
            return objResult.AsError();
        }

        return keyParse.Value switch
        {
            "text" when o.ContainsKey("text") => CastResult(LiteralSchema.Decode(value, decoder)),
            "translatable" when o.ContainsKey("translate") => CastResult(
                TranslatableSchema.Decode(value, decoder)),
            "score" when o.ContainsKey("score") => CastResult(ScoreSchema.Decode(value, decoder)),
            "selector" when o.ContainsKey("selector") => CastResult(
                SelectorSchema.Decode(value, decoder)),
            "keybind" when o.ContainsKey("keybind") => CastResult(
                KeybindSchema.Decode(value, decoder)),
            "nbt" when o.ContainsKey("nbt") => DecodeNbtInternal(value, o, decoder),
            "object" when o.ContainsKey("object") => DecodeSpriteInternal(o, value,
                decoder),
            _ => DecodeByProperty(value, o, decoder)
        };
    }

    private Result<TextComponent> DecodeCollection<TElement>(IEnumerable<TElement> collection,
        IValueDecoder<TElement> decoder)
    {
        TextComponent? first = null;
        List<TextComponent>? extras = null;

        foreach (var col in collection)
        {
            // Parse first.
            var itemResult = Decode(col, decoder);
            if (!itemResult.IsSuccess(out var item))
            {
                return itemResult.AsError();
            }

            if (first == null)
            {
                first = item;
                continue;
            }

            extras ??= [];
            extras.Add(item);
        }

        first ??= TextComponent.CreateText("");
        return first with { Extra = extras };
    }

    private static Result<TextComponent> DecodeByProperty<TElement>(TElement value,
        IReadOnlyObjectLike<TElement> o,
        IValueDecoder<TElement> decoder)
    {
        return o switch
        {
            _ when o.ContainsKey("text") => CastResult(LiteralSchema.Decode(value, decoder)),
            _ when o.ContainsKey("translate") => CastResult(TranslatableSchema.Decode(value,
                decoder)),
            _ when o.ContainsKey("score") => CastResult(ScoreSchema.Decode(value, decoder)),
            _ when o.ContainsKey("selector") => CastResult(SelectorSchema.Decode(value, decoder)),
            _ when o.ContainsKey("keybind") => CastResult(KeybindSchema.Decode(value, decoder)),
            _ when o.ContainsKey("nbt") => DecodeNbtInternal(value, o, decoder),
            _ when o.ContainsKey("object") || o.ContainsKey("sprite") =>
                DecodeSpriteInternal(o, value, decoder),

            _ => Errors.Error("Unsupported or malformed component")
        };
    }

    private static Result<TextComponent> CastResult<TChild>(Result<TChild> from)
        where TChild : TextComponent
    {
        return from.Cast<TChild, TextComponent>();
    }
}