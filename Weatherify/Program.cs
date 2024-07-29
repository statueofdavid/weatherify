using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Weatherify.Components;
using Weatherify.Services;

public class Program 
{
  private readonly IConfiguration _configuration;

  public Program(IConfiguration configuration) 
  {
    _configuration = configuration;
  }

  public static async Task Main(string[] args) 
  {
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;
    var isDevelopment = builder.Environment.IsDevelopment();
    
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
    builder.Logging.AddConsole();

    builder.Configuration.AddEnvironmentVariables();
    // https://www.nuget.org/packages/Blazor.Geolocation
    builder.Services.AddGeolocationServices();
    builder.Services.AddRazorPages();
    
    builder.Services.AddServerSideBlazor();
    builder.Services.AddRazorComponents().AddInteractiveServerComponents(); 
    
    builder.Services.AddHttpClient();
    
    builder.Services.AddHttpClient<LocationService>();
    builder.Services.AddScoped<LocationService>();
    
    builder.Services.AddHttpClient<WeatherService>();
    builder.Services.AddScoped<WeatherService>();
    
    builder.Services.AddHealthChecks();
    builder.Services.AddScoped<WeatherifyDbContext>();

    builder.Services.AddDbContext<WeatherifyDbContext>(options => {
    	options.UseSqlite(builder.Configuration
	  .GetConnectionString("DefaultConnection"))
          .EnableDetailedErrors()
          .LogTo(Console.WriteLine, LogLevel.Information);

        if(isDevelopment) {
	  options.EnableSensitiveDataLogging();
	}
     }, ServiceLifetime.Scoped);
    
    var app = builder.Build();
    
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<WeatherifyDbContext>();
	
	await dbContext.Database.EnsureCreatedAsync();
        await dbContext.Database.MigrateAsync();
    }

    if (!app.Environment.IsDevelopment()) {
      app.UseExceptionHandler("/Error", createScopeForErrors: true);
      app.UseHsts();
    }

    app.UseHttpsRedirection();
    
    app.UseAntiforgery();
    app.UseStaticFiles();

    app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
    
    app.MapHealthChecks("/health");
    app.Run();
  }
}
