# Serialization module pre-consolidation changelog

## 0.1.0-alpha.7

> [!NOTE]
> This release reworked the majority of the Results and Schemas framework. It is necessary to check for all usages for errors.

### Added

- Added the following functionalities to valued results:
  - `Map` which executes map functions that takes the result value returns another result
  - `IsSuccess(out T)` which enables checking for success and value retrival at the same time
  - `Deconstruct` which breaks a valued result down to value and value-less result
- Added a Try pattern extension method for decoders.
- Added a new `ErrorRecord` structure for transmitting error state.
  - Added the implicit case from `ErrorResult` for all result types.

### Changed

- **Breaking**: Update to .NET 10.
- **Breaking**: Renamed `DataResult` to `Result<T>`.
  - This type may no longer be used to represent absence of value as a result of _successful_ (rather than _errored_) operation.
- **Breaking**: Renamed `Results` to `Errors`.
- **Breaking**: Replaced result constructors with result factory methods in respective classes.
- **Breaking**: Value schema types are no longer generic.
  - All `Encode` and `Decode` methods now instead have a type parameter `TElement` for element type.
- **Breaking**: Primitive schema classes are now sealed.
- **Breaking**: All primitive schema classes now have a private constructor, except for `StringGuidSchema` which have customisation support.

### Removed

- **Breaking**: Removed previously deprecated factory methods from `OpResult` and `DataResult`.
- **Breaking**: Removed successful result factories and instances from `Errors` class.
- **Breaking**: Removed previously deprecated functionalties from result types.
- **Breaking**: Removed absence-of-value related facilities.

### Deprecated

- Deprecated implicit cast from `OpResult` to `DataResult<T>`, in favour of the new `ErrorRecord` structure.

## 0.1.0-alpha.6 - 2025-10-28

### Changed

- Added a check in `ObjectSchemaBuilder` which will throw an exception when trying to specify a property with its type different from the one specified in the type argument.
  - This is not a breaking change because this is a guard against the misuse of the API, that can cause failure in decoding.

### Fixed

- Fixed the issue where object schema always return missing property error results with `property.Name` as property name. 

## 0.1.0-alpha.5 - 2025-10-25

### Added

- Added inverse operator for `DataResult` that determines whether the result indicates failure.
- Added implicit conversion operator for the following conversions:
  - `OpResult` to `DataResult`
  - `DataResult` with arbitrary type to `DataResult<object>`
- Added `Results` static class which contains shared instances and factory methods for common results of operation.
- Added extension methods for encoders, decoders and schemas that accepts `DataResult` as input and will return the error if there is any.

### Changed

- Changed how value presence and absence state in `DataResult` works.
  - Now whether the value of a result is present is determined by the `HasValue` property.
  - Added new constructor `DataResult(bool, TValue)` that indicates whether the value is present and specifies the value.
  - Added new constructor `DataResult(string)` that indicates an error state.
  - `DataResult(TValue, string)` is now deprecated.
  - For callers, this only affects users using manual `Result == null` check; `RequireNonEmpty` has been updated to adapt to this change.
- Deprecated factory methods in `OpResult` and `DataResult` in favour of `Results` class.
- Deprecated `CausedBy(OpResult)` in `DataResult` in favour of the implicit conversion.
- Deprecated `Success(TValue)` in `DataResult` in favour of the implicit conversion.

## 0.1.0-alpha.4 - 2025-10-17

### Added

- **Breaking:** Added `EnumerateObject` method to `IReadOnlyObjectLike`. ([#3](https://github.com/MineJason/serialization/issues/3))

### Changed

- Improved the error message returned by `OneOfValueSchema` in cases where none of the schemas were successful. ([#2](https://github.com/MineJason/serialization/issues/2))

## 0.1.0-alpha.3 - 2025-10-16

### Added

- Added a value schema for encoding and decoding `Guid` in a string format.
- Added a schema for encoding a decoding collections.

### Changed

- **Breaking:** Changed `OneOfValueSchema` to accept `IValueSchema<TValue, TElement>` instead of the class `ValueSchema<TValue, TElement>`.

## 0.1.0-alpha.2 - 2025-10-14

### Added

- Added a strongly typed schema interface.
- Added a value schema for `Uri`.
- Added support for optional value and reference type properties in `ObjectSchema`.
- Added support for constants in `ObjectSchema`.
- Added support for using a value predicate to filter properties not to encode.
- Added `RequireNonEmpty` and a few other utility methods to `DataResult`.
- Added `OpResult` that represents an operational result that does not contain a value.

### Changed

- **Breaking:** Moved most property addition helper methods for the builder to extensions.
- **Breaking:** `ObjectSchema` is now immutable.
- **Breaking:** Element types can now be named. Due to this, `elementName`
  parameter is added to the `IValueEncoder` interface.
- **Breaking:** `Encode` method in schemas now return `DataResult`.
- `ObjectSchema` will now throw an exception for attempting to decode on a read-only value type. ([#1](https://github.com/MineJason/serialization/issues/1))
    - This is supported and would cause the resulting structure to be effectively `default` anyway.

## 0.1.0-alpha.1 - 2025-10-08

_Initial release_.

<!-- Put here to not break Kate preview -->
<!--
Copyright (C) 2025-2026 WithLithum & contributors
SPDX-License-Identifier: Apache-2.0
-->