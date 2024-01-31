# Changelog

This document notes all API visible changes of this library.

## [v0.3.0-alpha]

### Added

- Added support for target selector models and building.
  - Supports converting from and to strings.
- Added support for equality comparisons with scoreboard chat components.
- Added basic builder support for chat components.
- Added new builder `Create` methods for `keybind` and `selector` components.

### Changed

- Entity chat components now uses `EntitySelector` instead of a string.

### Fixed

- Fixed definition error in `show_item` hover event that renders `count` and `tag` required, and incorrectly serializes `tag` as `nbt`.

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

[Unreleased]: https://codeberg.org/WithLithum/MineJason/compare/v0.3.0-alpha...trunk
[v0.3.0-alpha]: https://codeberg.org/WithLithum/MineJason/compare/v0.2.1-alpha...v0.3.0-alpha
[v0.2.1-alpha]: https://codeberg.org/WithLithum/MineJason/compare/v0.2.0-alpha...v0.2.1-alpha
[v0.2.0-alpha]: https://codeberg.org/WithLithum/MineJason/compare/v0.1.0-alpha...v0.2.0-alpha
[v0.1.0-alpha]: https://codeberg.org/WithLithum/MineJason/src/tag/v0.1.0-alpha
