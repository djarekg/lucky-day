using LuckyDay.Db.Tests.TestInfrastructure;
using Microsoft.EntityFrameworkCore;
using LuckyDay.Db.Data;

namespace LuckyDay.Db.Tests;

public class DbInitializerTests
{
  [Fact]
  public async Task InitializeDatabaseAsync_WithEmptyConnectionString_ThrowsArgumentException()
  {
    await Assert.ThrowsAsync<ArgumentException>(() => DbInitializer.InitializeDatabaseAsync(string.Empty));
  }

  [Fact]
  public async Task InitializeDatabaseAsync_CreatesAndSeedsAllTables()
  {
    await using var testDatabase = await SqliteTestDatabase.CreateEmptyAsync();

    await DbInitializer.InitializeDatabaseAsync(testDatabase.ConnectionString);

    await using var context = testDatabase.CreateContext();

    Assert.True(await context.States.AnyAsync());
    Assert.True(await context.Users.AnyAsync());
    Assert.True(await context.UserCredentials.AnyAsync());
    Assert.True(await context.Customers.AnyAsync());
    Assert.True(await context.CustomerContacts.AnyAsync());
    Assert.True(await context.Products.AnyAsync());
    Assert.True(await context.ProductColors.AnyAsync());
    Assert.True(await context.ProductInventories.AnyAsync());
    Assert.True(await context.ProductSales.AnyAsync());

    var adminUser = await context.Users.SingleOrDefaultAsync(u => u.Email == "admin@fu.com");
    Assert.NotNull(adminUser);
  }

  [Fact]
  public async Task InitializeDatabaseAsync_WhenRunTwice_DoesNotDuplicateSeedData()
  {
    await using var testDatabase = await SqliteTestDatabase.CreateEmptyAsync();

    await DbInitializer.InitializeDatabaseAsync(testDatabase.ConnectionString);

    await using var firstContext = testDatabase.CreateContext();
    var initialCounts = await GetCountsAsync(firstContext);

    await DbInitializer.InitializeDatabaseAsync(testDatabase.ConnectionString);

    await using var secondContext = testDatabase.CreateContext();
    var secondCounts = await GetCountsAsync(secondContext);

    Assert.Equal(initialCounts, secondCounts);
  }

  private static async Task<SeededCounts> GetCountsAsync(LuckyDayDbContext context)
  {
    return new SeededCounts(
      await context.States.CountAsync(),
      await context.Users.CountAsync(),
      await context.UserCredentials.CountAsync(),
      await context.Customers.CountAsync(),
      await context.CustomerContacts.CountAsync(),
      await context.Products.CountAsync(),
      await context.ProductColors.CountAsync(),
      await context.ProductInventories.CountAsync(),
      await context.ProductSales.CountAsync());
  }

  private sealed record SeededCounts(
    int States,
    int Users,
    int UserCredentials,
    int Customers,
    int CustomerContacts,
    int Products,
    int ProductColors,
    int ProductInventories,
    int ProductSales);
}
