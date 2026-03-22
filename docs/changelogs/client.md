# Changelog

This document notes most user-visible changes of this library. This format is loosely based on the [Keep a Changelog](https://keep-a-changelog.com) format and this project adheres to [Semantic Versioning](https://semver.org).

## [Unreleased]

### Added

- Added support for the following new text component fields introduced in 26.1: 
	- `fallback` field for object components
	- `plain` field for NBT components
- Added `TryParse` support for `IntegralRange`.

### Changed

- Refactored entity selector parsers to use `Span<char>` where appropriate.
  - This should, in theory, reduce allocation and improve performance of entity selector parsing. 
- Changed the implementation of several `GuidExtensions` methods to use `Span`-based implementations.

### Removed

- SharpNBT extension library has been removed from codebase.

### Fixed

- Added missing documentation comments for some symbols.

### Deprecated

- Deprecated `EntitySelectorParser.ParseIntegralRange`. Calls to that method should be replaced
  with call to `IntegralRange.Parse()` or `IntegralRange.TryParse()`.
- Deprecated the `StringGuidSchema` in the client library.

### Other

- Consolidated repositories.

## [0.7.0-alpha.2] - 2026/03/01

### Changed

- **BREAKING**: Updated to .NET 10.
- **BREAKING**: Updated serialization library.
  - Changed serialization API surface to adapt to serialization library changes.
- **BREAKING**: Constructors of inheritable types that were previously publicly inheritable are now [private-protected](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/private-protected).
- **BREAKING**: Constructors from object text component type that accepts `TextComponentCreationInfo` have been made internal.
- **BREAKING**: Sealed the previous unsealed `ClickEvent` descendants.
- Refactored internal APIs used by component builders to use `in` parameters for construction information.

### Removed

- **BREAKING**: Removed the following previously deprecated types:
  - `NameMatch`
  - `ScoreboardSearcher` 

## [0.7.0-alpha.1] - 2025/10/28

Note that this release is extensively breaking in many, many ways. Check every usage of the library
after you have upgraded the package. Old code WILL NOT WORK.

Some breaking changes may be missed, so again, check your entire codebase.

### Added

- Added a rewritten `System.Drawing.Color` support into the main assembly, named `TextColor`.
- Added support for `shadow_color`.
- Added support for serialization of text components, powered by the new [serialization library](https://codeberg.org/MineJason/serialization).
- Added a fluent-syntax builder for scoreboard text component.
- Added support for [dialogs](https://minecraft.wiki/w/Dialog).
- Added support for [object components](https://minecraft.wiki/w/Text_component_format#Object) introduced in 1.21.9.
- Added platform support service abstraction.
  - Does not support having more than one platform in a single application domain. With that being said, platform support implementations should be stateless. 
  - Added platform-specific support for data components.
- Added a new `ShowItemHoverEvent`.

### Changed

- **BREAKING**: Text components are now records, and their properties are now immutable.
  - All `IList` references in text components has been changed to `IReadOnlyList`.
  - Changed how builders create components and apply properties.
- Deprecated `RgbChatColor` in favour of `TextColor`.
- **BREAKING**: Renamed `KnownColor` to `NamedTextColor` and moved it to
  `MineJason.Text.Colors` namespace.
- The JSON conversion now uses the new serialization logic with adapter logic that writes to (and reads from) JSON.
  - Deprecated the old JSON resolver `ChatComponentConverter` in favour of the new resolver `TextComponentConverter`.
- **BREAKING**: The serialization context no longer includes `RgbChatColor`. Instead, it now includes the new `TextColor` type.

### Removed

- **BREAKING**: Removed `MineJason.Extensions.Drawing` from the codebase. It is no longer supported.

### Fixed

- Fixed behavioural differences between the game and the library when parsing components with `type` property.

## [0.6.0-alpha.2] - 2024/6/20

### Added

- Added `INbtDataProvider` feature, an interface to support processing NBT data without hard dependency on third-party libraries.

### Changed

- Updated SharpNBT extension for use with the above change.

### Fixed

- Attempts to fix an issue where `@n` is not exposed in NuGet packages.

### Deprecations

- Deprecated `NbtProvider` and relevant functions, including in SharpNBT extension.

## [0.6.0-alpha.1] - 2024/6/14

### Added

- Added constructors for `DistanceRange`.
- Added support for number providers in loot tables.

#### Entity Selectors

- Added interface `IEntitySelector`.
- Added `EntityGuidSelector` for selecting a single entity via GUID.
- Added support for `@n` - the nearest entity selector kind.

### Changed

- Updated for 1.21 use.
- **BINARY-BREAKING**: Changed all APIs to use `IEntitySelector`.
- **BREAKING**: Changed all APIs to use `BlockPosition` rather than `AnyBlockPosition`.
- **BREAKING**: Most structures that were not previous read-only are made read-only. These includes:
  - `BooleanAdvancementCondition`
  - `DistanceRange`
  - `IntegralRange`
  - `ScoreboardExactMatch`
  - `ScoreboardRangeMatch`
  - `ScoreboardSearcher`
  - `TagSelector`
  - `Vector3D`

## [0.5.1-alpha] - 2024/05/25

### Fixes

- Fixed raw string not supported for deserializing `ChatComponent`.
- Fixed `ChatComponentConverter` not supporting context-based serialization.

## [0.5.0-alpha] - 2024/05/10

### Added

- Added building methods for `EntitySelector`.
- Added a serialization context `MineJasonTextJsonContext` for serialization in cases that reflection is not available.
- Added negation for `CriterionRule`, `BooleanAdvancementCondition`, `PredicateCondition` and `TagSelector`.
- Added support for Minecraft-style UUID formatting.
- Added support for reading and writing `ResourceLocation` as property name (thus supporting using it in dictionary names).
- Added support for local block positions in `BlockPosition` and `BlockPositionComponent`.

### Changed

- Refactored `name` argument support for entity selectors.
  - As a result, `NameMatch` is deprecated and is replaced with `EntityNameMatch` and `EntityNameMatchCollection`.
- `MineJasonTextJsonContext` no longer supports `AnyBlockPosition`, and supports `BlockPosition` instead.

### Deprecated

- `IsRelative` properties and constructor reloads are made deprecated in `BlockPosition` and `BlockPositionComponent`.
- `AnyBlockPosition` and associated serialization types have been deprecated.

## [0.4.0-alpha]

This release contains breaking changes.

### Added

#### Target selectors

- Added support for additional target selector argument:
  - `advancements` argument for filtering players with certain advancements. (#7)
  - `predicate` arguments for filtering entities with predicates. (#8)
- Added split up NBT chat component types in `MineJason.Components` namespace.
- Added support for NBT chat component building.
- Added support for block positions.
- Added `NbtProvider.Empty` read-only value.
- Added `ChatColor` utility class and `ChatColor.FromColorCode` utility method.

### Changed

- Rewritten the pair parser for target selectors to support nested braces. (!3)
- Added `JsonIgnore` and `JsonPropertyName` attributes properly to all JSON serializable types. (#1, #2)
- Added a default hash function to `ChatComponent`. (!2)
- `JsonComponentSerializer` is now deprecated. (!2)

### Removed

- **BREAKING CHANGE**:
  - `ChatComponent.SerializerOptions` is removed.

### Fixed

- Fixed definition error in `show_item` hover event that renders `count` and `tag` required, and incorrectly serializes `tag` as `nbt`.
- Fixed an issue resulted in `NotSupportedException` being thrown when deserializing NBT components.

## [0.3.0-alpha]

### Added

- Added support for target selector models and building.
  - Supports converting from and to strings.
- Added support for equality comparisons with scoreboard chat components.
- Added basic builder support for chat components.
- Added new builder `Create` methods for `keybind` and `selector` components.

### Changed

- Entity chat components now uses `EntitySelector` instead of a string.

## [0.2.1-alpha]

### Added

- Added support for RGB chat colors.

## [0.2.0-alpha]

### Added

- Added `ResourceLocation.TryParse`.
- Added JSON conversion support for `ResourceLocation`.
- Added JSON conversion support for NBT and Keybind chat component.
- Added XML documentation.
- Added `JsonComponentSerializer` utility class that helps converting a chat component.
- Added support utilities for extra components inside components, as well as supports for array form of components.

### Changed

- `JetBrains.Annotations` package is no longer a dependency.
- Changed the type of the following properties from `string` to `ResourceLocation`:
  - `Type` property in `ShowEntityHoverEvent`.
  - `Font` property in `ChatComponent`.

## [0.1.0-alpha]

- First version release.

[Unreleased]: https://codeberg.org/MineJason/text/compare/v0.7.0-alpha.2..HEAD
[0.7.0-alpha.2]: https://codeberg.org/MineJason/text/compare/v0.7.0-alpha.1..v0.7.0-alpha.2
[0.7.0-alpha.1]: https://codeberg.org/MineJason/text/compare/v0.6.0-alpha.2..v0.7.0-alpha.1
[0.6.0-alpha.2]: https://codeberg.org/MineJason/text/compare/v0.6.0-alpha.1..v0.6.0-alpha.2
[0.6.0-alpha.1]: https://codeberg.org/MineJason/text/compare/v0.5.1-alpha..v0.6.0-alpha.1
[0.5.1-alpha]: https://codeberg.org/MineJason/text/compare/v0.4.0-alpha..v0.5.1-alpha
[0.5.0-alpha]: https://codeberg.org/MineJason/text/compare/v0.4.1-alpha..v0.5.0-alpha
[0.4.0-alpha]: https://codeberg.org/MineJason/text/compare/v0.3.0-alpha..v0.4.0-alpha
[0.3.0-alpha]: https://codeberg.org/MineJason/text/compare/v0.2.1-alpha..v0.3.0-alpha
[0.2.1-alpha]: https://codeberg.org/MineJason/text/compare/v0.2.0-alpha..v0.2.1-alpha
[0.2.0-alpha]: https://codeberg.org/MineJason/text/compare/v0.1.0-alpha..v0.2.0-alpha
[0.1.0-alpha]: https://codeberg.org/MineJason/text/tree/v0.1.0-alpha

<!-- Put this here because this breaks KIO -->
<!-- SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026 -->
<!-- SPDX-License-Identifier: Apache-2.0 -->