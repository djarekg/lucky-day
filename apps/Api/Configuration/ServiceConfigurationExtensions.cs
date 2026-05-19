using LuckyDay.Api.Services;

namespace LuckyDay.Api.Configuration;

public static class ServiceConfigurationExtensions
{
  public static IServiceCollection AddApiServiceConfiguration(this IServiceCollection services)
  {
    services.AddControllers().AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.Converters.Add(
        new System.Text.Json.Serialization.JsonStringEnumConverter(
          new UpperInvariantJsonNamingPolicy()));
    });
    services.ConfigureHttpJsonOptions(options =>
    {
      options.SerializerOptions.Converters.Add(
        new System.Text.Json.Serialization.JsonStringEnumConverter(
          new UpperInvariantJsonNamingPolicy()));
    });

    services.AddScoped<AuthService>();
    services.AddScoped<CustomerService>();
    services.AddScoped<ProductService>();
    services.AddScoped<UserService>();
    services.AddScoped<StateService>();
    services.AddScoped<CustomerContactService>();
    services.AddScoped<UserCredentialService>();
    services.AddScoped<ProductColorService>();
    services.AddScoped<ProductInventoryService>();
    services.AddScoped<ProductSaleService>();
    services.AddScoped<DashboardService>();
    services.AddScoped<SearchService>();
    services.AddScoped<DashboardWidgetService>();
    services.AddScoped<UserDashboardService>();

    return services;
  }
}
