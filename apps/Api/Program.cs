using Api.Configuration;
using Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabaseConfiguration(builder.Configuration, builder.Environment);
builder.Services.AddControllers();
builder.Services.AddJwtAuthenticationConfiguration(builder.Configuration);
builder.Services.AddScoped<AuthService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

await app.InitializeDatabaseAsync();
app.UseApiHttpConfiguration();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
