# MineJason SNBT

This library provides support for building SNBT formatted NBT components without depending on any additional NBT library.


## Usage

This library is designed to be used straightforwardly.

- `SNbtCompound` is a dictionary of any SNBT value, and compound itself is a SNBT value.
- There are different kinds of supported SNBT values:
  -	All SNBT values other than Compound, List and Arrays are record structures.
  - SNBT arrays and list are different kinds of collections of corresponding values.
  - They all implement `ISNbtWritable` interface.

To convert SNBT value instances (including SNBT compounds) to string representation, call `ToSNbtString()` on any value.

## Issues

Please report issues and feature requests to the [issues section](https://codeberg.org/MineJason/nbt/issues).

<!-- Put this here because this breaks KIO -->
<!-- SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026 -->
<!-- SPDX-License-Identifier: Apache-2.0 -->