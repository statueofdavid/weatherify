using Microsoft.Extensions.Diagnostics.HealthChecks;

public class DatabaseHealthCheck : IHealthCheck
{
  private readonly IConfiguration _configuration;
  private readonly WeatherifyDbContext _dbContext;

  public DatabaseHealthCheck(IConfiguration configuration, WeatherifyDbContext dbContext)
  {
    _configuration = configuration;
    _dbContext = dbContext;
  }

  public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken)
  {
    try
    {
      await _dbContext.Database.CanConnectAsync();
      return HealthCheckResult.Healthy();
    }
    catch(Exception e)
    {
      return HealthCheckResult.Unhealthy(e.Message);
    }
  }
}
