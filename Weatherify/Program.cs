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

    builder.Configuration.AddEnvironmentVariables();
    builder.Services.AddRazorComponents().AddInteractiveServerComponents();

    var configuration = builder.Configuration;
    
    builder.Services.AddDbContext<WeatherifyDbContext>(options => {
      try {
        options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
      } catch (Exception e) {
        Console.WriteLine($"Error connecting to database: {e.Message}");
      }
    });

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
