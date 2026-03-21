// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: LGPL-3.0-or-later

namespace MineJason.Utilities;

using System.Globalization;
using System.Text;
using MineJason.Serialization.Utilities.Results;
using MR = MineJason.Properties.MessageResources;

internal static class MyResults
{
    private static readonly CompositeFormat MalformedResourceLocation =
        CompositeFormat.Parse(MR.ResultMalformedResourceLocation);
    private static readonly CompositeFormat DoesNotKnowTypeDecodeFormat =
        CompositeFormat.Parse(MR.ResultDoesNotKnowTypeDecode);
    private static readonly CompositeFormat DoesNotKnowTypeEncodeFormat =
        CompositeFormat.Parse(MR.ResultDoesNotKnowTypeEncode);

    internal static readonly ErrorRecord MultiReferenceDisallowed = new(MR
        .ResultMultiDialogSourceDisallowed);

    internal static readonly ErrorRecord UnsupportedValue = new(MR.ResultUnsupportedValue);

    internal static readonly ErrorRecord NotMinecraftNamespace = new(MR.ResultNotMinecraftSpace);

    internal static ErrorRecord ResourceLocationError(string str)
    {
        return new ErrorRecord(string.Format(CultureInfo.InvariantCulture,
            MalformedResourceLocation,
            str));
    }

    internal static ErrorRecord DoesNotKnowTypeEncode(string type)
    {
        return new ErrorRecord(string.Format(CultureInfo.InvariantCulture,
            DoesNotKnowTypeEncodeFormat,
            type));
    }

    internal static ErrorRecord DoesNotKnowTypeDecode(string type)
    {
        return new ErrorRecord(string.Format(CultureInfo.InvariantCulture,
            DoesNotKnowTypeDecodeFormat,
            type));
    }
}