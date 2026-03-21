// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.Schema;

using MineJason.Components;
using MineJason.Serialization.IO;
using MineJason.Serialization.Schema.Objects;
using MineJason.Serialization.Utilities.Results;

public partial class TextComponentSchema
{
    private static readonly ObjectSchema<BlockNbtChatComponent> NbtBlockSchema
        = new ObjectSchemaBuilder<BlockNbtChatComponent>()
            .CommonNbtTextComponentSchema(BlockNbtChatComponent.SourceName)
            .Property("block", x => x.Block,
                BlockPositionSchema.Instance)
            .CommonTextComponentSchema()
            .Build();

    private static readonly ObjectSchema<EntityNbtChatComponent> NbtEntitySchema
        = new ObjectSchemaBuilder<EntityNbtChatComponent>()
            .CommonNbtTextComponentSchema(EntityNbtChatComponent.SourceName)
            .Property("entity", x => x.Entity,
                EntitySelectorSchema.Instance)
            .CommonTextComponentSchema()
            .Build();

    private static readonly ObjectSchema<StorageNbtChatComponent> NbtStorageSchema
        = new ObjectSchemaBuilder<StorageNbtChatComponent>()
            .CommonNbtTextComponentSchema(StorageNbtChatComponent.SourceName)
            .ResourceLocation("storage", x => x.Storage)
            .CommonTextComponentSchema()
            .Build();

    private static Result<ChatComponent> DecodeNbtInternal<TElement>(TElement value,
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

    private static Result<ChatComponent> DecodeNbtBySourceInternal<TElement>(TElement value,
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