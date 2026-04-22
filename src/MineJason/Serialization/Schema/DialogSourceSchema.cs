// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema;

using System.Collections.Immutable;
using MineJason.Dialogs.Reference;
using MineJason.Properties;
using MineJason.Serialization.IO;
using MineJason.Serialization.Utilities.Results;
using MineJason.Utilities;

/// <summary>
/// Encodes or decodes <see cref="MultiDialogSource"/> to or from the specified element type.
/// </summary>
public sealed class DialogSourceSchema : ValueSchema<MultiDialogSource>
{
    private readonly bool _allowMulti;

    /// <summary>
    /// The shared, <see cref="OneDialogSource"/> only schema.
    /// </summary>
    public static readonly DialogSourceSchema SingleInstance = new(false);

    /// <summary>
    /// The shared schema that accepts multi dialog sources.
    /// </summary>
    public static readonly DialogSourceSchema MultiInstance = new(true);

    private static readonly CollectionSchema<MultiDialogSource> OneCollection =
        new(SingleInstance);

    private DialogSourceSchema(bool allowMulti)
    {
        _allowMulti = allowMulti;
    }

    #region Encoder

    /// <inheritdoc />
    public override Result<TElement> Encode<TElement>(MultiDialogSource value, IValueEncoder<TElement> encoder,
        string? elementName = null)
    {
        if (!_allowMulti && value is not OneDialogSource)
        {
            return MyResults.MultiReferenceDisallowed;
        }

        return value switch
        {
            InlineDialogSource inline => EncodeInline(inline, encoder, elementName),
            DialogReference resource => EncodeResource(resource, encoder, elementName),
            TagDialogSource tag => encoder.CreateString($"#{tag.Id}"),
            CollectionDialogSource col => OneCollection.Encode(col.Values, encoder, elementName),
            _ => Errors.Error("Unsupported dialog reference type")
        };
    }

    private static Result<TElement> EncodeResource<TElement>(DialogReference value,
        IValueEncoder<TElement> encoder,
        string? elementName)
    {
        return ResourceLocationSchema.Instance.Encode(value.Identifier,
            encoder, elementName);
    }

    private static Result<TElement> EncodeInline<TElement>(InlineDialogSource value,
        IValueEncoder<TElement> encoder,
        string? elementName)
    {
        return DialogSchema.Instance.Encode(value.Dialog, encoder, elementName);
    }

    #endregion

    #region Decoder

    /// <inheritdoc />
    public override Result<MultiDialogSource> Decode<TElement>(TElement value, IValueDecoder<TElement> decoder)
    {
        var strResult = decoder.GetString(value);
        if (strResult.Error == null)
        {
            return DecodeReferenceOrTag(strResult.Value!);
        }

        var colResult = decoder.GetCollection(value);
        if (colResult.Error == null)
        {
            return DecodeCollectionSource(value, decoder);
        }

        var objResult = decoder.GetObjectLike(value);
        if (objResult.Error == null)
        {
            return DecodeInlineDialog(value, decoder);
        }

        return Errors.Error(MessageResources.ResultNotDialogSource);
    }

    private static Result<MultiDialogSource> DecodeCollectionSource<TElement>(TElement value,
        IValueDecoder<TElement> decoder)
    {
        var collection = OneCollection.Decode(value, decoder);
        if (!collection)
        {
            return collection.AsError();
        }

        return new CollectionDialogSource(collection.Value!.Cast<OneDialogSource>().ToImmutableList());
    }

    private static Result<MultiDialogSource> DecodeInlineDialog<TElement>(TElement value,
        IValueDecoder<TElement> decoder)
    {
        var dialogResult = DialogSchema.Instance.Decode(value, decoder);
        if (!dialogResult) return dialogResult.AsError();

        return new InlineDialogSource(dialogResult.Value!);
    }

    private Result<MultiDialogSource> DecodeReferenceOrTag(string value)
    {
        var span = value.AsSpan();
        if (span.StartsWith('#'))
        {
            return DecodeTag(span);
        }

        if (!ResourceLocation.TryParse(span, out var refId))
        {
            return MyResults.ResourceLocationError(new string(span[1..]));
        }

        return new DialogReference(refId);
    }

    private Result<MultiDialogSource> DecodeTag(ReadOnlySpan<char> span)
    {
        if (!_allowMulti)
        {
            return MyResults.MultiReferenceDisallowed;
        }

        if (!ResourceLocation.TryParse(span[1..], out var tagId))
        {
            return MyResults.ResourceLocationError(new string(span[1..]));
        }

        return new TagDialogSource(tagId);
    }

    #endregion
}