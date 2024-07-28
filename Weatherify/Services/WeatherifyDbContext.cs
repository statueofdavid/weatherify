using Microsoft.EntityFrameworkCore;

using Weatherify.Models;

namespace Weatherify.Services {
  public class WeatherifyDbContext : DbContext {
  public WeatherifyDbContext(DbContextOptions<WeatherifyDbContext> options) : base(options) { }

  public DbSet<Location> Locations {get; set;}
  public DbSet<Weather> Weathers {get; set;}

  protected override void OnConfiguring(DbContextOptionsBuilder options) {
  options.UseSqlite("Data source=weatherify.db");
  }
  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.Entity<Weather>(entity => {
      entity.HasKey(e => e.Id);
      entity.HasOne(e => Latitude).HasColumnType(typeof(double));
      entity.HasOne(e => Longitude).HasColumnType(typeof(double));
      entity.HasOne(e => GenerationTimeMs).HasColumnType(typeof(float));
      entity.HasOne(e => UtcOffsetSeconds).HasColumnType(typeof(double));
      entity.HasOne(e => Timezone).HasColumnType(typeof(string));
      entity.HasOne(e => Elevation).HasColumnType(typeof(double));
    });

    modelBuilder.Entity<Location>(entity => {
      entity.HasKey(e => Id);
      entity.HasOne(e => City).HasColumnType(typeof(string));
      entity.HasOne(e => State).HasColumnType(typeof(string));
      entity.HasOne(e => Latitude).HasColumnType(typeof(double));
      entity.HasOne(e => Longitude).HasColumnType(typeof(double));
    });
  }
} 
}
