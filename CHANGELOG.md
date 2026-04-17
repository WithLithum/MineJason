# Changelog

This document notes most user-visible changes of this library. The format is based on the [Keep a Changelog](https://keepachangelog.com) format and this project adheres to [Semantic Versioning](https://semver.org).

## [0.7.0-alpha.5] - 2026-04-17

### Added

- Added `JsonSchemaBasedConverter` which contains common logic for `JsonConverter` implementations that simply wraps around a schema.
- Added support for `UriKind` configuration in `UriSchema`. 

### Changed

- **BREAKING CHANGE**: Moved click and hover events to `MineJason.Text.Behaviours`.
- **BREAKING CHANGE**: Renamed `ChatComponent` classes to `TextComponent`.
- **BREAKING CHANGE**: Renamed `IChatColor` to `ITextColor`.
  - Renamed `TextColor` to `RgbTextColor`.
  - The argument of `TextColor.FromRgb(int)` is now validated.
- **BREAKING CHANGE**: Made the following converters inherit `JsonSchemaBasedConverter` instead:
  - `ClickEventConverter`
  - `HoverEventConverter`
  - `TextComponentConverter`
- Improved entity selector parsing for origin and diagonal coordinate conditions.

### Removed

- **BREAKING CHANGE**: Removed previously deprecated legacy `ShowItemHoverEvent`.

### Fixed

- Fixed `NbtListAdapter` not validating if the tag is named, which would result in `ArgumentException`.

### Deprecated

- Deprecated built-in JSON contexts.
- Deprecated loot number providers.
- Deprecated `UriSchema.Instance` in favour of constructing own instances.

## [0.7.0-alpha.4] - 2026-03-22

### Fixed

- Corrected misconfiguration that prevents package publishing.

## [0.7.0-alpha.3] - 2026-03-22

This is the first consolidated version of MineJason, bringing all modules under the same repository and the same version number.

### Added

- Added support for the following new text component fields introduced in 26.1: 
    - `fallback` field for object components
    - `plain` field for NBT components
- Added `TryParse` support for `IntegralRange`.

### Changed

- **Breaking**: Updated NBT module to .NET 10.
- Refactored entity selector parsers to use `Span<char>` where appropriate.
  - This should, in theory, reduce allocation and improve performance of entity selector parsing. 
- Changed the implementation of several `GuidExtensions` methods to use `Span`-based implementations.
- Added checks for certain `null` values in `ObjectSchema` and `ObjectSchemaBuilder`.

### Removed

- SharpNBT extension library has been removed from codebase.

### Fixed

- Added missing documentation comments for some symbols in Client module.

### Deprecated

- Deprecated `EntitySelectorParser.ParseIntegralRange`. Calls to that method should be replaced
  with call to `IntegralRange.Parse()` or `IntegralRange.TryParse()`.
- Deprecated the `StringGuidSchema` in the client library.
- Deprecated the NBT codec system, which is obsolete since the introduction of Serialization schemas.

### Other

- Consolidated repositories.

## Older versions

Please see the [old changelogs directory](docs/changelogs).

[0.7.0-alpha.5]: https://github.com/WithLithum/MineJason/compare/v0.7.0-alpha.4...v0.7.0-alpha.5
[0.7.0-alpha.4]: https://github.com/WithLithum/MineJason/compare/v0.7.0-alpha.3...v0.7.0-alpha.4
[0.7.0-alpha.3]: https://github.com/WithLithum/MineJason/releases/tag/v0.7.0-alpha.3

<!-- Put this here because this breaks KIO -->
<!-- SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026 -->
<!-- SPDX-License-Identifier: Apache-2.0 -->