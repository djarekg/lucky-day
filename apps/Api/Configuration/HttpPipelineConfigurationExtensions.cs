namespace Api.Configuration;

public static class HttpPipelineConfigurationExtensions
{
  public static void UseApiHttpConfiguration(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
      app.MapOpenApi();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
  }
}
