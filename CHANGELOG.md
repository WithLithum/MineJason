# Changelog

This document notes most user-visible changes of this library. The format is based on the [Keep a Changelog](https://keep-a-changelog.com) format and this project adheres to [Semantic Versioning](https://semver.org).

## [Unreleased]

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