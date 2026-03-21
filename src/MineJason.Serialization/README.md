# MineJason.Serialization

`MineJason.Serialization` is a serialization library that deals with DOM and
value types through format-agnostic schema.

It was grown out of the need of a robust, schema-based and format-agnostic
serialization and deserialization between values, and their DOM format, during
the development of `MineJason`.

The library consists of two critical functions:

- Encoder and decoders, which encodes CLR primitives to their target DOM
  equivalent;
- Schemas, which are responsible for encoding and decoding further, more
  complex types.

## Usage

Implementation of encoders, decoders and schemas, as well as direct consumption
of the types provided by this library requires a separate article, which have
yet been writing due to the library being in early development.

Format implementations should provide `Encoder` and `Decoder` implementations
and schema implementations should provide schema implementations. This library
comes with format implementation for `System.Text.Json` as well as schema
implementations for basic .NET data types.

## Contributing

To report issues or contribute code for this library, go to the
[project repository](https://codeberg.org/MineJason/serialization). See the
README file for more information.

<!-- Copyright (C) 2025-2026 WithLithum & contributors -->
<!-- SPDX-License-Identifier: Apache-2.0 -->