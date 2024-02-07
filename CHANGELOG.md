# Changelog

This document notes most user-visible changes of this library. This format is loosely based on the [Keep a Changelog](https://keep-a-changelog.com) format and this project adheres to [Semantic Versioning](https://semver.org).

## [Unreleased]

This release contains breaking changes.

### Added

#### Target selectors

- Added support for additional target selector argument:
  - `advancements` argument for filtering players with certain advancements. (#7)
  - `predicate` arguments for filtering entities with predicates. (#8)
- Added split up NBT chat component types in `MineJason.Components` namespace.
- Added support for NBT chat component building.
- Added support for block positions.
- Added `NbtProvider.Empty` readonly value.
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

## [v0.3.0-alpha]

### Added

- Added support for target selector models and building.
  - Supports converting from and to strings.
- Added support for equality comparisons with scoreboard chat components.
- Added basic builder support for chat components.
- Added new builder `Create` methods for `keybind` and `selector` components.

### Changed

- Entity chat components now uses `EntitySelector` instead of a string.

## [v0.2.1-alpha]

### Added

- Added support for RGB chat colors.

## [v0.2.0-alpha]

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

## [v0.1.0-alpha]

- First version release.

[Unreleased]: https://gitlab.com/WithLithum/MineJason/compare/v0.3.0-alpha...trunk
[v0.3.0-alpha]: https://gitlab.com/WithLithum/MineJason/compare/v0.2.1-alpha...v0.3.0-alpha
[v0.2.1-alpha]: https://gitlab.com/WithLithum/MineJason/compare/v0.2.0-alpha...v0.2.1-alpha
[v0.2.0-alpha]: https://gitlab.com/WithLithum/MineJason/compare/v0.1.0-alpha...v0.2.0-alpha
[v0.1.0-alpha]: https://gitlab.com/WithLithum/MineJason/src/tag/v0.1.0-alpha
