//Currently the point of this service is to return latitude and longitude
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Weatherify.Models;

namespace Weatherify.Services
{
  public class LocationService {
  private readonly HttpClient _httpClient;
  private readonly WeatherifyDbContext _context;
  private readonly ILogger<LocationService> _logger;

  public LocationService(HttpClient httpClient, WeatherifyDbContext context, ILogger<LocationService> logger) {
    _httpClient = httpClient;
    _context = context;
    _logger = logger;
  }

  public async Task<Location?> fetchLocationData(string city, string state) {
    _logger.LogInformation("Fetching location data for city: {city}, state: {state}", city, state);

    string url = buildNominatimUrl(city, state);

    try {
      var responseString = await _httpClient.GetStringAsync(url);
      _logger.LogInformation(responseString);
      
      var locationData = parseLocationResponse(responseString);
      
      if(locationData != null) {
        await saveLocationAsync(locationData);
      }
	
      return locationData;

    } catch (HttpRequestException e) {
      _logger.LogInformation($"Error fetching: {e.Message}");
      return null;
    }
  }

  public async Task saveLocationAsync(Location location) {
    if(location == null) {
      throw new ArgumentNullException(nameof(location), "Location was null, but can't.");
    }
    _context.Locations.Add(location);
    await _context.SaveChangesAsync();
  }

  private string buildNominatimUrl(string city, string state) {
    string baseUrl = "https://nominatim.openstreetmap.org/search.php?q=";
    string parameters = city + ", " + state;

    string nominatimUrl = baseUrl + parameters;
    
    _logger.LogInformation($"LocationService.buildNominatimUrl:: {nominatimUrl}");
    return nominatimUrl;
  }

  private Location? parseLocationResponse(string responseString) {
    var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
    var result = JsonSerializer.Deserialize<LocationDetails[]>(responseString, options);

    if(result == null || result.Length == 0) {
      throw new Exception("No location data found");
    }

    System.Diagnostics.Debug.WriteLine($"response: {result[0]}");
    var response = result[0];
    return new Location {
      City = response.address?.county ?? string.Empty,
      State = response.address?.state ?? string.Empty,
      Latitude = double.Parse(response.lat ?? "0"),
      Longitude = double.Parse(response.lon ?? "0")
    };
  }

  public class LocationDetails {
    public string? place_id { get; set; }
    public string? licence { get; set; }
    public string? osm_type { get; set; }
    public int? osm_id { get; set; }
    public string? lat { get; set; }
    public string? lon { get; set; }
    public string? @class { get; set; }
    public string? type { get; set; }
    public int? place_rank { get; set; }
    public double? importance { get; set; }
    public string? addresstype { get; set; }
    public string? name { get; set; }
    public string? display_name { get; set; }
    public Address? address { get; set; }
    public string[]? boundingbox { get; set; }
    public string? svg { get; set; }
  }

  public class Address {
    public string? county { get; set; }
    public string? state { get; set; }
    public string? ISO3166_2_lvl4 { get; set; }
    public string? postcode { get; set; }
    public string? country { get; set; }
    public string? country_code { get; set; }
  }
}
}
