public class Location {
  public int Id {get; set;}
  
  public string City {get; set;}
  public string State {get; set;}
  
  public double Latitude {get; set;}
  public double Longitude {get; set;}

  public double? Temperature {get; set;}
  public string? WeatherDescription {get; set;}

  public Location(string city, string state) {
    City = city;
    State = state;
  }
}
