# lucky-day

A full-stack workspace monorepo for the Lucky Day platform, combining API, web, and shared package projects.

## TOC

## Overview

`lucky-day` is a monorepo containing:

## Prerequisites

- Bun `>=1.3.13`
- .NET SDK `10.0` (preview-compatible, used by `net10.0` projects)

```bash
vp install
dotnet restore .vscode/Api.slnx
```

- [web](apps/web/README.md): Next.js app that consumes the API and manages authenticated sessions.
- [packages](packages): Shared package projects.
  - [dotnet](packages/dotnet): .NET shared libraries and tests.
    - [Db](packages/dotnet/Db/README.md): EF Core SQLite data access layer, repositories, and seeding.
    - [Db.Tests](packages/dotnet/Db.Tests/README.md): Integration-style tests for database initialization and repository behavior.
  - [ui](packages/ui): Shared frontend libraries.
    - [core](packages/ui/core/README.md): Shared TypeScript utility package.
    - [components](packages/ui/components/README.md): Reusable React UI components package.
