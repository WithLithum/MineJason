# MineJason

MineJason is a .NET library that provides serialization compatible models for commonly used client data types in Minecraft: Java Edition.

## Usage

### Serialize and deserialize

You serialize and deserialize the `ChatComponent` types normally like how you would serialize other types.

Please do not use `Newtonsoft.Json`; it is not supported. Instead, use `System.Text.Json`.

### Creating components

`Create` methods in `ChatComponent` class allows you to create various types of components.

## Issues

Please report issues in the [issue tracker](https://codeberg.org/MineJason/text/issues).

<!-- Put this here because this breaks KIO -->
<!-- SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026 -->
<!-- SPDX-License-Identifier: LGPL-3.0-or-later -->