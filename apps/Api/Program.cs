using LuckyDay.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);
var jwtConfiguration = new JwtConfigurationService(builder.Configuration);

// Add services to the container.
builder.Services.AddDatabaseConfiguration(builder.Configuration, builder.Environment);
builder.Services.AddSingleton<IJwtConfigurationService>(jwtConfiguration);
builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddJwtAuthenticationConfiguration(jwtConfiguration);
builder.Services.AddApiServiceConfiguration();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.InitializeDatabaseAsync();
app.UseApiHttpConfiguration();

app.MapControllers();

app.Run();
