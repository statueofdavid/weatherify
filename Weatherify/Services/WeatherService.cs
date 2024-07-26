// https://api.open-meteo.com/v1/forecast?latitude=37.2441&longitude=-76.782&current=temperature_2m,is_day,precipitation,pressure_msl,surface_pressure,wind_speed_10m,wind_direction_10m,wind_gusts_10m&hourly=temperature_2m,relative_humidity_2m,precipitation_probability,pressure_msl,surface_pressure,cloud_cover,visibility&daily=sunrise,sunset,daylight_duration,uv_index_max,precipitation_probability_max&temperature_unit=fahrenheit&wind_speed_unit=mph&precipitation_unit=inch&timezone=America%2FNew_York
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
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

    public async Task<Weather> fetchWeatherByLatLon(double? lat, double? lon) {
        _logger.LogInformation("Fetching weather data by Latitude: {lat}, Longitude: {lon}, lat, lon");
	    
	string url = BuildOpenMeteoUrl(lat, lon);

        try {
            var responseString = await _httpClient.GetStringAsync(url);
            _logger.LogInformation(responseString);
	    
	    var weatherData = ParseWeatherResponse(responseString);

            if(weatherData != null) {
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

    private string BuildOpenMeteoUrl(double? lat, double? lon) {
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

    private Weather? ParseWeatherResponse(string responseString) {
      var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
      var result = JsonSerializer.Deserialize<WeatherDetails>(responseString, options);

      if(result == null || result.Length == 0) {
        throw new Exception("No Weather data available.");
      }

      System.Diagnostics.Debug.WriteLine($"response: {result[0]}");
      var response = result[0];
      return new Weather {
        latitude = response.latitude,
        longitude = response.longitude,
        
	generationtimeMs = response.generation_ms,
        utcOffSetSeconds = response.utc_off_set_seconds,

        timezoneAbbr = response.timezone_abbreviation,
        elevation = response.elevation,

        daylightDuration = response.daylight_duration,
	uvIndexMax = response.uv_index_max,
        precipitionProbabilityMax = precipitation_probability_max,
        
	currentUnits = new CurrentUnits {
	  time = response.current_units.time,
          interval = response.current_units.interval,
          temperatureTwoM = response.current_unit.temperature_2m,
          isDay = response.current_units.is_day,
          precipitation = response.current_units.precipitation,
          pressureMsl = response.current_units.pressure_msl,
          surfacePressure = response.current_units.surface_pressure,
          windSpeedTenM = response.current_units.wind_speed_10m,
          windDirectionTenM = response.current_units.wind_direction_10m,
          windGustsTenM = response.current_units.wind_gusts_10m
	},

        current = new Current {},
        hourlyUnits = new HourlyUnits {},

        hourly = new Hourly {},
        dailyUnits = new DailyUnits {},
        daily = new Daily {}
      };
    }

    public class Weather {
      public double? latitude {get; set;}
      public double? longitude {get; set;}

      public float? generationtime_ms {get; set;}
      public int? utc_off_set_seconds {get; set;}

      public string? timezone_abbreviation {get; set;}
      public int? elevation {get; set;}

      public CurrentUnits? currentUnits {get; set;}
      public Current? current {get; set;}
      public HourlyUnits? hourly_units {get; set;}

      public Hourly? hourly {get; set;}
      public DailyUnits? daily_units {get; set;}
      public Daily? daily {get; set;}

      public List<double>? daylight_duration {get; set;}
      public List<double>? uv_index_max {get; set;}
      public List<double>? precipition_probability_max {get; set;}
    }

      public class CurrentUnits {
        public char? wind_direction_10m {get; set;}
        
	public string? time {get; set;}
        public string? interval {get; set;}
        public string? temperature_2m {get; set;}
        
	public string? is_day {get; set;}
        public string? precipitation {get; set;}
        public string? pressure_msl {get; set;}
        
	public string? surface_pressure {get; set;}
        public string? wind_speed_10m {get; set;}
        public string? wind_gusts_10m {get; set;}
      }

      public class Current {
        public DateTime? time {get; set;}

        public int? interval {get; set;}
        public int? wind_direction_10m {get; set;}

        public int? is_day {get; set;}
        public int? precipitation {get; set;}

        public double? pressure_msl {get; set;}
        public double? surface_pressure {get; set;}

        public double? temperature_2m {get; set;}
        public double? wind_speed_10m {get; set;}
        public double wind_gusts_10m {get; set;}
      }

      public class HourlyUnits {
        public DateTime? time {get; set;}

        public string? temperature_2m {get; set;}
        public string? pressure_msl {get; set;}
        public string? visibility {get; set;}

        public char? precipitation_probability {get; set;}
        public char? surface_pressure {get; set;}

        public char? relative_humidity_2m {get; set;}
        public char? cloud_cover {get; set;}
      }

      public class Hourly {
        public List<DateTime>? time {get; set;}

        public List<int>? cloud_cover {get; set;}
        public List<int>? precipitation_probability {get; set;}
        public List<int>? relative_humidity_2m {get; set;}

        public List<double>? pressure_msl {get; set;}
        public List<double>? surface_pressure {get; set;}
        public List<double>? temperature_2m {get; set;}
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
        public List<DateTime>? time {get; set;}
        public List<DateTime>? sunrise {get; set;}
        public List<DateTime>? sunset {get; set;}
      }
}
