namespace Db.Seeding;

public class DatabaseSeeder
{
  public static async Task SeedDatabaseAsync(LuckyDayDbContext context)
  {
    try
    {
      Console.WriteLine("Starting database seeding...");

      // Ensure database is created
      await context.Database.EnsureCreatedAsync();

      // Seed in order of dependencies
      Console.WriteLine("Seeding States...");
      await StateSeed.SeedStatesAsync(context);

      Console.WriteLine("Seeding Users...");
      await UserSeed.SeedUsersAsync(context);

      Console.WriteLine("Seeding User Credentials...");
      await UserCredentialSeed.SeedUserCredentialsAsync(context);

      Console.WriteLine("Seeding Customers...");
      await CustomerSeed.SeedCustomersAsync(context);

      Console.WriteLine("Seeding Customer Contacts...");
      await CustomerContactSeed.SeedCustomerContactsAsync(context);

      Console.WriteLine("Seeding Products...");
      await ProductSeed.SeedProductsAsync(context);

      Console.WriteLine("Seeding Product Colors...");
      await ProductColorSeed.SeedProductColorsAsync(context);

      Console.WriteLine("Seeding Product Inventories...");
      await ProductInventorySeed.SeedProductInventoriesAsync(context);

      Console.WriteLine("Seeding Product Sales...");
      await ProductSaleSeed.SeedProductSalesAsync(context);

      Console.WriteLine("Database seeding completed successfully!");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An error occurred during seeding: {ex.Message}");
      throw;
    }
  }
}
