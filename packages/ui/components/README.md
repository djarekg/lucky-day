# @lucky-day/components

Shared React component package for reusable UI building blocks used across Lucky Day applications.

## TOC

## Overview

`@lucky-day/components` is a React component package for reusable UI blocks used across Lucky Day apps.

## Installation

From the workspace root:

```bash
vp pack
```

## Structure

- [`src`](src): Package source root.
  - [`index.ts`](src/index.ts): Main package entrypoint.
  - [`loader`](src/loader): Loader component module.
    - [`loader.tsx`](src/loader/loader.tsx): Loader component implementation.
    - [`loader.module.css`](src/loader/loader.module.css): Loader styles.

## APIs

- [`noop`](src/index.ts): Default no-op utility exported from package root.
- [`Loader`](src/loader/loader.tsx): Circular progress loader component for full-container loading states. Example:

```tsx
import Loader from '@lucky-day/components/loader';

export const PendingView = () => <Loader />;
```

## References

- [Workspace](../../../README.md)
- [@lucky-day/core](../core/README.md)
