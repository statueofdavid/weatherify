public class LocationService {
  private readonly HttpClient _httpClient;

  public LocationService(HttpClient httpClient) {
    _httpClient = httpClient;
  }

  public async Task<Location?> GetLocationCoordinates(string city, string state) {
    string url = BuildNominatimUrl(city, state);

    try {
      var responseString = await _httpClient.GetStringAsync(url);
      var locationData = ParseLocationResponse(responseString);

      if (locationData != null) {
        return new Location(city, state) {
	  Latitude = locationData.lat,
	  Longitude = locationData.lon
	};
      } else {
        return null;
      }
    } catch (HttpRequestException e) {
      Console.WriteLine($"Error fetching: {e.Message}");
      return null;
    }
  }

  private string BuildNominatimUrl(string city, string state) {
    //TODO https://nominatim.openstreetmap.org/ui/search.html?q=terre+haute%2C+IN
    //implement this
  }

  private Location? ParseLocationResponse(string responseString) {
    //TODO take the response and make it into at least an object
  }
 //TODO add a getter that returns int lat, int long, Time timeStamp
}
