using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuredConnectionString = builder.Configuration.GetConnectionString("LuckyDayDb")
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
        builder.Environment.ContentRootPath);
}

var dbDirectory = Path.GetDirectoryName(sqliteConnectionBuilder.DataSource);
if (!string.IsNullOrWhiteSpace(dbDirectory))
{
    Directory.CreateDirectory(dbDirectory);
}

var dbConnectionString = sqliteConnectionBuilder.ToString();

builder.Services.AddDbServices(dbConnectionString);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
