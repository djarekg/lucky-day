# Lucky Day Database Project (Db)

This project contains the Entity Framework Core database layer for the Lucky Day application using SQLite.

## Overview

- **Technology**: Entity Framework Core 8.0
- **Database**: SQLite
- **Data Generation**: Bogus (v35.5.1)
- **Architecture Pattern**: Repository Pattern with Unit of Work

## Project Structure

```
Db/
├── Models/
│   ├── Enums.cs              - Database enums (Gender, Role, Size, Color, ProductType)
│   ├── State.cs              - State model
│   ├── User.cs               - User model
│   ├── UserCredential.cs     - User credentials and roles
│   ├── Customer.cs           - Customer model
│   ├── CustomerContact.cs    - Customer contact model
│   ├── Product.cs            - Product model
│   ├── ProductColor.cs       - Product color variations
│   ├── ProductInventory.cs   - Product inventory/stock
│   └── ProductSale.cs        - Product sales transactions
├── Data/
│   └── LuckyDayDbContext.cs  - EF Core DbContext with model configurations
├── Seeding/
│   ├── DatabaseSeeder.cs     - Main seeding orchestrator
│   ├── FakerConfiguration.cs - Bogus faker configuration
│   ├── StateSeed.cs          - States seeding (all US states)
│   ├── UserSeed.cs           - Users seeding
│   ├── UserCredentialSeed.cs - User credentials seeding
│   ├── CustomerSeed.cs       - Customers seeding
│   ├── CustomerContactSeed.cs- Customer contacts seeding
│   ├── ProductSeed.cs        - Products seeding
│   ├── ProductColorSeed.cs   - Product colors seeding
│   ├── ProductInventorySeed.cs- Product inventories seeding
│   └── ProductSaleSeed.cs    - Product sales seeding
├── Repositories/
│   ├── IRepository.cs        - Generic repository interface
│   ├── Repository.cs         - Generic repository implementation
│   ├── UserRepository.cs     - User-specific repository with queries
│   ├── ProductRepository.cs  - Product-specific repository with queries
│   ├── CustomerRepository.cs - Customer-specific repository with queries
│   └── UnitOfWork.cs         - Unit of Work pattern implementation
├── DbInitializer.cs          - Database initialization helper
├── ServiceCollectionExtensions.cs - DI service registration
└── .gitignore                - Excludes SQLite database files
```

## Database Schema

The database includes the following tables:

- **States**: US states reference data
- **Users**: Application users
- **UserCredentials**: User authentication and roles (ADMIN, USER, SALES, ACCOUNTING)
- **Customers**: Business customers
- **CustomerContacts**: Contacts for each customer
- **Products**: Product catalog with gender and type
- **ProductColors**: Color variations for products
- **ProductInventories**: Stock levels by size
- **ProductSales**: Sales transactions linking products, customers, and sales users

## Getting Started

### 1. Setup the Database

The database is automatically initialized when the Api application starts in Development mode. The database file (`lucky-day.db`) will be created in the specified directory.

### 2. Using Repositories in the Api

In your Api `Program.cs`:

```csharp
using Db;

var builder = WebApplication.CreateBuilder(args);

// Register Db services
builder.Services.AddDbServices();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Initialize database
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<Db.Data.LuckyDayDbContext>();
        await DbInitializer.InitializeDatabaseAsync(dbContext);
    }
}

// ... rest of configuration
```

### 3. Injecting Repositories

In your Api controllers:

```csharp
using Db.Repositories;
using Db.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var products = await _productRepository.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(string id)
    {
        var product = await _productRepository.GetProductWithDetailsAsync(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<Product>>> GetActive()
    {
        var products = await _productRepository.GetActiveProductsAsync();
        return Ok(products);
    }
}
```

### 4. Using Unit of Work Pattern

For complex operations involving multiple entities:

```csharp
using Db.Repositories;
using Db.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public SalesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<ActionResult> CreateSale(ProductSale sale)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // Perform multiple operations
            var product = await _unitOfWork.Products.GetByIdAsync(sale.ProductId);
            if (product == null)
                return NotFound("Product not found");

            await _unitOfWork.ProductSales.AddAsync(sale);

            await _unitOfWork.CommitTransactionAsync();
            return Ok(sale);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            return BadRequest("Transaction failed");
        }
    }
}
```

## Available Repositories

1. **IUserRepository**: Find users by email, get user with credentials
2. **IProductRepository**: Get products by type, get active products with details
3. **ICustomerRepository**: Get customers with contacts, get active customers
4. **IUnitOfWork**: Coordinate multiple repositories with transaction support

## Seeding Data

The database is automatically seeded on startup with:

- All 50 US states
- 1 admin user + 10 regular users + 10 sales users + 5 accounting users
- User credentials for all users
- 120 customers
- 1,200 customer contacts (10 per customer)
- Multiple products of each type and gender
- Product colors and inventory sizes
- 1,000 product sales transactions

## Database Configuration

The database connection string defaults to `Data Source=lucky-day.db` in the working directory. To customize:

```csharp
builder.Services.AddDbServices("Data Source=path/to/your/database.db");
```

## Notes

- The database file is excluded from git via `.gitignore`
- All entity IDs are GUIDs
- Timestamps (dateCreated, dateUpdated) are automatically managed
- Foreign key relationships are configured with appropriate delete behaviors
- The repository pattern provides abstraction over EF Core for testability
