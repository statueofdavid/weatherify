using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Weatherify.Components;

public class Program 
{
  private readonly IConfiguration _configuration;

  public Program(IConfiguration configuration) 
  {
    _configuration = configuration;
  }

  public static void Main(string[] args) 
  {
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
    builder.Configuration.AddEnvironmentVariables();
    builder.Services.AddRazorComponents().AddInteractiveServerComponents();
    
    builder.Services.AddHttpClient<LocationService>();
    builder.Services.AddScoped<LocationService>();
    
    builder.Services.AddHttpClient();

    var configuration = builder.Configuration;

    builder.Services.AddDbContext<WeatherifyDbContext>(options =>
    	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
        .EnableDetailedErrors()
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Information));

    builder.Services.AddHealthChecks();

    var app = builder.Build();

    if (!app.Environment.IsDevelopment()) {
      app.UseExceptionHandler("/Error", createScopeForErrors: true);
      app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseAntiforgery();

    app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

    app.MapHealthChecks("/health");
    app.Run();
  }
}
