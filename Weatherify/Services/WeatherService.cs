// https://api.open-meteo.com/v1/forecast?latitude=37.2441&longitude=-76.782&current=temperature_2m,is_day,precipitation,pressure_msl,surface_pressure,wind_speed_10m,wind_direction_10m,wind_gusts_10m&hourly=temperature_2m,relative_humidity_2m,precipitation_probability,pressure_msl,surface_pressure,cloud_cover,visibility&daily=sunrise,sunset,daylight_duration,uv_index_max,precipitation_probability_max&temperature_unit=fahrenheit&wind_speed_unit=mph&precipitation_unit=inch&timezone=America%2FNew_York
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Setialization;
using System.Threading.Tasks;

public class WeatherService {
    private readonly HttpClient _httpClient;
    private readonly WeatherifyDbContext _context;
    private readonly ILogger<LocationService> _logger;

    public WeatherService(HttpClient httpClient, WeatherifyDbContext context, ILogger<LocationService> logger) {
        _httpClient = httpClient;
	_context = context;
	_logger = logger;
    }

    public async Task<Weather> fetchWeatherByLatLon(string lat, string lon) {
        _logger.LogInformation("Fetching weather data by Latitude: {lat}, Longitude: {lon}, lat, lon");
	    
	string url = BuildOpenMeteoUrl(lat, lon);

        try {
            var responseString = await _httpClient.GetStringAsync(url);
            _logger.LogInformation(responseString);
	    
	    var weatherData = ParseWeatherResponse(responseString);

            if(weatherDate != null) {
	      await saveWeatherAsync(weatherData);
	    }

	    return weatherData;
        }
        catch (HttpRequestException e) {
            Console.WriteLine($"Error fetching: {e.Message}");
            return null;
        }
    }

    public async Task saveWeatherAsync(Weather weather) {
      if(weather == null) {
        throw new ArgumentNullException(nameof(weather), "Weather was null, nope.");
      }
      _context.Weathers.Add(weather);
      await _context.SaveChangesAsync();
    }

    private string BuildOpenMeteoUrl(string lat, string lon) {
        var queryParams = new Dictionary<string, string> {
            {"latitude", lat},
            {"longitude", lon},
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

    private Weather ParseWeatherResponse(string responseString) {
      var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
      var result = JsonSerializer.Deserialize<WeatherDetails[]>(responseString, options);

      if(result == null || result.Length == 0) {
        throw new Exception("No Weather data available.");
      }

      System.Diagnostics.Debug.WriteLine($"response: {result[0]}");
      var response = result[0];
      return new Weather {
        latitude = response.latitude,
        longitude = response.longitude,
        generationtimeMs response.
  public int? utcOffSetSeconds {get; set;}

  public string? timezoneAbbr {get; set;}
  public int? elevation {get; set;}

  public CurrentUnits? currentUnits {get; set;}
  public Current? current {get; set;}
  public HourlyUnits? hourlyUnits {get; set;}

  public Hourly? hourly {get; set;}
  public DailyUnits? dailyUnits {get; set;}
  public Daily? daily {get; set;}

  public List<double>? daylightDuration {get; set;}
  public List<double>? uvIndexMax {get; set;}
  public List<double>? precipitionProbabilityMax {get; set;}
      }
    }
    public class Weather {
  public double? latitude {get; set;}
  public double? longitude {get; set;}

  public float? generationtimeMs {get; set;}
  public int? utcOffSetSeconds {get; set;}

  public string? timezoneAbbr {get; set;}
  public int? elevation {get; set;}

  public CurrentUnits? currentUnits {get; set;}
  public Current? current {get; set;}
  public HourlyUnits? hourlyUnits {get; set;}

  public Hourly? hourly {get; set;}
  public DailyUnits? dailyUnits {get; set;}
  public Daily? daily {get; set;}

  public List<double>? daylightDuration {get; set;}
  public List<double>? uvIndexMax {get; set;}
  public List<double>? precipitionProbabilityMax {get; set;}

  public class CurrentUnits {
    public string? time {get; set;}
    public string? interval {get; set;}
    public string? temperatureTwoM {get; set;}
    public string? isDay {get; set;}
    public string? precipitation {get; set;}
    public string? pressureMsl {get; set;}
    public string? surfacePressure {get; set;}
    public string? windSpeedTenM {get; set;}
    public char? windDirectionTenM {get; set;}
    public string? windGustsTenM {get; set;}
  }

  public class Current {
    public DateTime? time {get; set;}

    public int? interval {get; set;}
    public int? windDirectionTenM {get; set;}

    public int? isDay {get; set;}
    public int? precipitation {get; set;}

    public double? pressureMsl {get; set;}
    public double? surfacePressure {get; set;}

    public double? temperatureTwoM {get; set;}
    public double? windSpeedTenM {get; set;}
    public double windGustsTenM {get; set;}
  }
  public class HourlyUnits {
    public DateTime time {get; set;}

    public string? temperatureTwoM {get; set;}
    public string? pressureMsl {get; set;}
    public string? visibility {get; set;}

    public char? precipitationProbability {get; set;}
    public char? surfacePressure {get; set;}

    public char? relativeHumidityTwoM {get; set;}
    public char? cloudCover {get; set;}
  }

  public class Hourly {
    public List<DateTime>? time {get; set;}

    public List<int>? cloudCover {get; set;}
    public List<int>? precipitationProbability {get; set;}
    public List<int>? relativeHumidityTwoM {get; set;}

    public List<double>? pressureMsl {get; set;}
    public List<double>? surfacePressure {get; set;}
    public List<double>? temperatureTwoM {get; set;}
    public List<double>? visibility {get; set;}
  }

  public class DailyUnits {
    public string? time {get; set;}
    public string? sunrise {get; set;}
    public string? sunset {get; set;}

    public char? daylightDuration {get; set;}
    public char? precipitationProbabilityMax {get; set;}
  }

  public class Daily {
    public List<Date>? time {get; set;}
    public List<DateTime>? sunrise {get; set;}
    public List<DateTime>? sunset {get; set;}
  }
}

