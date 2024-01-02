# Changelog

This document notes all API visible changes of this library.

## [Unreleased]

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

[Unreleased]: https://codeberg.org/WithLithum/MineJason/compare/v0.1.0-alpha...trunk
[v0.1.0-alpha]: https://codeberg.org/WithLithum/MineJason/src/tag/v0.1.0-alpha