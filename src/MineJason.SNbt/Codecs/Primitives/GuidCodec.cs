// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.SNbt.Parsing;
using MineJason.SNbt.Values;
using MineJason.SNbt.Values.Guids;

namespace MineJason.SNbt.Codecs.Primitives;

/// <inheritdoc />
[Obsolete("Use Serialization instead.")]
public class GuidCodec : ISNbtCodec<Guid>
{
    /// <inheritdoc />
    public Guid Read(SNbtBasicTokenReader reader)
    {
        var parser = new SNbtArrayParser(reader.Reader, SNbtTagType.IntArray);
        var parsed = parser.ParseArray();

        if (parsed is not SNbtIntArray array)
        {
            throw new ArgumentException("Cannot parse array.",
                nameof(reader));
        }

        var uid = array.ToArray();
        return GuidHelper.FromMinecraft(uid);
    }

    /// <inheritdoc />
    public void Write(Guid value, SNbtWriter writer)
    {
        GuidHelper.WriteGuid(value, writer);
    }
}