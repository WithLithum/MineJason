// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Serialization.Schema.Objects;

public record ValueWithSchema
{
    public required object Value { get; init; }
    public required IValueSchema Schema { get; init; }
}