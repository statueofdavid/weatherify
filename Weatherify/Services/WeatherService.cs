// https://api.open-meteo.com/v1/forecast?latitude=37.2441&longitude=-76.782&current=temperature_2m,is_day,precipitation,pressure_msl,surface_pressure,wind_speed_10m,wind_direction_10m,wind_gusts_10m&hourly=temperature_2m,relative_humidity_2m,precipitation_probability,pressure_msl,surface_pressure,cloud_cover,visibility&daily=sunrise,sunset,daylight_duration,uv_index_max,precipitation_probability_max&temperature_unit=fahrenheit&wind_speed_unit=mph&precipitation_unit=inch&timezone=America%2FNew_York
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Weatherify.Models;

namespace Weatherify.Services
{
  public class WeatherService {
    private readonly HttpClient _httpClient;
    private readonly WeatherifyDbContext _context;
    private readonly ILogger<LocationService> _logger;

    public WeatherService(HttpClient httpClient, WeatherifyDbContext context, ILogger<LocationService> logger) {
        _httpClient = httpClient;
	_context = context;
	_logger = logger;
    }

    public async Task<Weather> fetchWeatherByLatLon(double? lat, double? lon) {
        _logger.LogInformation("Fetching weather data by Latitude: {lat}, Longitude: {lon}", lat, lon);
	    
	string url = BuildOpenMeteoUrl(lat, lon);

        try {
            var weatherData = new Weather();
	    var responseString = await _httpClient.GetStringAsync(url);
	   
	    if(responseString != null) {
	      weatherData = ParseWeatherResponse(responseString);
	    }

            if(weatherData != null) {
	      await saveWeatherAsync(weatherData);
	    }

	    return weatherData!;
        }
        catch (HttpRequestException e) {
            _logger.LogInformation($"Error fetching: {e.Message}");
            return new Weather();
        }
    }

    public async Task saveWeatherAsync(Weather weather) {
      if(weather == null) {
        throw new ArgumentNullException(nameof(weather), "Weather was null, nope.");
      }
      _context.Weathers.Add(weather);
      await _context.SaveChangesAsync();
    }

    private string BuildOpenMeteoUrl(double? lat, double? lon) {

	var queryParams = new Dictionary<string, string?> {
          {"latitude", lat.ToString()},
          {"longitude", lon.ToString()},
          {"temperature_unit", "fahrenheit"},
          {"wind_speed_unit", "mph"},
          {"precipitation_unit", "inch"},
          {"timezone", "America/New_York"},
          {"current", "temperature_2m,is_day,precipitation,pressure_msl,surface_pressure,wind_speed_10m,wind_direction_10m,wind_gusts_10m"},
          {"hourly", "temperature_2m,relative_humidity_2m,precipitation_probability,pressure_msl,surface_pressure,cloud_cover,visibility"},
          {"daily", "sunrise,sunset,daylight_duration,uv_index_max,precipitation_probability_max"}
        };

        var url = "https://api.open-meteo.com/v1/forecast";
        var queryString = string.Join("&", queryParams.Select(x => $"{x.Key}={x.Value}"));
        return $"{url}?{queryString}";
    }

    private Weather? ParseWeatherResponse(string responseString) {
      var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
      var result = JsonSerializer.Deserialize<WeatherDetails>(responseString, options);

      if(result == null) {
        throw new Exception("No Weather data available.");
      }

      _logger.LogInformation($"response: {result}");
      var response = result;
      return new Weather {
        Latitude = response.latitude,
        Longitude = response.longitude,
        
	GenerationTimeMs = response.generation_time_ms,
        UtcOffsetSeconds = response.utc_off_set_seconds,

        TimezoneAbbr = response.timezone_abbreviation,
        Elevation = response.elevation,
	CurrentUnits = response.current_units,
	Current = response.current,
	HourlyUnits = response.hourly_units,
	Hourly = response.hourly,
	Daily = response.daily,
	DaylightDuration = response.daylight_duration,
	UvIndexMax = response.uv_index_max,
	PrecipitationProbabilityMax = response.precipitation_probability_max
      };
    }

    public class WeatherDetails {
      public double? latitude {get; set;}
      public double? longitude {get; set;}

      public float? generation_time_ms {get; set;}
      public double? utc_off_set_seconds {get; set;}

      public string? timezone_abbreviation {get; set;}
      public double? elevation {get; set;}

      public CurrentUnits? current_units {get; set;}
      public Current? current {get; set;}
      public HourlyUnits? hourly_units {get; set;}

      public Hourly? hourly {get; set;}
      public DailyUnits? daily_units {get; set;}
      public Daily? daily {get; set;}

      public List<double>? daylight_duration {get; set;}
      public List<double>? uv_index_max {get; set;}
      public List<double>? precipitation_probability_max {get; set;}
    }
  }
}
