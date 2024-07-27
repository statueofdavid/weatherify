using Microsoft.EntityFrameworkCore;

public class WeatherifyDbContext : DbContext {
  public WeatherifyDbContext(DbContextOptions<WeatherifyDbContext> options) : base(options) { }

  public DbSet<Location> Locations {get; set;}
  public DbSet<Weather> Weathers {get; set;}

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.Entity<Weather>(entity => {
      entity.HasKey(e => e.id);
      entity.Property(e => latitude).HasColumnType(typeof(double));
      entity.Property(e => longitude).HasColumnType(typeof(double));
      entity.Property(e => generationTimeMs).HasColumnType(typeof(float));
      entity.Property(e => utcOffsetSeconds).HasColumnType(typeof(double));
      entity.Property(e => timezone).HasColumnType(typeof(string));
      entity.Property(e => elevation).HasColumnType(typeof(double));
    });

    modelBuilder.Entity<Location>(entity => {
      entity.HasKey(e => id);
      entity.Property(e => city).HasColumnType(typeof(string));
      entity.Property(e => state).HasColumnType(typeof(string));
      entity.Property(e => latitude).HasColumnType(typeof(double));
      entity.Property(e => longitude).HasColumnType(typeof(double));
    });
  }
} 
