using LuckyDay.Db.Data;
using LuckyDay.Db.Enums;
using LuckyDay.Db.Models;
using LuckyDay.Db.Repositories;
using LuckyDay.Db.Tests.TestInfrastructure;
using Microsoft.EntityFrameworkCore;

namespace LuckyDay.Db.Tests;

public class RepositoryCrudTests
{
  [Fact]
  public async Task UserRepository_CanCreateReadUpdateAndDeleteUser()
  {
    await using var testDatabase = await SqliteTestDatabase.CreateEmptyAsync();

    await using var context = testDatabase.CreateContext();
    var state = await AddStateAsync(context);
    var repository = new UserRepository(context);

    var user = new User
    {
      FirstName = "Test",
      LastName = "User",
      Gender = Gender.PreferNotToSay,
      Email = "repo.user@example.com",
      StreetAddress = "1 Main St",
      City = "Austin",
      StateId = state.Id,
      Zip = "78701",
      Phone = "555-0100",
      JobTitle = "Engineer",
      ImageId = 1,
      IsActive = true
    };

    await repository.AddAsync(user);

    var loadedByEmail = await repository.GetByEmailAsync("repo.user@example.com");
    Assert.NotNull(loadedByEmail);
    Assert.Equal(user.Id, loadedByEmail.Id);

    user.LastName = "Updated";
    await repository.UpdateAsync(user);

    var updated = await repository.GetByEmailAsync(user.Email);
    Assert.NotNull(updated);
    Assert.Equal("Updated", updated.LastName);

    Assert.True(await repository.ExistsAsync(user.Id));

    await repository.DeleteAsync(user.Id);

    Assert.False(await repository.ExistsAsync(user.Id));
  }

  [Fact]
  public async Task UserRepository_GetUserWithCredentialAsync_LoadsCredential()
  {
    await using var testDatabase = await SqliteTestDatabase.CreateEmptyAsync();

    await using var context = testDatabase.CreateContext();
    var state = await AddStateAsync(context);

    var user = new User
    {
      FirstName = "Credential",
      LastName = "User",
      Gender = Gender.Male,
      Email = "credential.user@example.com",
      StreetAddress = "2 Main St",
      City = "Austin",
      StateId = state.Id,
      Zip = "78701",
      Phone = "555-0200",
      JobTitle = "Operator",
      ImageId = 2,
      IsActive = true
    };

    context.Users.Add(user);
    context.UserCredentials.Add(new UserCredential
    {
      UserId = user.Id,
      Password = "pw",
      Role = Role.User
    });
    await context.SaveChangesAsync();

    var repository = new UserRepository(context);
    var loaded = await repository.GetUserWithCredentialAsync(user.Id);

    Assert.NotNull(loaded);
    Assert.NotNull(loaded.UserCredential);
    Assert.Equal(Role.User, loaded.UserCredential.Role);
  }

  [Fact]
  public async Task ProductRepository_QueriesByTypeActiveAndDetails()
  {
    await using var testDatabase = await SqliteTestDatabase.CreateEmptyAsync();

    await using var context = testDatabase.CreateContext();
    var repository = new ProductRepository(context);

    var activeShirt = new Product
    {
      Name = "Active Shirt",
      Description = "Test",
      Price = "49.99",
      Gender = Gender.Female,
      ProductType = ProductType.Shirt,
      IsActive = true
    };

    var inactiveHat = new Product
    {
      Name = "Inactive Hat",
      Description = "Test",
      Price = "19.99",
      Gender = Gender.Male,
      ProductType = ProductType.Hat,
      IsActive = false
    };

    await repository.AddAsync(activeShirt);
    await repository.AddAsync(inactiveHat);

    context.ProductColors.Add(new ProductColor
    {
      ProductId = activeShirt.Id,
      Color = Color.Blue
    });

    context.ProductInventories.Add(new ProductInventory
    {
      ProductId = activeShirt.Id,
      Size = Size.Medium,
      Quantity = 10
    });

    await context.SaveChangesAsync();

    var shirtProducts = await repository.GetProductsByTypeAsync(ProductType.Shirt);
    Assert.Single(shirtProducts);

    var activeProducts = await repository.GetActiveProductsAsync();
    Assert.Single(activeProducts);

    var details = await repository.GetProductWithDetailsAsync(activeShirt.Id);
    Assert.NotNull(details);
    Assert.Single(details.ProductColors);
    Assert.Single(details.ProductInventories);
  }

  [Fact]
  public async Task CustomerRepository_CanQueryActiveAndContacts()
  {
    await using var testDatabase = await SqliteTestDatabase.CreateEmptyAsync();

    await using var context = testDatabase.CreateContext();
    var state = await AddStateAsync(context);
    var repository = new CustomerRepository(context);

    var activeCustomer = new Customer
    {
      Name = "Active Customer",
      StreetAddress = "10 Market",
      City = "Austin",
      StateId = state.Id,
      Zip = "78702",
      Phone = "555-0300",
      IsActive = true
    };

    var inactiveCustomer = new Customer
    {
      Name = "Inactive Customer",
      StreetAddress = "11 Market",
      City = "Austin",
      StateId = state.Id,
      Zip = "78702",
      Phone = "555-0301",
      IsActive = false
    };

    await repository.AddAsync(activeCustomer);
    await repository.AddAsync(inactiveCustomer);

    context.CustomerContacts.Add(new CustomerContact
    {
      CustomerId = activeCustomer.Id,
      FirstName = "Casey",
      LastName = "Contact",
      Email = "contact@example.com",
      StreetAddress = "12 Market",
      City = "Austin",
      StateId = state.Id,
      Zip = "78702",
      Phone = "555-0302",
      ImageId = 10,
      IsActive = true
    });

    await context.SaveChangesAsync();

    var activeCustomers = await repository.GetActiveCustomersAsync();
    Assert.Single(activeCustomers);

    var withContacts = await repository.GetCustomerWithContactsAsync(activeCustomer.Id);
    Assert.NotNull(withContacts);
    Assert.Single(withContacts.CustomerContacts);
  }

  [Fact]
  public async Task UnitOfWork_RollbackTransaction_UndoesRepositoryWrites()
  {
    await using var testDatabase = await SqliteTestDatabase.CreateEmptyAsync();

    string stateId;

    await using (var setupContext = testDatabase.CreateContext())
    {
      var state = await AddStateAsync(setupContext);
      stateId = state.Id;
    }

    await using (var transactionContext = testDatabase.CreateContext())
    {
      var unitOfWork = new UnitOfWork(transactionContext);
      await unitOfWork.BeginTransactionAsync();

      var user = new User
      {
        FirstName = "Rollback",
        LastName = "Candidate",
        Gender = Gender.Female,
        Email = "rollback@example.com",
        StreetAddress = "50 River",
        City = "Austin",
        StateId = stateId,
        Zip = "78703",
        Phone = "555-0400",
        JobTitle = "Sales",
        ImageId = 11,
        IsActive = true
      };

      await unitOfWork.Users.AddAsync(user);
      await unitOfWork.RollbackTransactionAsync();
    }

    await using var verifyContext = testDatabase.CreateContext();
    Assert.False(await verifyContext.Users.AnyAsync(u => u.Email == "rollback@example.com"));
  }

  private static async Task<State> AddStateAsync(LuckyDayDbContext context)
  {
    var state = new State
    {
      Name = "Texas",
      Code = $"TX-{Guid.NewGuid():N}"[..8]
    };

    context.States.Add(state);
    await context.SaveChangesAsync();
    return state;
  }
}
