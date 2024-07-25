public class WeatherService {
  private readonly HttpClient _httpClient;

  public WeatherService(HttpClient httpClient) {
    _httpClient = httpClient;
  }

  public async Task<string?> GetWeatherByLatLon(string lat, string lon) {
    string url = buildOpenMeteoUrl(lat, lon);

    try {
      var responseString = await _httpClient.GetStringAsync(url);
      var weatherData = parseWeatherResponse(responseString);
      return weatherData;
    } catch (HttpRequestException e) {
      Console.WriteLine($"Error fetching: {e.Message}");
      return null;
    }
  }

  private string buildOpenMeteoUrl(string lat, string lon) {
    //TODO implement https://open-meteo.com/en/docs
    return "my url";
  }

  private string parseWeatherResponse(string responseString) {
    //TODO take the response and make it into a weather object
    return "the weather";
  }

  //TODO add getter that takes in url params and returns weather data
}
