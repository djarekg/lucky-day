namespace LuckyDay.Db;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddDbServices(this IServiceCollection services, string connectionString)
  {
    if (string.IsNullOrWhiteSpace(connectionString))
    {
      throw new ArgumentException("A valid SQLite connection string is required.", nameof(connectionString));
    }

    // Register DbContext
    services.AddDbContext<LuckyDayDbContext>(options => options.UseSqlite(connectionString));

    // Register Unit of Work
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    // Register Repositories
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<ICustomerRepository, CustomerRepository>();
    services.AddScoped<IStateRepository, StateRepository>();
    services.AddScoped<IUserCredentialRepository, UserCredentialRepository>();
    services.AddScoped<ICustomerContactRepository, CustomerContactRepository>();
    services.AddScoped<IProductColorRepository, ProductColorRepository>();
    services.AddScoped<IProductInventoryRepository, ProductInventoryRepository>();
    services.AddScoped<IProductSaleRepository, ProductSaleRepository>();
    services.AddScoped<ITokenRevocationRepository, TokenRevocationRepository>();
    services.AddScoped<ISearchRepository, SearchRepository>();
    services.AddScoped<IDashboardWidgetRepository, DashboardWidgetRepository>();
    services.AddScoped<IUserDashboardRepository, UserDashboardRepository>();
    services.AddScoped<TokenRevocationRepository>();

    return services;
  }
}
