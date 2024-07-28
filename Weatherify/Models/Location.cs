namespace Weatherify.Models
{
  public class Location {
    public Guid Id {get; set;}
  
    public string? City {get; set;}
    public string? State {get; set;}
  
    public double? Latitude {get; set;}
    public double? Longitude {get; set;}
  }
}
