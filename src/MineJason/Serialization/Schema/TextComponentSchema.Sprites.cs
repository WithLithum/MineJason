// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.Schema;

using MineJason.Serialization.IO;
using MineJason.Serialization.Schema.Objects;
using MineJason.Serialization.Utilities.Results;
using MineJason.Text;

public partial class TextComponentSchema
{
    private static readonly ObjectSchema<AtlasObjectTextComponent> AtlasSpriteSchema =
        new ObjectSchemaBuilder<AtlasObjectTextComponent>()
            .CommonObjectTextComponentSchema("atlas")
            .OptionalResourceLocation("atlas", x => x.Atlas)
            .ResourceLocation("sprite", x => x.Sprite)
            .Build();

    private static readonly ObjectSchema<PlayerObjectTextComponent> PlayerSpriteSchema =
        new ObjectSchemaBuilder<PlayerObjectTextComponent>()
            .CommonObjectTextComponentSchema("player")
            .Property("player", x => x.Player,
                MineJasonSchemas.PlayerProfile)
            .Build();

    private static Result<ChatComponent> DecodeSpriteInternal<TElement>(
        IReadOnlyObjectLike<TElement> obj,
        TElement elementObj,
        IValueDecoder<TElement> decoder)
    {
        string objType = "atlas";
        if (obj.ContainsKey("object"))
        {
            var objTypeResult = obj.Get("object").RequireStringValue(decoder);
            if (!objTypeResult.IsSuccess(out objType!))
            {
                return objTypeResult.AsError();
            }
        }

        return objType switch
        {
            "atlas" => CastResult(AtlasSpriteSchema.Decode(elementObj, decoder)),
            "player" => CastResult(PlayerSpriteSchema.Decode(elementObj, decoder)),
            _ => Errors.Error($"Unsupported sprite object type '{objType}'")
        };
    }
}