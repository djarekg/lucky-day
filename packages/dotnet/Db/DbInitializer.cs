namespace LuckyDay.Db;

public static class DbInitializer
{
  /// <summary>
  /// Initialize the database with migrations and seeding
  /// </summary>
  public static async Task InitializeDatabaseAsync(string connectionString)
  {
    if (string.IsNullOrWhiteSpace(connectionString))
    {
      throw new ArgumentException("A valid SQLite connection string is required.", nameof(connectionString));
    }

    var context = new LuckyDayDbContext(connectionString);
    try
    {
      // Ensure database is created and migrations are applied
      await context.Database.EnsureCreatedAsync();

      // Seed the database
      await DatabaseSeeder.SeedDatabaseAsync(context);

      // Build and synchronize FTS tables used by the search endpoint
      await SearchFtsInitializer.EnsureAsync(context);
    }
    finally
    {
      await context.DisposeAsync();
    }
  }

  /// <summary>
  /// Initialize the database with an existing context
  /// </summary>
  public static async Task InitializeDatabaseAsync(LuckyDayDbContext context)
  {
    // Ensure database is created and migrations are applied
    await context.Database.EnsureCreatedAsync();

    // Seed the database
    await DatabaseSeeder.SeedDatabaseAsync(context);

    // Build and synchronize FTS tables used by the search endpoint
    await SearchFtsInitializer.EnsureAsync(context);
  }
}
