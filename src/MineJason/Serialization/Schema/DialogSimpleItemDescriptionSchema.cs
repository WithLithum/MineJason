// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Serialization.Schema;

using MineJason.Dialogs;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;

internal sealed class DialogSimpleItemDescriptionSchema : ValueSchema<ItemDialogMessage.DescriptionLabel>
{
    public override Result<TElement> Encode<TElement>(ItemDialogMessage.DescriptionLabel value, IValueEncoder<TElement> encoder, string? elementName = null)
    {
        return Errors.Error("Not supported");
    }

    public override Result<ItemDialogMessage.DescriptionLabel> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var compResult = TextComponentSchema.Instance.Decode(value, decoder);
        if (!compResult.IsSuccess(out var comp))
        {
            return compResult.AsError();
        }

        return new ItemDialogMessage.DescriptionLabel
        {
            Contents = comp
        };
    }
}