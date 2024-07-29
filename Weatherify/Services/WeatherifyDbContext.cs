using Microsoft.EntityFrameworkCore;

using Weatherify.Models;

namespace Weatherify.Services
{
  public class WeatherifyDbContext : DbContext {
    public WeatherifyDbContext(DbContextOptions<WeatherifyDbContext> options) : base(options) { }

    public DbSet<Location> Locations {get; set;}
    public DbSet<Weather> Weathers {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder options) {
      options.UseSqlite("Data source=weatherify.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<Weather>(entity => {
        entity.ToTable("Weathers");
        entity.HasKey(e => e.Id);
        
	entity.Property(e => e.Latitude).HasPrecision(17,10);
        entity.Property(e => e.Longitude).HasPrecision(17,10);
        entity.Property(e => e.GenerationTimeMs).HasPrecision(28,7);
        
	entity.Property(e => e.UtcOffsetSeconds).HasPrecision(28,7);
        entity.Property(e => e.TimezoneAbbr).HasMaxLength(20);
        entity.Property(e => e.Elevation).HasPrecision(18,10);
	
	entity.HasOne(w => w.CurrentUnits)
	  .WithOne()
	  .HasForeginKey<CurrentUnits>(cu => cu.WeatherId);

	entity.HasOne(w => w.Current)
	  .WithOne()
	  .HasForeginKey<Current>(c => c.WeatherId);
	
	entity.HasOne(w => w.HourlyUnits)
	  .WithOne()
	  .HasForeginKey<HourlyUnits>(hu => hu.WeatherId);

	entity.HasOne(w => w.Hourly)
	  .WithOne()
	  .HasForeginKey<Hourly>(h => h.WeatherId);
	
	entity.HasOne(w => w.DailyUnits)
	  .WithOne()
	  .HasForeginKey<DailyUnits>(du => du.WeatherId);

	entity.HasOne(w => w.Daily)
	  .WithOne()
	  .HasForeginKey<Daily>(d => d.WeatherId);

	entity.Property(w => w.DaylightDuration).HasConversion(new DoubleListConverter());
        entity.Property(w => w.UvIndexMax).HasConversion(new DoubleListConverter());
        entity.Property(w => w.PrecipitationProbabilityMax).HasConversion(new DoubleListConverter());
      });

      modelBuilder.Entity<Location>(entity => {
	entity.ToTable("Locations");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.City).HasMaxLength(50);
        entity.Property(e => e.State).HasMaxLength(50);
        entity.Property(e => e.Latitude).HasPrecision(17,10);
        entity.Property(e => e.Longitude).HasPrecision(17,10);
      });
    }
  } 
}
