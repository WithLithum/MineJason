# NBT module pre-consolidation changelog

## 0.5.0-alpha.1 - 2025/09/27

### Added

- Added support for parsing heterogeneous lists.
    - As with Minecraft, heterogeneous lists are saved as a list of compounds with empty key name (e.g. `[{"":1},{"":2}]`).

### Changed

- Rewritten `GuidHelper`, replacing some implementations with low-allocation `Span`-based ones.
- Deprecated `GuidHelper.FromUniversalBytes` in favour of the built-in `Guid(ReadOnlySpan<byte>, bool)` constructor.

## 0.4.0-alpha.2 - 2024/6/17

### Added

- Added a codec system that converts arbitrary types, other than `ISNbtWriter`, to and from SNBT.
  - Added `SNbtConverter` for this purpose. 
- Added `CompoundResolver` to help resolving complex compounds.
- Added JSON conversion support for certain types of primitive NBT tags.

### Changed

- `SNbtWriter` now uses a converter to write arbitrary objects.

### Removed

- Removed the deprecated `ISNbtValue` interface and relevant methods.

## 0.4.0-alpha.1 - 2024/5/31

### Added

- Added support for parsing primitive type and compounds consisted of primitive types.
- Added a system to write arbitrary value with codecs that converts it to its NBT representation.
- Added `SNbtWriter.WriteValue(object)` method.
- Added the `SNbtCodec` attribute that specifies a codec for any arbitrary type.

### Changed

- **BREAKING CHANGE**: `SNbtWriter.WriteValue(string, bool)` is now deprecated.
  - The new method for writing string is now `SNbtWriter.WriteStringValue(string, bool)`, that behaves exactly the same as the original method.
  - This was done because calling the old method without the optional single quote argument results in a call to `SNbtWriter.WriteValue(object)`, making the code no longer source compatible.

## 0.3.6-alpha - 2024/5/27

### Fixed

- Addressed an issue where suffix of entries in written `SNbtLongArray` and `SNbtByteArray` values are missing. 

## 0.3.5-alpha - 2024/5/14

- Changed the URLs of the NuGet package to its new locations.
- Updated the package README.
- Enabled XML documentation output and completed all documentation comments for symbols.

## 0.3.4-alpha - 2024/4/30

### Added

- Added support for writing quoted key names in `SNbtWriter`.
  - Also added support for writing properties with quoted key names.
- Added support for writing Boolean values and properties (represented by `TAG_Byte`) in `SNbtWriter.`

### Fixed

- Fixed the `sbyte` overload of `WriteProperty` not writing the `b` suffix required to specify `TAG_Byte`.

## 0.3.3-alpha - 2024/4/27

### Added

- Added `IList` constructor for `SNbtCollection<T>`.

## 0.3.2-alpha - 2024/4/19

### Fixes

- Fixed build failure by removing GitVersion.

## 0.3.1-alpha - 2024/4/19

### Added

- Added implementation for `Guid` values in SNBT.
- Added `sbyte` primitive support to `SNbtWriter`.

### Changed

- Renamed all overloads of `WriteValue` that deals with `IFormattable` and restricts their access to `internal`.

## 0.3.0-alpha - 2024/4/18

### Added

- Added an extension method `ToSNbtString` to `ISNbtWritable`.
- Added primitive writing methods to `SNbtWriter`.
- Added the `SNbtCollection<T>` base type.

### Changed

- `ISNbtValue` is deprecated and replaced wih `ISNbtWritable`.
- Deprecated methods relevant with `ISNbtValue`.
- Removed `ISNbtValue` and its implementation from the value representation types.

## 0.2.2-alpha - 2024/3/22

### Fixed

- Fixed the constructor of `SNbtByteValue` representing `false` as `1` and `true` as `0`.
- Fixed the `BooleanValue` method of `SNbtByteValue` not operating correctly.

## 0.2.1-alpha - 2024/3/17

### Fixed

- Fixed an issue resulted in adding a `Byte` value into `SNbtByteArray` causing stack overflow.

## 0.2.0-alpha - 2024/3/17

### Added

- Added `SNbtObjectBuilder`.
- Added `SNbtWriter`.
  - Supports easier writing string NBT components, lists and arrays to any `TextWriter`.
- Added interface `ISNbtWritable` for NBT values with native `SNbtWriter` support.
- Added support for implicit conversion between `String`, `Int`, `Byte`, `Long`, `Short`, `Double` and `Float` types of NBT value and their value representation.

### Changed

- `ToSNbtString()` method of default `ISNbtValue` implementations now internally use `SNbtWriter`.

## 0.1.0-alpha - 2024/3/15

- Initial release.

<!-- Copyright (C) WithLithum & contributors 2024-2026 -->
<!-- SPDX-License-Identifier: Apache-2.0 -->