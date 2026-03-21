# Style Guide

> [!WARNING]
> 
> **NEVER** use Rider to edit localization files! It produces localization resource file names with all lowercase, which will break builds on non-Windows environments.

## 1. Formatting and general matters

### 1.1. Naming

1. Use *pascal case* for symbol names *except* for parameters, instance fields and local variables.
2. Do not use Hungarian notation.
3. Unless otherwise specified in this Guide do not use any special characters, suffixes nor prefixes.
4. Name all symbols in English. Anything that is not English (including romanisations that is not a proper noun or a popular loan word) is not permitted.
5. Instance fields (fields that are not static) are prefixed with an underscore (`_`).
6. Use nouns for symbol names unless otherwise specified.
7. Prefix interface names with `I`.
8. Interface names should be an adjective word, or a noun if none is appropriate/plausible.
9. Do *not* prefix enum names with `E`, nor suffix them with `Enum` or `Flags`. To indicate an enum represents options, just suffix `Options`.
10. Start all type parameter names with a letter `T`. If a generic type or method has only one type parameter, consider using just `T` for the name of that parameter.
11. Suffix `TextComponent` for text components.

The [standards on Microsoft Learn](https://learn.microsoft.com/dotnet/standard/design-guidelines/names-of-classes-structs-and-interfaces) should be used for something that is not listed above.

### 1.2. Formatting and style

1. Use Allman bracing - that means a new line for each brace, opening or closing.
2. Use four spaces instead of `Tab` when possible. In case of that `Tab` is absolutely necessary, make your editor display it as four spaces wide.
3. Maximum line length is 100 characters.
4. Separate the comment start/end marker with the comment text by a single space character _only_.
5. Use file-scoped namespaces.
6. Put all imports *before* namespace declarations. Some old code exists that puts the imports after namespace which needs to be corrected when touching them.
7. Imports should be sorted alphabetically, however imports of `System` namespaces which should be listed before everything else.
8. Avoid `this.` unless you need to differentiate current type members from other symbols.
9. Explicitly write visibility for modules even if they have implicit default visibilities.
10. Use `var` only if the type of the variable can be determined reasonably from the immediate context.
11. Put `readonly` after `static` static.

The [.NET Runtime Style Guide](https://github.com/dotnet/runtime/blob/HEAD/docs/coding-guidelines/coding-style.md) can be used for something not listed above.

### 1.3. Design

1. Use nullable reference types analysis correctly and do not suppress compiler warnings for possible `null`. The null-forgiving operator (`!`) should only be used in a situation where `null` is not or should not be possible.
2. Don't repeat yourself. Extract common methods or make utility types if necessary.
3. Don't declare set-only properties. If you need to expose something that is set-only, use methods.
4. Types not visible to outside, such as `internal` and `private`, should be sealed unless someone needs to inherit from them.
5. Only catch exceptions that indicates _an abnormal, unexpected or exceptional situation or state_. Avoid catching broad or general exception types like `Exception`.
6. Don't `try-catch` in cases where patterns like [Tester-Doer](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/exceptions-and-performance#tester-doer-pattern) or `TryGet` is sufficient.
7. Instances of types encapsulating unmanaged resources (e.g. `IDisposable`) must be disposed when appropriate (via for example the `using` blocks and statements). Any types that directly handles unmanaged resources *must* implement the `IDisposable` interface and the Dispose pattern.
8. Library code and non-UI code must await async methods via `.ConfigureAwait(false)`.
9. Avoid `goto` as best as you can.

### 1.4. Unit tests

1. Unit tests names should consist of the _subject under test_, the _state being simulated_ and the _expected result_, separated by underscores (`_`).
2. Unit tests should be written to conform to the AAA (Arrange-Act-Assert) pattern.

## 2. Documentation comments

### 2.1. In general

1. Summary should be written as a present indicative sentence.
2. Write documentation comments in English. If needed, documentation in other languages can be published elsewhere.
3. Use [Recommended XML Documentation Tags](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/recommended-tags) and [Sandcastle Help File Builder extended comment tags](https://ewsoftware.github.io/ExtendedDocCommentsProvider/html/141160ac-ba6a-4f6d-a802-1110aadea932.htm).

### 2.2. Standard formats

1. Constructor summaries should be written as:
   
   > Initializes a new instance of the [type] [class|structure|etc...]

2. Summary of a constructor _with parameters_ should be written as:
   
   > Initializes a new instance of the [type] [class|structure|etc..] with [configuration].

3. Sealed classes summary should be followed by:
   
   > This class cannot be inherited.

4. Properties that are _not_ of `bool` should be written as:
   
   > [Gets|Gets or sets] [the value].

5. Properties that _are_ `bool` should be written as:
   
   > [Gets|Gets or sets] a value indicating [state].

6. Field summaries should _not_ be written like property summaries.
7. If a type has only one feature then its summary may be written like a void-returning parameter-less method.

## See also

- [Framework Design Guidelines Digest](https://github.com/dotnet/runtime/blob/main/docs/coding-guidelines/framework-design-guidelines-digest.md)

- [.NET Runtime Coding Style](https://github.com/dotnet/runtime/blob/HEAD/docs/coding-guidelines/coding-style.md)

- [Google C# style guide](https://google.github.io/styleguide/csharp-style.html)

- Framework Design Guidelines (the book)

- [Cheat Sheet: Best practices for writing XML documentation in C#](https://blog.rsuter.com/best-practices-for-writing-xml-documentation-phrases-in-c/)

<!-- Put this here because this breaks KIO -->

<!-- SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026 -->

<!-- SPDX-License-Identifier: Apache-2.0 -->