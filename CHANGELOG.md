# Change Log

All notable changes to the "rascript-language-server" extension will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [[0.4.0](https://github.com/joshraphael/rascript-language-server/releases/tag/v0.4.0)] - 2025-09-18

[diff](https://github.com/joshraphael/rascript-language-server/compare/v0.3.0...v0.4.0)

### Added

- Changelog
- New built-in function definition `achievement_set()`

### Changed

- `achievement()` built-in function definition to support new `set` parameter
- `leaderboard()` built-in function definition to support new `set` parameter

### Removed

### Fixed

- Lint errors on main and parser functions

## [[0.3.0](https://github.com/joshraphael/rascript-language-server/releases/tag/v0.3.0)] - 2025-09-15

[diff](https://github.com/joshraphael/rascript-language-server/compare/v0.2.1...v0.3.0)

### Added

- Support for structured data

### Changed

### Removed

### Fixed

## [[0.2.1](https://github.com/joshraphael/rascript-language-server/releases/tag/v0.2.1)] - 2025-07-06

[diff](https://github.com/joshraphael/rascript-language-server/compare/v0.2.0...v0.2.1)

### Added

### Changed

### Removed

### Fixed

- Pipeline build to support new file structure to output binary

## [[0.2.0](https://github.com/joshraphael/rascript-language-server/releases/tag/v0.2.0)] - 2025-07-06

[diff](https://github.com/joshraphael/rascript-language-server/compare/v0.1.0...v0.2.0)

### Added

- Code note hover text

### Changed

- Refactor project file structure

### Removed

### Fixed

## [[0.1.0](https://github.com/joshraphael/rascript-language-server/releases/tag/v0.1.0)] - 2025-06-17

[diff](https://github.com/joshraphael/rascript-language-server/compare/v0.0.5...v0.1.0)

### Added

- MIT License
- User defined variable names to auto completion
- User defined function names to auto completion

### Changed

### Removed

### Fixed

## [[0.0.5](https://github.com/joshraphael/rascript-language-server/releases/tag/v0.0.5)] - 2025-06-13

[diff](https://github.com/joshraphael/rascript-language-server/compare/v0.0.4...v0.0.5)

### Added

### Changed

### Removed

### Fixed

- Output binary release file in zip file directory bugfix

## [[0.0.4](https://github.com/joshraphael/rascript-language-server/releases/tag/v0.0.4)] - 2025-06-13

[diff](https://github.com/joshraphael/rascript-language-server/compare/v0.0.3...v0.0.4)

### Added

### Changed

- Output binary release file in zip file to preserve permissions

### Removed

### Fixed

## [[0.0.3](https://github.com/joshraphael/rascript-language-server/releases/tag/v0.0.3)] - 2025-06-13

[diff](https://github.com/joshraphael/rascript-language-server/compare/v0.0.2...v0.0.3)

### Added

- Version number to output binary release file

### Changed

### Removed

### Fixed

## [[0.0.2](https://github.com/joshraphael/rascript-language-server/releases/tag/v0.0.2)] - 2025-06-13

[diff](https://github.com/joshraphael/rascript-language-server/compare/v0.0.1...v0.0.2)

### Added

### Changed

### Removed

### Fixed

- Binary release building in pipeline bugfix

## [[0.0.1](https://github.com/joshraphael/rascript-language-server/releases/tag/v0.0.1)] - 2025-06-13

[diff](https://github.com/joshraphael/rascript-language-server/compare/7bcafc9797100428154812387a302316c00f1d3a...v0.0.1)

### Added

- Repository skeleton code setup
- Function definition links
- Hover box for built-in functions: byte, word, tbyte, dword, bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit, low4, high4, bitcount, word_be, tbyte_be, dword_be, float, float_be, mbf32, mbf32_le, double32, double32_be, prev, prior, bcd, identity_transform, ascii_string_equals, unicode_string_equals, repeated, once, tally, deduct, never, unless, measured, trigger_when, disable_when, always_true, always_false, format, substring, length, range, array_push, array_pop, array_map, array_contains, array_reduce, array_filter, dictionary_contains_key, any_of, all_of, none_of, sum_of, tally_of, max_of, assert, achievement, rich_presence_display, rich_presence_value, rich_presence_lookup, rich_presence_ascii_string_lookup, rich_presence_macro, rich_presence_conditional_display, leaderboard, __ornext
- Auto completion for built-in functions: byte, word, tbyte, dword, bit0, bit1, bit2, bit3, bit4, bit5, bit6, bit7, bit, low4, high4, bitcount, word_be, tbyte_be, dword_be, float, float_be, mbf32, mbf32_le, double32, double32_be, prev, prior, bcd, identity_transform, ascii_string_equals, unicode_string_equals, repeated, once, tally, deduct, never, unless, measured, trigger_when, disable_when, always_true, always_false, format, substring, length, range, array_push, array_pop, array_map, array_contains, array_reduce, array_filter, dictionary_contains_key, any_of, all_of, none_of, sum_of, tally_of, max_of, assert, achievement, rich_presence_display, rich_presence_value, rich_presence_lookup, rich_presence_ascii_string_lookup, rich_presence_macro, rich_presence_conditional_display, leaderboard, __ornext
- Binary release building in pipeline

### Changed

### Removed

### Fixed