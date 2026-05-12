# Db

Shared Entity Framework Core data access package with SQLite, repositories, and database seeding.

## TOC

## Overview

`Db` is the shared EF Core data layer for Lucky Day. It contains the SQLite `DbContext`, entities, repository abstractions, and seed logic used by the API project.

## Structure

- [`Db`](.): Data package root.
  - [`Data`](Data): EF Core context and model configuration.
    - [`Product.cs`](Models/Product.cs): Product entity.
    - [`Customer.cs`](Models/Customer.cs): Customer entity.
  - [`Repositories`](Repositories): Repository interfaces/implementations and unit-of-work.
    - [`IRepository.cs`](Repositories/IRepository.cs): Generic repository contract.
    - [`Repository.cs`](Repositories/Repository.cs): Generic repository implementation.
    - [`UserRepository.cs`](Repositories/UserRepository.cs): User-specific repository.
    - [`ProductRepository.cs`](Repositories/ProductRepository.cs): Product-specific repository.
    - [`CustomerRepository.cs`](Repositories/CustomerRepository.cs): Customer-specific repository.
    - [`UnitOfWork.cs`](Repositories/UnitOfWork.cs): Transaction and repository coordination.
  - [`Seeding`](Seeding): Seed data orchestration.
    - [`DatabaseSeeder.cs`](Seeding/DatabaseSeeder.cs): Seed entrypoint.
    - [`FakerConfiguration.cs`](Seeding/FakerConfiguration.cs): Bogus faker setup.
  - [`ServiceCollectionExtensions.cs`](ServiceCollectionExtensions.cs): DI registration extension.
  - [`DbInitializer.cs`](DbInitializer.cs): Database initialization and seeding entrypoints.

## APIs

- [`AddDbServices(IServiceCollection, string)`](ServiceCollectionExtensions.cs): Registers `LuckyDayDbContext`, repositories, and `IUnitOfWork`.
- [`DbInitializer.InitializeDatabaseAsync(string)`](DbInitializer.cs): Creates and seeds the database from a connection string.
- [`DbInitializer.InitializeDatabaseAsync(LuckyDayDbContext)`](DbInitializer.cs): Seeds using an existing `DbContext`.
- [`IUserRepository`](Repositories/UserRepository.cs): User-specific query operations (email and credential-related lookups).
- [`IProductRepository`](Repositories/ProductRepository.cs): Product-specific queries (active/products by type/details).
- [`ICustomerRepository`](Repositories/CustomerRepository.cs): Customer queries with contact loading.
- [`IUnitOfWork`](Repositories/UnitOfWork.cs): Transaction boundary and multi-repository coordination.

Example:

```csharp
builder.Services.AddDbServices("Data Source=./data/lucky-day.db");
```

## References

- [Workspace](../../../README.md)
