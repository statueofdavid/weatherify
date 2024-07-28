namespace Weatherify.Models
{
  public class Weather {
    public Guid Id {get; set;}

    public double? Latitude {get; set;}
    public double? Longitude {get; set;}

    public float? GenerationTimeMs {get; set;}
    public double? UtcOffsetSeconds {get; set;}
  
    public string? TimezoneAbbr {get; set;}
    public double? Elevation {get; set;}
  }
}
