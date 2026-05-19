using LuckyDay.Db;
using Microsoft.Data.Sqlite;

namespace LuckyDay.Api.Configuration;

public static class DatabaseConfigurationExtensions
{
  public static IServiceCollection AddDatabaseConfiguration(
      this IServiceCollection services,
      IConfiguration configuration,
      IWebHostEnvironment environment)
  {
    var configuredConnectionString = configuration.GetConnectionString("LuckyDayDb")
        ?? throw new InvalidOperationException("ConnectionStrings:LuckyDayDb is required.");

    var sqliteConnectionBuilder = new SqliteConnectionStringBuilder(configuredConnectionString);

    if (string.IsNullOrWhiteSpace(sqliteConnectionBuilder.DataSource))
    {
      throw new InvalidOperationException("ConnectionStrings:LuckyDayDb must include a SQLite Data Source.");
    }

    if (!Path.IsPathRooted(sqliteConnectionBuilder.DataSource))
    {
      sqliteConnectionBuilder.DataSource = Path.GetFullPath(
          sqliteConnectionBuilder.DataSource,
          environment.ContentRootPath);
    }

    var dbDirectory = Path.GetDirectoryName(sqliteConnectionBuilder.DataSource);
    if (!string.IsNullOrWhiteSpace(dbDirectory))
    {
      Directory.CreateDirectory(dbDirectory);
    }

    return services.AddDbServices(sqliteConnectionBuilder.ToString());
  }

  public static async Task InitializeDatabaseAsync(this WebApplication app)
  {
    if (!app.Environment.IsDevelopment())
    {
      return;
    }

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<LuckyDay.Db.Data.LuckyDayDbContext>();
    await DbInitializer.InitializeDatabaseAsync(dbContext);
  }
}
