// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Serialization.IO;
using MineJason.Serialization.Schema.Objects;
using MineJason.Serialization.Schema.Primitive;
using MineJason.Serialization.Utilities.Results;
using MineJason.Text;

namespace MineJason.Serialization.Schema;

public partial class TextComponentSchema
{
    private static readonly ObjectSchema<BlockNbtTextComponent> NbtBlockSchema
        = new ObjectSchemaBuilder<BlockNbtTextComponent>()
            .CommonNbtTextComponentSchema(BlockNbtTextComponent.SourceName)
            .Property("block", x => x.Block,
                BlockPositionSchema.Instance)
            .CommonTextComponentSchema()
            .Build();

    private static readonly ObjectSchema<EntityNbtTextComponent> NbtEntitySchema
        = new ObjectSchemaBuilder<EntityNbtTextComponent>()
            .CommonNbtTextComponentSchema(EntityNbtTextComponent.SourceName)
            .Property("entity", x => x.Entity,
                StringValueSchema.Instance)
            .CommonTextComponentSchema()
            .Build();

    private static readonly ObjectSchema<StorageNbtTextComponent> NbtStorageSchema
        = new ObjectSchemaBuilder<StorageNbtTextComponent>()
            .CommonNbtTextComponentSchema(StorageNbtTextComponent.SourceName)
            .ResourceLocation("storage", x => x.Storage)
            .CommonTextComponentSchema()
            .Build();

    private static Result<TextComponent> DecodeNbtInternal<TElement>(TElement value,
        IReadOnlyObjectLike<TElement> o,
        IValueDecoder<TElement> decoder)
    {
        if (o.ContainsKey("source"))
        {
            return DecodeNbtBySourceInternal(value, o, decoder);
        }

        // Otherwise search for fields
        return o switch
        {
            _ when o.ContainsKey("block") => CastResult(NbtBlockSchema.Decode(value, decoder)),
            _ when o.ContainsKey("entity") => CastResult(
                    NbtEntitySchema.Decode(value, decoder)),
            _ when o.ContainsKey("storage") => CastResult(
                    NbtStorageSchema.Decode(value, decoder)),
            _ => Errors.Error("Unsupported or malformed source type")
        };
    }

    private static Result<TextComponent> DecodeNbtBySourceInternal<TElement>(TElement value,
        IReadOnlyObjectLike<TElement> o,
        IValueDecoder<TElement> decoder)
    {
        var sourceResult = decoder.GetString(o.Get("source"));
        if (!sourceResult.IsSuccess(out var sourceName))
        {
            return sourceResult.AsError();
        }

        return sourceName switch
        {
            "block" => CastResult(NbtBlockSchema.Decode(value, decoder)),
            "entity" => CastResult(NbtEntitySchema.Decode(value, decoder)),
            "storage" => CastResult(NbtStorageSchema.Decode(value, decoder)),
            _ => Errors.Error("Unsupported or malformed source type")
        };
    }
}