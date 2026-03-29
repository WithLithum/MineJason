# AGENTS instructions

## Key commands

- Restore: `dotnet restore "src/MineJason.slnx"`
- Build solution: `dotnet build "src/MineJason.slnx"`
- Run tests: `dotnet test "src/MineJason.Tests/MineJason.Tests.csproj"`
- Commit with commitizen: `cz c -- -s`

## Environment setup

- Check [`global.json`](global.json) for .NET version
- For general information refer to [SETUP documentation](docs/SETUP.md)

## Repository structure

Key paths:

- `docs/` — Documentation files directory 
- `LICENSES/` — REUSE licenses directory
- `src/` — Source code directory
    - `MineJason/` — Handles client formats such as text components, dialogs, entity selectors
        - `Dialogs/` — [Dialogs](http://minecraft.wiki/w/Dialog)  support
        - `Serialization/` — Code that works with `MineJason.Serialization`
            - `Schema/` — Schemas for the Client module
        - `Text/` — Where new [text components](https://minecraft.wiki/w/Text_component_format) types and other text-related types needs to be in
    - `MineJason.Serialization/` — Handles format-agnostic DOM serialization
        - `Schema/` — Schemas which handles specific types
            - `Objects/` — Object schema
            - `Primitive/` — Schemas for BCL numeric types and primitives
    - `MineJason.Serialization.fNbt/` — fNBT support for `MineJason.Serialization`
    - `MineJason.SNbt/` — NBT format support originally written for SNBT
        - `Collections/` — Collection type implementation
        - `Values/` — NBT tag type implementations
    - `MineJason.Tests/` — Unit tests
        - `Client/` — Tests for `MineJason`
        - `NBT/` — Tests for `MineJason.SNbt`
        - `Serialization/` — Tests for `MineJason.Serialization`

## Build and test commands

From repository root:

- Build entire solution: `dotnet build "src/MineJason.slnx"`
- Run unit tests: `dotnet test --project "src/MineJason.Tests/MineJason.Tests.csproj"`

## Coding standards

- Do not suffix or name `ChatComponent` for new types, use `TextComponent`
- Add REUSE SPDX licence header on all files
- Use [Conventional Gitmoji](https://github.com/ljnsn/cz-conventional-gitmoji) for authoring commit messages
- Refer to [the Style Guide](docs/STYLEGUIDE.md) for more standards

## Important things

- Do not add `VersionPrefix` or `VersionSuffix` in `csproj` files unless they are already there
- Do not specify `VersionPrefix` or `VersionSuffix` nor use `--version-suffix` in command line

## Unit testing

- **Use Microsoft Testing Platform (MTP)**
- Tests for [`MineJason`](src/MineJason/) are in [`MineJason.Tests.Client`](src/MineJason.Tests/Client/) namespace
- Tests for [`MineJason.Serialization`](src/MineJason.Serialization/) and [`MineJason.Serialization.fNbt`](src/MineJason.Serialization.fNbt/) are in [`MineJason.Tests.Serialization`](src/MineJason.Tests/Serialization/) namespace
- Tests for [`MineJason.SNbt`](src/MineJason.SNbt/) are in [`MineJason.Tests.NBT`](src/MineJason.Tests/NBT/) namespace

<!-- SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026 -->
<!-- SPDX-License-Identifier: Apache-2.0 -->