# @lucky-day/core

Shared TypeScript utility package with reusable helpers for formatting, validation, data handling, and runtime checks.

## TOC

## Overview

`@lucky-day/core` is a shared TypeScript utility package used by frontend projects in this workspace.

## Installation

From the workspace root:

```bash
vp pack
```

## Structure

- [`src`](src): Package source root.
  - [`index.ts`](src/index.ts): Package root exports.
  - [`utils`](src/utils): Shared utility modules.
    - [`index.ts`](src/utils/index.ts): Aggregated utility exports.
    - [`cookie.ts`](src/utils/cookie.ts): Cookie helpers.
    - [`date.ts`](src/utils/date.ts): Date helpers.
    - [`debounce.ts`](src/utils/debounce.ts): Debounce helpers.
    - [`environment.ts`](src/utils/environment.ts): Environment helpers.
    - [`format.ts`](src/utils/format.ts): Formatting helpers.
    - [`number.ts`](src/utils/number.ts): Number helpers.
    - [`object.ts`](src/utils/object.ts): Object helpers.
    - [`string.ts`](src/utils/string.ts): String helpers.
    - [`try-catch.ts`](src/utils/try-catch.ts): Safe execution wrappers.
    - [`types`](src/utils/types): Shared utility type definitions.
    - [`validation`](src/utils/validation): Validation helpers.

## APIs

- [`@lucky-day/core` root export](src/index.ts): Exposes the complete utility surface.
- [`cookie utilities`](src/utils/cookie.ts): Cookie helpers for browser/session values.
- [`date utilities`](src/utils/date.ts): Date formatting/parsing helpers.
- [`debounce utilities`](src/utils/debounce.ts): Debounce helpers for event-driven UIs.
- [`environment utilities`](src/utils/environment.ts): Runtime environment checks.
- [`format utilities`](src/utils/format.ts): Shared formatting helpers.
- [`number utilities`](src/utils/number.ts): Number conversion and formatting helpers.
- [`object utilities`](src/utils/object.ts): Object manipulation helpers.
- [`string utilities`](src/utils/string.ts): String transformation helpers.
- [`validation utilities`](src/utils/validation/index.ts): Shared validators and validation helpers.
- [`try-catch utilities`](src/utils/try-catch.ts): Safe execution wrappers.

## References

- [Workspace](../../../README.md)
