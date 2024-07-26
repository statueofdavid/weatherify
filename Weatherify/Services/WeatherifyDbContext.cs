using Microsoft.EntityFrameworkCore;

public class WeatherifyDbContext : DbContext {
  public WeatherifyDbContext(DbContextOptions<WeatherifyDbContext> options) : base(options) { }

  public DbSet<Location> Locations {get; set;}
  public DbSet<Weather> Weathers {get; set;}
}
