using Db.Data;

namespace Db.Tests.TestInfrastructure;

internal sealed class SqliteTestDatabase : IAsyncDisposable
{
  private readonly string _databasePath;

  private SqliteTestDatabase(string databasePath)
  {
    _databasePath = databasePath;
    ConnectionString = $"Data Source={databasePath}";
  }

  public string ConnectionString { get; }

  public static async Task<SqliteTestDatabase> CreateEmptyAsync()
  {
    var databasePath = Path.Combine(Path.GetTempPath(), $"lucky-day-tests-{Guid.NewGuid():N}.db");
    var database = new SqliteTestDatabase(databasePath);

    await using var context = database.CreateContext();
    await context.Database.EnsureCreatedAsync();

    return database;
  }

  public static async Task<SqliteTestDatabase> CreateSeededAsync()
  {
    var databasePath = Path.Combine(Path.GetTempPath(), $"lucky-day-tests-{Guid.NewGuid():N}.db");
    var database = new SqliteTestDatabase(databasePath);

    await DbInitializer.InitializeDatabaseAsync(database.ConnectionString);

    return database;
  }

  public LuckyDayDbContext CreateContext()
  {
    return new LuckyDayDbContext(ConnectionString);
  }

  public ValueTask DisposeAsync()
  {
    try
    {
      if (File.Exists(_databasePath))
      {
        File.Delete(_databasePath);
      }
    }
    catch
    {
      // Ignore cleanup failures for temp test artifacts.
    }

    return ValueTask.CompletedTask;
  }
}
