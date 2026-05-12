# Db.Tests

Integration test project for validating database initialization, seeding, and repository behavior.

## TOC

## Overview

`Db.Tests` validates the database package behavior using xUnit and SQLite-backed integration-style tests. It verifies database initialization, idempotent seeding, CRUD operations, repository query behavior, and transaction rollback semantics.

## Structure

- [`Db.Tests`](.): Test project root.
  - [`DbInitializerTests.cs`](DbInitializerTests.cs): Tests database creation and seeding behavior.

## APIs

- [`DbInitializerTests`](DbInitializerTests.cs): Coverage for initialization validation and seed idempotency.
- [`RepositoryCrudTests`](RepositoryCrudTests.cs): Coverage for repository methods and transaction rollback behavior.
- [`SqliteTestDatabase.CreateEmptyAsync()`](TestInfrastructure/SqliteTestDatabase.cs): Creates an isolated empty SQLite database for tests.
- [`SqliteTestDatabase.CreateSeededAsync()`](TestInfrastructure/SqliteTestDatabase.cs): Creates an isolated pre-seeded SQLite database for tests.

Example:

```bash
dotnet test packages/dotnet/Db.Tests/Db.Tests.csproj
```

## References

- [Workspace](../../../README.md)
- [Db](../Db/README.md)
