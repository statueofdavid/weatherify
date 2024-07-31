using Microsoft.EntityFrameworkCore;

using Weatherify.Models;

namespace Weatherify.Services
{
  public class WeatherifyDbContext : DbContext {
    public WeatherifyDbContext(DbContextOptions<WeatherifyDbContext> options) : base(options) { }

    public DbSet<Current> CurrentData {get; set;}
    public DbSet<CurrentUnits> CurrentDataUnits {get; set;}
    
    public DbSet<Daily> DailyData {get; set;}
    public DbSet<DailyUnits> DailyDataUnits {get; set;}

    public DbSet<Hourly> HourlyData {get; set;}
    public DbSet<HourlyUnits> HourlyDataUnits {get; set;}

    public DbSet<Location> Locations {get; set;}
    public DbSet<Weather> Weathers {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder options) {
      options.UseSqlite("Data source=weatherify.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<Hourly>(entity => {
        entity.ToTable("HourlyData");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Time).HasConversion(new DoubleListConverter());
        entity.Property(e => e.TemperatureTwoM).HasConversion(new DoubleListConverter());
        entity.Property(e => e.PressureMsl).HasConversion(new DoubleListConverter());
        entity.Property(e => e.PrecipitationProbability).HasConversion(new DoubleListConverter());
        entity.Property(e => e.SurfacePressure).HasConversion(new DoubleListConverter());
        entity.Property(e => e.RelativeHumidityTwoM).HasConversion(new DoubleListConverter());
        entity.Property(e => e.CloudCover).HasConversion(new DoubleListConverter()); 
      });

      modelBuilder.Entity<HourlyUnits>(entity => {
        entity.ToTable("HourlyDataUnits");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Time).HasMaxLength(50);
        entity.Property(e => e.TemperatureTwoM).HasMaxLength(50);
        entity.Property(e => e.PressureMsl).HasMaxLength(50);
        entity.Property(e => e.PrecipitationProbability).HasMaxLength(1);
        entity.Property(e => e.SurfacePressure).HasMaxLength(50);
        entity.Property(e => e.RelativeHumidityTwoM).HasMaxLength(1);
        entity.Property(e => e.CloudCover).HasMaxLength(1);
      });

      modelBuilder.Entity<Location>(entity => {
	entity.ToTable("Locations");
        entity.Property(e => e.State).HasMaxLength(50);
        entity.Property(e => e.Latitude).HasPrecision(17,10);
        entity.Property(e => e.Longitude).HasPrecision(17,10);
      });

      modelBuilder.Entity<Weather>(entity => {
        entity.ToTable("Weathers");
        entity.HasKey(e => e.Id);
        
	entity.Property(e => e.Latitude).HasPrecision(17,10);
        entity.Property(e => e.Longitude).HasPrecision(17,10);
        entity.Property(e => e.GenerationTimeMs).HasPrecision(28,7);
        
	entity.Property(e => e.UtcOffsetSeconds).HasPrecision(28,7);
        entity.Property(e => e.TimezoneAbbr).HasMaxLength(20);
        entity.Property(e => e.Elevation).HasPrecision(18,10);
	
	entity.HasOne(e => e.CurrentUnits);
	entity.HasOne(e => e.Current);
	entity.HasOne(e => e.HourlyUnits);
	
	entity.HasOne(e => e.Hourly);
	entity.HasOne(e => e.DailyUnits);
	entity.HasOne(e => e.Daily);

	entity.Property(w => w.DaylightDuration).HasConversion(new DoubleListConverter());
        entity.Property(w => w.UvIndexMax).HasConversion(new DoubleListConverter());
        entity.Property(w => w.PrecipitationProbabilityMax).HasConversion(new DoubleListConverter());
      });

    }
  } 
}
